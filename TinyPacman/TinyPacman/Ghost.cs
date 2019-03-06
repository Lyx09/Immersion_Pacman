using System;
using System.Collections.Generic;

namespace Pacman
{
    public class Ghost : Character
    {
        public Dictionary<Direction, Coords> d2c = new Dictionary<Direction, Coords>()
        {
            {Direction.Up, new Coords(0, -1)},
            {Direction.Left, new Coords(-1, 0)},
            {Direction.Down, new Coords(0, 1)},
            {Direction.Right, new Coords(1, 0)}
        };
        
        private Player player;
        private Game.CellType[,] map;

        // Only one behaviour for now
        public Ghost(Coords pos,  Game.CellType[,] map, Player player, Direction dir = Direction.Left, char icon = '#',
            ConsoleColor color = ConsoleColor.Cyan) : base(pos, dir, icon, color)
        {
            this.player = player;
            this.map = map;
        }
        
        public void Move()
        {
            // FIXME
        }
    }
}