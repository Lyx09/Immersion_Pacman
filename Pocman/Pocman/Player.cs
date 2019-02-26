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
        private int score;

        // Constructeur de la classe Joueur
        public Player(int x, int y, Direction dir = Direction.Right, char icon = '●')
        {
            score = 0;
            this.icon = icon;
            pos = new Coords(x, y);
            this.dir = dir;
        }

        // Affiche le joueur a la bonne position sur l'ecran
        public void PrintPlayer()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(pos.x, pos.y);
            Console.Write(icon);
            Console.SetCursorPosition(0, 0);
            Console.ResetColor();
        }

        // Renvoie le score actuel du joueur
        public int GetScore()
        {
            return score;
        }
        
        // Augmente le score actuel du joueur de s
        public void IncreaseScore(int s)
        {
            score += s;
        }
        
        // Retourne la direction dans laquelle pointe le joueur
        public Direction GetDir()
        {
            return dir;
        }

        // Retourne la position du joueur
        public Coords GetPos()
        {
            return pos;
        }

        // Definit la position du joueur
        public void SetPos(Coords c)
        {
            pos = c;
        }

        // Recupere l'action du joueur
        public Direction GetInput()
        {
            if (!Console.KeyAvailable)
                return dir;

            ConsoleKeyInfo key = Console.ReadKey(true);
            switch (key.Key)
            {  
                case ConsoleKey.W:
                    dir = Direction.Up;
                    icon = 'ᗢ';
                    break;
                case ConsoleKey.A:
                    dir = Direction.Left;
                    icon = 'ᗤ';
                    break;
                case ConsoleKey.S:
                    dir = Direction.Down;
                    icon = 'ᗣ';
                    break;
                case ConsoleKey.D:
                    dir = Direction.Right;
                    icon = 'ᗧ';
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