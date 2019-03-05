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
        private uint height;
        private uint pacgum;
        private uint timer;
        private uint width;
        public Player player;

        // Constructeur de la classe Game, initialises ses attributs
        public Game(string mapFile, uint timer)
        {
            this.timer = timer;
            gameIsRunning = true;
            player = new Player(new Coords(1, 1));
            ghosts = new List<Ghost>();
            pacgum = 0;
            using (StreamReader sr = new StreamReader(mapFile))
            {
                if (!uint.TryParse(sr.ReadLine(), out height))
                    throw new Exception("Error: Could not parse height");
                if (!uint.TryParse(sr.ReadLine(), out width))
                    throw new Exception("Error: Could not parse width");

                map = new CellType[height, width];
                for (int i = 0; i < height; i++)
                {
                    string line = sr.ReadLine();
                    for (int j = 0; j < width; j++)
                    {
                        switch (line[j])
                        {
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
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
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
                Console.SetCursorPosition(0,  map.GetLength(0));
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, map.GetLength(0));
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
                PrintScore();
                
                System.Threading.Thread.Sleep(150); 
            }
            Console.Clear();
            Console.WriteLine("Game over");
            if (timer == 0)
                Console.WriteLine("You lost!");
            if (pacgum == 0)
                Console.WriteLine("You win!");
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
            int height = map.GetLength(0);
            int width = map.GetLength(1);
            Coords new_pos = old_pos + new Coords(delta_x, delta_y);
            
            if (new_pos.x > 0 && new_pos.x < width && new_pos.y > 0 && new_pos.y < height &&
                GetCellType(new_pos) != CellType.Wall)
            {
                if (GetCellType(new_pos) == CellType.Pacgum)
                    EatPacgum(new_pos);
                player.SetPos(new_pos);
                
                // We clear the old position
                Console.SetCursorPosition(old_pos.x, old_pos.y);
                Console.Write(' ');
            }
        }
    }
}