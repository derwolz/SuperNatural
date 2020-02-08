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
            GameActions.Damage(monster,Weapon.WeaponName.Wooden_Slug.ToString(), damage);
        }
        public static void BearTrap(Monster monster)
        {
            monster.Speed = 0;
            int damage = 2;
            GameActions.Damage(monster, Weapon.WeaponName.Bear_Trap.ToString(), damage) ;
        }
        public static void HolyWater(Monster monster)
        {
            if (monster.SubType == Monster.SubTypes.Undead || monster.SubType == Monster.SubTypes.Unholy)
            {
                monster.CastSpeed = 0;
                int damage = 2;
                GameActions.Damage(monster, Weapon.WeaponName.Holy_Water.ToString(), damage) ;
            }
        }
        public static void SilverBirdShot(Board board, List<Monster> monsters,Monster monster)
        {
                foreach (Monster _monster in monsters)
                {
                    if (board.GetAdjacentTiles(monster.Position).Contains(_monster.Position))
                    {
                    GameActions.Damage(_monster, Weapon.WeaponName.Silver_Bird_Shot.ToString(), 1);
                    }
                }
        }
        public static void RigTrap(Board board, Weapon weapon, Tile.Places place)
        {
            board.PlaceWeapon(place, weapon);
        }
        public static void ActivateTraps(List<Weapon> weapons, Monster monster, List<Monster> monsters, Board board)
        {
            if (weapons.Count > 0)
            {
                Console.WriteLine("{0} Trap(s) Activate!", weapons.Count);
                foreach (var weapon in weapons)
                    switch (weapon.Name)
                    {
                        case Weapon.WeaponName.Bear_Trap:
                            BearTrap(monster);
                            break;
                        case Weapon.WeaponName.Holy_Water:
                            HolyWater(monster);
                            break;
                        case Weapon.WeaponName.Silver_Bird_Shot:
                            SilverBirdShot(board, monsters, monster);
                            break;
                        default:
                            return;
                    }
            }
            else return;
        }
    }
}
