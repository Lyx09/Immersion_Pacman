using System;
using System.Collections.Generic;
using System.IO;

namespace Pocman
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

        public Map(string mapFile)
        {
            pacgum = 0;
            using (StreamReader sr = new StreamReader(mapFile))
            {
                if (! uint.TryParse(sr.ReadLine(),out height))
                    throw new Exception("Error: Could not parse height");
                if (! uint.TryParse(sr.ReadLine(),out width))
                    throw new Exception("Error: Could not parse width");

                map = new CellType[height,width] ;
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

        public void PrintMap()
        {
            Console.Clear();
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    switch (map[i,j])
                    {
                        case CellType.Wall:
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write('X');
                            break;
                        case CellType.Pacgum:
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write('o');
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

        public uint GetHeight()
        {
            return height;
        }

        public uint GetWidth()
        {
            return width;
        }

        public CellType GetCellType(int x, int y)
        {
            if (x > height || y > height)
                throw new Exception("Requested cell is out of bounds");
            return map[x, y];
        }
    }
}