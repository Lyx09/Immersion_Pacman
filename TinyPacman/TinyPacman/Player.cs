using System;

namespace Pacman
{
    public class Player : Character
    {
        private int score;

        // Constructeur de la classe Joueur
        public Player(Coords pos, Direction dir = Direction.Right, char icon = '●',
            ConsoleColor color = ConsoleColor.Yellow) : base(pos, dir, icon, color)
        {
            score = 0;
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
            // FIXME
            return Direction.Right;
        }
    }
}