using System;
using System.Collections.Generic;
using System.Text;

namespace Supernatural
{
    public static class WeaponAbilities
    {
        public static int ShotgunDamage = 3;
        public static int RifleDamage = 1;
        public static List<Monster> Rifle(Board board, Player player, List<Monster> monsters)
        {
            List<Monster> result = new List<Monster>();
            List<Tile.Places> posresult = new List<Tile.Places>();
            foreach (Monster monster in monsters)
            {
                foreach (Tile.Places item in board.GetAdjacentTiles(player.Position))
                {
                    List<Tile.Places> tempList = new List<Tile.Places>();
                    tempList = board.GetAdjacentTiles(item);
                    foreach (Tile.Places item2 in tempList)
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
        public static void WoodenSlug(Board board, Player player, List<Monster> monsters)
        {

        }
    }
}
