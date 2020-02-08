using System;
using System.Collections.Generic;
using System.Text;

namespace Supernatural
{
    public class WeaponDeck
    {
        private List<Weapon.WeaponParts> _Deck = new List<Weapon.WeaponParts>();
        public List<Weapon.WeaponParts> Deck { get { return _Deck; } set { value = _Deck; } }
        public WeaponDeck()
        {
            int weaptype = Enum.GetValues(typeof(Weapon.WeaponParts)).Length;
            for ( int j = 0; j < weaptype; j++)
            {
                for (int i = 0; i < 3; i++)
                {
                    Deck.Add((Weapon.WeaponParts)j);
                }
            }
            
        }
    }
}
