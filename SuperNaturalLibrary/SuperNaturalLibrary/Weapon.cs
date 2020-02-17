using System;
using System.Collections.Generic;
using System.Text;

namespace SupernaturalLibrary
{
    public class Weapon
    {
        public enum WeaponParts
        {
            Holy_Vial,
            Sacred_Blessing,
            Purified_Water,
            Holy_Rites,
            Wooden_Stake,
            Large_Metal_Parts,
            Spring_set,
            Small_Silver_Pellets,
            Blessed_Shotgun_Casing,
            Propellant,
            Magnesium,
            Pipe
        }
        public enum WeaponName
        {
            Holy_Water = 0,
            Trap_Kit = 1,
            Silver_Bird_Shot = 2,
            Wooden_Slug = 3,
            Stun_Grenade = 4
        }
        //List of possible combinations make up weapons, Research will check against this when building a weapon
        //to produce a list of viable builds
        public WeaponName Name { get; set; }
        public int Uses { get; set; }
        public void UseAbility(Board board, Player player, Monster monster)
        {

        }

    }
}
