using System;
using System.Data;

namespace Pacman
{
    public class Player : Character
    {
        private int score;

        // Constructeur de la classe Joueur
        public Player(int x, int y, Direction dir = Direction.Right, char icon = '●')
        {
            score = 0;
            this.icon = icon;
            pos = new Coords(x, y);
            this.dir = dir;
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
        

        // Recupere l'action du joueur
        public Direction GetInput()
        {
            if (!Console.KeyAvailable)
                return dir;

            ConsoleKeyInfo key = Console.ReadKey(true);
            switch (key.Key)
            {  
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    dir = Direction.Up;
                    icon = 'ᗢ';
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    dir = Direction.Left;
                    icon = 'ᗤ';
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    dir = Direction.Down;
                    icon = 'ᗣ';
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
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