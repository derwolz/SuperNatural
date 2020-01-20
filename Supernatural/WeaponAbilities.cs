using System;
using System.Collections.Generic;
using System.Text;

namespace Supernatural
{
    public static class WeaponAbilities
    {
        public static int ShotgunDamage = 3;
        public static int RifleDamage = 1;
        public static List<Monster> Rifle(Tile tile, Player player, List<Monster> monsters)
        {
            List<Monster> result = new List<Monster>();
            List<Tile.Name> posresult = new List<Tile.Name>();
            foreach (Monster monster in monsters)
            {
                foreach (Tile.Name item in tile.GetAdjacentPaths(player.Position))
                {
                    List<Tile.Name> tempList = new List<Tile.Name>();
                    tempList = tile.GetAdjacentPaths(item);
                    foreach (Tile.Name item2 in tempList)
                    {
                        posresult.Add(item2);
                    }
                    posresult.Add(item);
                }
                if (posresult.Contains(monster.Position))
                    result.Add(monster);
            }
            return result;
        }
    }
}
