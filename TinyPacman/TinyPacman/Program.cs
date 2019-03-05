using System;

namespace Pacman
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("╔══════════════════════╗\n║");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(" Welcome to Pᗣᗧ•••MᗣN ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("║\n╚══════════════════════╝");
            Console.ResetColor();
            Console.WriteLine("\n Press Enter to begin.");
            Console.ReadLine();
            
            Game game = new Game("map.txt", 500);
            game.Launch();
        }
    }
}