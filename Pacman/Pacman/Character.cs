using System;

namespace Pacman
{
    public class Character
    {
        public enum Direction
        {
            Up,
            Left,
            Down,
            Right,
            Quit
        };

        protected char icon;
        protected Coords pos;
        protected Direction dir;
        
        // Affiche le joueur a la bonne position sur l'ecran
        public void PrintPlayer()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(pos.x, pos.y);
            Console.Write(icon);
            Console.SetCursorPosition(0, 0);
            Console.ResetColor();
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
    }
}