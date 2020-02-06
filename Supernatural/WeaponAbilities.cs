using System;
using System.Collections.Generic;
using System.Text;

namespace Supernatural
{
    public static class WeaponAbilities
    {
        //................................Weapon damage set..............................
        public static int ShotgunDamage = 3;
        public static int RifleDamage = 1;
        
        //..................................Start individual Abilities...................
        public static void WoodenSlug(Player player, Monster monster)
        {
            int damage;
            if (monster.Name == "Vampire")
                damage = 8;
            else damage = 2;
            player.Damage(monster, damage);
        }
    }
}
