using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Supernatural
{
    public static class GameActions
    {
        /// <summary>
        /// Global Actions are placed here that can be used by all players / make the game logic easier to 
        /// work through.
        /// </summary>

        public static List<Monster> Target(Board board, Player player, List<Monster> monsters, int range = 1)
            //returns all monsters within a certain distance
        {
            List<Monster> result = new List<Monster>();

            List<Tile.Places> places = new List<Tile.Places>() { player.Position };
            
            for (int i = 0; i < range; i++)
            {
                List<Tile.Places> _temp = new List<Tile.Places>();
                foreach (Tile.Places place in places)
                {
                    _temp.Add(place);
                    foreach (Tile.Places _place in board.GetAdjacentTiles(place))
                        _temp.Add(_place);
                }
                places = _temp;
            }
            
            foreach (Tile.Places place in places)
                foreach (Monster monster in monsters)
                {
                    if (monster.Position == place)
                        result.Add(monster);
                }
            return result.Distinct().ToList(); //ensures no duplicates
        }
        public static void DisplayMonsters(List<Monster> monsters)
            //displays all monsters in the specified list, often used in conjunction with target
        {
            int count = 0;
            foreach (Monster monster in monsters)
            {
                count += 1;
                string monsterName = "";
                if (monster.IsRevealed == false) monsterName = "Monster";
                else monsterName = monster.Name;
                monsterName = monster.IsRevealed ? monster.Name : "Mysterious Figure";
                Console.WriteLine("\n{0}) {1} is at {2}", count, monsterName, monster.Position);
            }
            return;
        }
        public static void Damage(Monster monster, String name, int damage) // damages the monster
        {
            Console.ForegroundColor = ConsoleColor.Green;
            string monsterName = monster.IsRevealed ? monster.Name : "Figure";
            Console.WriteLine("{0} deals {1} damage to {2}", name, damage, monsterName);
            monster.Health -= damage;
            if (monster.Health < 0)
                Console.WriteLine("The {0} Falls", monsterName);
            Console.ResetColor();

        }
    }
}
