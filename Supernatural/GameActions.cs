using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Supernatural
{
    public static class GameActions
    {
        public static List<Monster> Target(Board board, Player player, List<Monster> monsters, int range = 1)
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
            return result.Distinct().ToList(); ;
        }
        public static void DisplayMonsters(List<Monster> monsters)
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
    }
}
