using System;

namespace Pocman
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            
           Game game = new Game("map.txt");
           game.Launch();
        }
    }
}