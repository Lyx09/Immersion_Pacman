using System;
using System.Data;
using System.Reflection.Emit;

namespace Pacman
{
    public class Ghost : Character
    {
        private Game.CellType[,] map;
        
        // Only one behaviour for now
        public Ghost(Coords pos,  Game.CellType[,] map, Direction dir = Direction.Right, char icon = '#',
            ConsoleColor color = ConsoleColor.Cyan) : base(pos, dir, icon, color)
        {
            this.map = map;
        }

        public void Move()
        {
                        
        }
    }
}