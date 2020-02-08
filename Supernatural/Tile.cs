using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Supernatural
{
    public class Tile
    {
        /// <summary>
        /// Tiles are the Areas on the board itself, each is named and carries a Panic value
        /// </summary>
        public enum Places
        {
            Baker_Street,
            Motel,
            Church,
            Graveyard,
            Shops,
            Library,
            Winchester_Ave,
            CornField,
            Bar,
            Trailer_Home,
            Junkyard,
            High_School_Stadium,
            High_School,
            Green_Meadows_Lane,
            Car_Dealership,
            CityHall,
            Mall_Food_Court,
            Mall_Back_Entrance,
            Mall_Front_Entrance,
            MallParkingLot,
        }

        public enum Panic
        {
            Level_0,
            Level_1,
            Level_2,
            Level_3,
            Level_4
        }
        public List<Places> AdjacentPaths = new List<Places>();

        public Places Name { get; set; }
        public Panic panic { get; set; }
        private List<Weapon> _Weapons = new List<Weapon>();
        public List<Weapon> PlacedWeapons { get { return _Weapons; } set { value = _Weapons; } }
    }
}
