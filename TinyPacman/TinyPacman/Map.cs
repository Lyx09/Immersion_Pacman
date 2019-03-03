using System;
using System.Collections.Generic;
using System.IO;

namespace Pacman
{
    public class Map
    {
        public enum CellType
        {
            Wall,
            Pacgum,
            Empty,
        }

        private CellType[,] map;
        private uint height;
        private uint width;
        private uint pacgum;

        // Constructeur de la classe Map, initialises ses attributs
        public Map(string mapFile)
        {
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
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write('█');
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
    }
}