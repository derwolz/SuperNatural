using System;
using System.Collections.Generic;
using System.Text;

namespace Supernatural
{
    class WeaponFactory
    {
        public static List<Weapon.WeaponParts> HolyWater = new List<Weapon.WeaponParts>() {
            Weapon.WeaponParts.Holy_Vial, Weapon.WeaponParts.Purified_Water, Weapon.WeaponParts.Sacred_Blessing, Weapon.WeaponParts.Sacred_Blessing 
        };
        public static List<Weapon.WeaponParts> BearTrap = new List<Weapon.WeaponParts>() {
            Weapon.WeaponParts.Wooden_Stake, Weapon.WeaponParts.Large_Metal_Parts, Weapon.WeaponParts.Spring_set 
        };
        public static List<Weapon.WeaponParts> SilverBirdShot = new List<Weapon.WeaponParts>() {
            Weapon.WeaponParts.Blessed_Shotgun_Casing, Weapon.WeaponParts.Small_Silver_Pellets, Weapon.WeaponParts.Propellant 
        };
        public static List<Weapon.WeaponParts> WoodenSlug = new List<Weapon.WeaponParts>() {
            Weapon.WeaponParts.Propellant, Weapon.WeaponParts.Wooden_Stake, Weapon.WeaponParts.Blessed_Shotgun_Casing 
        };
        //public Weapon WeaponFactory()
        //{
            
        //}

    }
}
