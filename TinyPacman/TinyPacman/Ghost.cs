using System;
using System.Reflection.Emit;

namespace Pacman
{
    public class Ghost : Character
    {
        // One behaviour for now
        public Ghost(Coords pos,  Direction dir = Direction.Right, char icon = '#',
            ConsoleColor color = ConsoleColor.Cyan) : base(pos, dir, icon, color)
        {
            
        }
    }
}