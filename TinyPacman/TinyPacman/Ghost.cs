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

        private double dist(Coords c1, Coords c2)
        {
            double x = c1.x - c2.x;
            double y = c1.y - c2.y;
            return Math.Sqrt(x * x + y * y);
        }

        public void Move()
        {
            int intersect = 0;
            foreach (KeyValuePair<Direction, Coords> e in d2c)
            {
                Coords test = pos + e.Value;
                if (map[test.y, test.x] != Game.CellType.Wall)
                    intersect++;
            }

            if (intersect == 2)
            {
                Coords newpos = d2c[dir] + pos ;
                if (map[newpos.y, newpos.x] == Game.CellType.Wall)
                {
                    //L intersection    
                }
            }
            if (intersect >= 3)
            {
                Coords vec = player.GetPos() - pos;
                Direction newdir = dir;
                double diff = Math.Abs(vec.x) - Math.Abs(vec.y);
                if (diff > 0 && vec.x < 0 && dir != Oposite(dir))
                    newdir = Direction.Left;
                else if (diff > 0 && vec.x > 0 && dir != Oposite(dir))
                    newdir = Direction.Right;
                else if (vec.y < 0 && dir != Oposite(dir))
                    newdir = Direction.Up;
                else if (vec.y < 0 && dir != Oposite(dir))
                    newdir = Direction.Down;
                
                Coords newpos = d2c[newdir] + pos ;
                foreach (KeyValuePair<Direction,Coords> e in d2c)
                {
                    if (map[newpos.y, newpos.x] != Game.CellType.Wall)
                        break;
                    newdir = e.Key;
                    newpos = d2c[newdir] + pos;
                }

                dir = newdir;
            }
            Console.SetCursorPosition(pos.x, pos.y);
            Console.Write(' ');
            pos = d2c[dir] + pos;
        }

        public Direction Oposite(Direction d)
        {
            switch (d)
            {
                case Direction.Up:
                    return Direction.Down;
                case Direction.Down:
                    return Direction.Up;
                case Direction.Left:
                    return Direction.Right;
                case Direction.Right:
                    return Direction.Left;
                default:
                    return Direction.Quit;
            }
        }
    }
}