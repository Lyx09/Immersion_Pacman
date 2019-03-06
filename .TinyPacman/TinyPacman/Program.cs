using System;

namespace Pacman
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("╔══════════════════════╗\n║");
            Console.ForegroundColor = ConsoleColor.Yellow;
            //Console.Write(" Welcome to Pᗣᗧ•••MᗣN ");
            Console.Write(" Welcome to PAC•••MAN ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("║\n╚══════════════════════╝");
            Console.ResetColor();
            Console.WriteLine("\n Press Enter to begin.");
            Console.ReadLine();
            
            Game game = new Game("map2.txt", 500);
            game.Launch();
        }
    }
}