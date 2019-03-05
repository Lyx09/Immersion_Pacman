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
        private List<Ghosts> ghosts;
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
            player = new Player(1, 1);
            ghosts = new List<Ghosts>();
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
                            case 'O':
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
                            Console.ForegroundColor = ConsoleColor.White;
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

        // Retourne la hauteur de la map
        public uint GetHeight()
        {
            return height;
        }

        // Retourne la largeur de la map
        public uint GetWidth()
        {
            return width;
        }

        // Retourne le nombre de pacgum
        public uint GetPacgum()
        {
            return pacgum;
        }

        // Reduit le nombre de pacbum de 1 et retourne le nouveau compte
        public uint DecreasePacgun()
        {
            if (pacgum <= 0)
                return pacgum;
            return --pacgum;
        }

        // Retourne le type de la cellule en position y, x
        public CellType GetCellType(int x, int y)
        {
            if (x > width || y > height)
                throw new Exception("Requested cell is out of bounds");
            return map[y, x];
        }
        
        // Definit le type de la cellule en position y, x
        public void SetCellType(int x, int y, CellType t)
        {
            if (x > width || y > height)
                throw new Exception("Requested cell is out of bounds");
            map[y, x] = t;
        }
        
        // Coustructeur de la classe Game
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
                Console.SetCursorPosition(0, (int)GetHeight());
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, (int)GetHeight());
                Console.Write("Score : " + player.GetScore() + ", Timer : " + timer );
                
                System.Threading.Thread.Sleep(150); 
            }
            Console.WriteLine("Game over");
        }

        // Met a jour les informations du jeu
        public void Update()
        {
            if (timer == 0)
            {
                gameIsRunning = false;
                return;
            }

            timer--;
            if (GetPacgum() == 0)
            {
                gameIsRunning = false;
            }
            
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
            if (new_pos.x > 0 && new_pos.x < GetWidth() &&
                new_pos.y > 0 && new_pos.y < GetHeight() &&
                GetCellType(new_pos.x, new_pos.y) != CellType.Wall)
            {
                if (GetCellType(new_pos.x, new_pos.y) == CellType.Pacgum)
                {
                    player.IncreaseScore(1);
                    SetCellType(new_pos.x, new_pos.y, CellType.Empty);
                }
                Console.SetCursorPosition(old_pos.x, old_pos.y);
                Console.Write(' ');
                player.SetPos(new_pos);
                player.PrintPlayer();
            }
        }
    }
}