using System;
using System.Collections.Generic;
using System.Text;

namespace Supernatural
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
            Propellant
        }
        public enum WeaponName
        {
            Holy_Water,
            Bear_Trap,
            Silver_Bird_Shot,
            Wooden_Slug
        }
        //List of possible combinations make up weapons, Research will check against this when building a weapon
        //to produce a list of viable builds
        public WeaponName Name { get; set; }
        public void UseAbility(Board board, Player player, Monster monster)
        {

        }

    }
}
