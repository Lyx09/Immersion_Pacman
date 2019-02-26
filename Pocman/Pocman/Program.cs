using System;

namespace Pocman
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("╔══════════════════════╗");
            Console.Write("║");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(" Welcome to Pᗣᗧ•••MᗣN ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("║"); 
            Console.WriteLine("╚══════════════════════╝");
            Console.ResetColor();
            Console.WriteLine("\n Press Enter to begin.");
            Console.ReadLine();
            
            Game game = new Game("map.txt", 500);
            game.Launch();
        }
    }
}