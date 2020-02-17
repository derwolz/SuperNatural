using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SupernaturalLibrary
{
    public class WeaponFactory
    {
        public static List<Weapon.WeaponParts> HolyWater = new List<Weapon.WeaponParts>() {
            Weapon.WeaponParts.Holy_Vial, Weapon.WeaponParts.Purified_Water, Weapon.WeaponParts.Sacred_Blessing
        };
        public static List<Weapon.WeaponParts> TrapKit = new List<Weapon.WeaponParts>() {
            Weapon.WeaponParts.Wooden_Stake, Weapon.WeaponParts.Large_Metal_Parts, Weapon.WeaponParts.Spring_set 
        };
        public static List<Weapon.WeaponParts> SilverBirdShot = new List<Weapon.WeaponParts>() {
            Weapon.WeaponParts.Blessed_Shotgun_Casing, Weapon.WeaponParts.Small_Silver_Pellets, Weapon.WeaponParts.Propellant 
        };
        public static List<Weapon.WeaponParts> WoodenSlug = new List<Weapon.WeaponParts>() {
            Weapon.WeaponParts.Propellant, Weapon.WeaponParts.Wooden_Stake, Weapon.WeaponParts.Blessed_Shotgun_Casing 
        };
        public static List<Weapon.WeaponParts> StunGrenade = new List<Weapon.WeaponParts>()
        {
            Weapon.WeaponParts.Propellant, Weapon.WeaponParts.Magnesium, Weapon.WeaponParts.Pipe
        };
        public static int[] Uses = new int[] { 2, 3, 4, 2, 2 };
        public static List<List<Weapon.WeaponParts>> WeaponPartsList = new List<List<Weapon.WeaponParts>>() 
        { HolyWater, TrapKit, SilverBirdShot, WoodenSlug, StunGrenade };
        
        public bool MakeWeapon(Player player, List<Player> players)
        {
            List<Weapon.WeaponName> weaponOptions = new List<Weapon.WeaponName>();   
            StringBuilder builder = new StringBuilder();
            int count = 0;
            int i = 0;
            int[] arr = new int[WeaponPartsList.Count];
            foreach (var item in WeaponPartsList)
            {
                if (player.WeaponHand.Where(x => item.Contains(x)).Distinct().Count() == item.Count)
                {   
                    weaponOptions.Add((Weapon.WeaponName)i);
                    builder.Append((count + 1).ToString() + "). " + weaponOptions[count].ToString() + "\n");
                    arr[count] = i;
                    count += 1;
                }
                i += 1;
            }
            if (count > 0)
            {
                    Console.WriteLine(builder.ToString());
                    Int32.TryParse(Console.ReadLine(), out int numQuery);
                    if (numQuery < 1 || numQuery > count) numQuery = 1;
                foreach (var _player in players)
                {
                    Weapon weapon = new Weapon();
                    weapon.Name = weaponOptions[numQuery - 1];
                    weapon.Uses = Uses[numQuery - 1];
                    foreach (var item in WeaponPartsList[arr[numQuery - 1]])
                        _player.WeaponHand.Remove(item);
                    _player.Weapons.Add(weapon);
                }
                
                return true;
            }
            else return false;
        }

    }
}
