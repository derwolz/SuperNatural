﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Supernatural
{
    public static class WeaponAbilities
    {
        public static int ShotgunDamage = 3;
        public static int RifleDamage = 1;
        public static bool Rifle(Tile tile, Player player, Monster monster)
        {
            List<Tile.Name> result = new List<Tile.Name>();
            foreach (Tile.Name item in tile.GetAdjacentPaths(player.Position))
            {
                List<Tile.Name> tempList = new List<Tile.Name>();
                tempList = tile.GetAdjacentPaths(item);
                foreach (Tile.Name item2 in tempList)
                {
                    result.Add(item2);
                }
                result.Add(item);
            }
            if (result.Contains(monster.Position))
                return true;
            else return false;
        }

        
            
    }
}
