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
        private uint timer;
        
        // Coustructeur de la classe Game
        public Game(string mapFile, uint timer)
        {
            this.timer = timer;
            gameIsRunning = true;
            map = new Map(mapFile);
            player = new Player(1, 1);
            Enemies = new List<Ghosts>();
        }

        // Lance le jeu
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
                Console.SetCursorPosition(0, (int)map.GetHeight());
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, (int)map.GetHeight());
                Console.Write("Score : " + player.GetScore() + ", Timer : " + timer );
                
                System.Threading.Thread.Sleep(150); 
            }
            Console.WriteLine("Game over");
        }

        // Met a jour les informations du jeu
        public void Update()
        {
            if (timer == 0)
            {
                gameIsRunning = false;
                return;
            }

            timer--;
            
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
                if (map.GetCellType(new_pos.x, new_pos.y) == Map.CellType.Pacgum)
                {
                    player.IncreaseScore(1);
                    map.SetCellType(new_pos.x, new_pos.y, Map.CellType.Empty);
                }
                Console.SetCursorPosition(old_pos.x, old_pos.y);
                Console.Write(' ');
                player.SetPos(new_pos);
                player.PrintPlayer();
            }
        }
    }
}