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

        protected ConsoleColor color;
        protected Coords pos;
        protected Direction dir;
        protected char icon;

        // Constructeur de Character
        public Character(Coords pos, Direction dir, char icon, ConsoleColor color)
        {
            this.color = color;
            this.dir = dir;
            this.icon = icon;
            this.pos = pos;
        }

        // Affiche le joueur a la bonne position sur l'ecran
        public void Print()
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(pos.x, pos.y);
            Console.Write(icon);
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