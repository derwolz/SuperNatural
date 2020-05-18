using System;
using System.Collections.Generic;
using System.Text;

namespace SupernaturalLibrary
{
    public static class WeaponAbilities
    {
        //................................Weapon damage set..............................
        public static int ShotgunDamage = 3;
        public static int RifleDamage = 7;
        public static int StunDamage = 1;

        //..................................Start individual Abilities...................
        public static void WoodenSlug(Monster monster)
        {
            int damage;
            if (monster.Name == "Vampire")
                damage = 8;
            else damage = 2;
            GameActions.Damage(monster,Weapon.WeaponName.Wooden_Slug.ToString(), damage);
        }
        public static void StunGrenade(Monster monster)
        {
            monster.isStunned = true;
            GameActions.Damage(monster, Weapon.WeaponName.Stun_Grenade.ToString(), StunDamage) ;
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
        public static void ActivateTraps(List<Weapon> weapons, Monster monster, List<Monster> monsters, Board board)
        {
            if (weapons.Count > 0)
            {
                Console.WriteLine("{0} Trap(s) Activate!", weapons.Count);
                foreach (var weapon in weapons)
                    switch (weapon.Name)
                    {
                        case Weapon.WeaponName.Stun_Grenade:
                            StunGrenade(monster);
                            break;
                        case Weapon.WeaponName.Holy_Water:
                            HolyWater(monster);
                            break;
                        case Weapon.WeaponName.Silver_Bird_Shot:
                            SilverBirdShot(board, monsters, monster);
                            break;
                        case Weapon.WeaponName.Wooden_Slug:
                            WoodenSlug(monster);
                            break;
                        default:
                            return;
                    }
            }
            else return;
        }
        public static bool PlaceTrap(Board board, Player player)
        {
            Weapon _temp = player.Weapons.Find(x => x.Name == Weapon.WeaponName.Trap_Kit);
            player.Weapons.Remove(_temp);
            bool isSuccess = false;
            Console.WriteLine("Place which weapon as a trap?");
            for (int i = 0; i < player.Weapons.Count; i++)
                Console.WriteLine("{0}). {1}", i + 1, player.Weapons[i].Name.ToString());
            if (Int32.TryParse(Console.ReadLine(), out int numQuery))
            {
                if (numQuery < 1 || numQuery > player.Weapons.Count - 1)
                    if (player.Weapons[numQuery - 1].Name != Weapon.WeaponName.Trap_Kit)
                    {
                        board.PlaceWeapon(player.Position, player.Weapons[numQuery - 1]);
                        isSuccess = true;
                    }
            }
            if (!isSuccess) player.Weapons.Add(_temp);
            return isSuccess;
        }
    }
}
