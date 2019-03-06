using System;
using System.Collections.Generic;
using System.IO;

namespace Pacman
{
    public class Game
    {
        public enum CellType
        {
            Wall,
            Pacgum,
            Empty,
        }

        private CellType[,] map;
        private List<Ghost> ghosts;
        private bool gameIsRunning;
        private int height;
        private int width;
        private uint pacgum;
        private uint timer;
        public Player player;

        // Constructeur de la classe Game, initialises ses attributs
        public Game(string mapFile, uint timer)
        {
            gameIsRunning = true;
            ghosts = new List<Ghost>();
            pacgum = 0;
            player = new Player(new Coords(1, 1));
            this.timer = timer;
            
            using (StreamReader sr = new StreamReader(mapFile))
            {
                if (!int.TryParse(sr.ReadLine(), out height))
                    throw new Exception("Error: Could not parse height");
                if (!int.TryParse(sr.ReadLine(), out width))
                    throw new Exception("Error: Could not parse width");

                map = new CellType[height, width];
                for (int i = 0; i < height; i++)
                {
                    string line = sr.ReadLine();
                    for (int j = 0; j < width; j++)
                    {
                        switch (line[j])
                        {
                            case 'G':
                                map[i, j] = CellType.Empty;
                                ghosts.Add(new Ghost(new Coords(j,i), map, player));
                                break;
                            case '.':
                                map[i, j] = CellType.Pacgum;
                                pacgum += 1;
                                break;
                            case 'X':
                                map[i, j] = CellType.Wall;
                                break;
                            case ' ':
                                map[i, j] = CellType.Empty;
                                break;
                            default:
                                throw new Exception("Error while parsing map: Character unknown: " + line[j]);
                        }
                    }
                }
            }
        }

        // Affiche la map
        public void PrintMap()
        {
            Console.Clear();
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    switch (map[i, j])
                    {
                        case CellType.Wall:
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.Write(' ');
                            break;
                        case CellType.Pacgum:
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write('·');
                            break;
                        case CellType.Empty:
                            Console.Write(' ');
                            break;
                        default:
                            Console.Write('?');
                            break;
                    }
                    Console.ResetColor();
                }

                Console.WriteLine();
            }

            Console.ResetColor();
        }

        // Reduit le nombre de pacgum de 1 et retourne le nouveau compte
        public void EatPacgum(Coords c)
        {
            if (GetCellType(c) != CellType.Pacgum) // For safety
                return;
            player.IncreaseScore(1);
            SetCellType(c, CellType.Empty);
            if (pacgum > 0) // For safety
                --pacgum;
        }

        // Retourne le type de la cellule en position y, x
        public CellType GetCellType(Coords c)
        {
            if (c.x > width || c.y > height)
                throw new Exception("Requested cell is out of bounds");
            return map[c.y, c.x];
        }
        
        // Definit le type de la cellule en position y, x
        public void SetCellType(Coords c, CellType t)
        {
            if (c.x > width || c.y > height)
                throw new Exception("Requested cell is out of bounds");
            map[c.y, c.x] = t;
        }

        public void PrintScore()
        {
                Console.SetCursorPosition(0,  height);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, width);
                Console.Write("Score : " + player.GetScore() + ", Timer : " + timer );
        }

        // Lance le jeu
        public void Launch()
        {
            PrintMap();
            while (gameIsRunning)
            {
                // Input
                player.GetInput();
                // Update
                Update();
                // Print
                player.Print();
                foreach (Ghost ghost in ghosts)
                    ghost.Print();
                PrintScore();
                
                System.Threading.Thread.Sleep(150); 
            }
            Console.Clear();
            Console.WriteLine("Game over");
            if (pacgum == 0)
                Console.WriteLine("You win!");
            else
                Console.WriteLine("You lost!"); // timer == 0 or eaten by a ghost
        }

        // Met a jour les informations du jeu
        public void Update()
        {
            if (timer == 0 || pacgum == 0)
            {
                gameIsRunning = false;
                return;
            }

            MovePlayer();
            foreach (Ghost ghost in ghosts)
            {
                ghost.Move();
                if (ghost.GetPos() == player.GetPos())
                {
                    gameIsRunning = false;
                    return;
                }
            }

            timer--;
        }
        
        public void MovePlayer()
        {
            int delta_x = 0;
            int delta_y = 0;
            switch (player.GetDir())
            {
               case Player.Direction.Quit:
                   gameIsRunning = false;
                   return;
               case Player.Direction.Left:
                   delta_x = -1;
                   break;
               case Player.Direction.Right:
                   delta_x = 1;
                   break;
               case Player.Direction.Up:
                   delta_y = -1;
                   break;
               case Player.Direction.Down:
                   delta_y = 1;
                   break;
            }

            Coords old_pos = player.GetPos();
            Coords new_pos = old_pos + new Coords(delta_x, delta_y);

            if (new_pos.y >= height)
                new_pos.y = 0;
            if (new_pos.x >= width)
                new_pos.x = 0;
            if (new_pos.y < 0)
                new_pos.y = height - 1;
            if (new_pos.x < 0)
                new_pos.x = width - 1;
            
            
            if (GetCellType(new_pos) != CellType.Wall)
            {
                player.SetPos(new_pos);
                if (GetCellType(new_pos) == CellType.Pacgum)
                    EatPacgum(new_pos);
                
                // We clear the old position
                Console.SetCursorPosition(old_pos.x, old_pos.y);
                Console.Write(' ');
            }
        }
    }
}