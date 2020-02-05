using System;
using System.Collections.Generic;
using System.Text;

namespace Supernatural
{
    public static class Weapon
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
        public static List<WeaponParts> HolyWater = new List<WeaponParts>() { WeaponParts.Holy_Vial, WeaponParts.Purified_Water, WeaponParts.Sacred_Blessing, WeaponParts.Sacred_Blessing };
        public static List<WeaponParts> BearTrap = new List<WeaponParts>() { WeaponParts.Wooden_Stake, WeaponParts.Large_Metal_Parts, WeaponParts.Spring_set };
        public static List<WeaponParts> SilverBirdShot = new List<WeaponParts>() { WeaponParts.Blessed_Shotgun_Casing, WeaponParts.Small_Silver_Pellets, WeaponParts.Propellant };
        public static List<WeaponParts> WoodenSlug = new List<WeaponParts>() { WeaponParts.Propellant, WeaponParts.Wooden_Stake, WeaponParts.Blessed_Shotgun_Casing };

    }
}
