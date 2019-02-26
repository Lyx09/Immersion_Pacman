using System;
using System.Data;

namespace Pocman
{
    public class Player
    {
        public enum Direction
        {
            Up,
            Left,
            Down,
            Right,
            Quit
        };

        private char icon;
        private Coords pos;
        private Direction dir;

        public Player(int x, int y, Direction dir = Direction.Right, char icon = 'P')
        {
            this.icon = icon;
            pos = new Coords(x, y);
            this.dir = dir;
        }

        public void PrintPlayer()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(pos.x, pos.y);
            Console.Write(icon);
            Console.SetCursorPosition(0, 0);
            Console.ResetColor();
        }

        public Direction GetDir()
        {
            return dir;
        }

        public Coords GetPos()
        {
            return pos;
        }

        public void SetPos(Coords c)
        {
            pos = c;
        }

        public Direction GetInput()
        {
            if (!Console.KeyAvailable)
                return dir;

            ConsoleKeyInfo key = Console.ReadKey(true);
            switch (key.Key)
            {  
                case ConsoleKey.W:
                    dir = Direction.Up;
                    break;
                case ConsoleKey.A:
                    dir = Direction.Left;
                    break;
                case ConsoleKey.S:
                    dir = Direction.Down;
                    break;
                case ConsoleKey.D:
                    dir = Direction.Right;
                    break;
                case ConsoleKey.Escape:
                    dir = Direction.Quit;
                    break;
                default:
                    break;
            }
            return dir;
        }
    }
}