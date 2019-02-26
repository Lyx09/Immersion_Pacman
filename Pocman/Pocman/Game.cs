using System;
using System.Collections.Generic;
using System.Media;

namespace Pocman
{
    public class Game
    {
        private Map map;
        private Player player;
        private List<Ghosts> Enemies;
        private bool gameIsRunning;
        
        public Game(string mapFile)
        {
            gameIsRunning = true;
            map = new Map(mapFile);
            player = new Player(1, 1);
            Enemies = new List<Ghosts>();
        }

        public void Launch()
        {
            map.PrintMap();
            while (gameIsRunning)
            {
                // Input
                player.GetInput();
                // Update
                Update();
                // Print
                
                System.Threading.Thread.Sleep(50); 
            }
            Console.WriteLine("Game over");
        }

        public void Update()
        {
            int delta_x = 0;
            int delta_y = 0;
            switch (player.GetDir())
            {
               case Player.Direction.Quit:
                gameIsRunning = false;
                return;
               case Player.Direction.Left:
                   delta_x = -1;
                   break;
               case Player.Direction.Right:
                   delta_x = 1;
                   break;
               case Player.Direction.Up:
                   delta_y = -1;
                   break;
               case Player.Direction.Down:
                   delta_y = 1;
                   break;
            }

            Coords old_pos = player.GetPos();
            Coords new_pos = old_pos + new Coords(delta_x, delta_y);
            if (new_pos.x > 0 && new_pos.x < map.GetWidth() &&
                new_pos.y > 0 && new_pos.y < map.GetHeight() &&
                map.GetCellType(new_pos.x, new_pos.y) != Map.CellType.Wall)
            {
                
                Console.SetCursorPosition(old_pos.x, old_pos.y);
                Console.Write(' ');
                player.SetPos(new_pos);
                player.PrintPlayer();
            }
        }
    }
}