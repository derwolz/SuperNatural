using System;
using System.Collections.Generic;
using System.Text;

namespace Supernatural
{
    class Weapon
    {
        public enum WeaponParts
        {
            Holy_Vial,
            Sacred_Blessing,
            Purified_Water,
            Holy_Rites,
            Wooden_Stake,

        }
        public List<WeaponParts> HolyWater = new List<WeaponParts>() { WeaponParts.Holy_Vial, WeaponParts.Purified_Water, WeaponParts.Sacred_Blessing, WeaponParts.Sacred_Blessing };

    }
}
