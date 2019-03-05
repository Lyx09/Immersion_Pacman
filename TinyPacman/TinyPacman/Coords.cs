using System.Xml;

namespace Pacman
{
    public struct Coords
    {
        public int x;
        public int y;

        public Coords(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Coords operator +(Coords c1, Coords c2)
        {
            return new Coords(c1.x + c2.x, c1.y + c2.y);
        }

        public static bool operator !=(Coords c1, Coords c2)
        {
            return (c1.x != c2.x || c1.y != c2.y);
        }

        public static bool operator ==(Coords c1, Coords c2)
        {
            return !(c1 != c2);
        }

    }
}