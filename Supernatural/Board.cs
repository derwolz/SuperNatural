using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Supernatural
{
    public class Board
    {
        private List<Tile> _tiles = new List<Tile>();
        public List<Tile> Tiles { get { return _tiles; } set { value = _tiles; } }
        public List<Tile.Places> ResearchAreas = new List<Tile.Places>() { Tile.Places.Church, Tile.Places.Shops, Tile.Places.Bar, Tile.Places.Junkyard };
        public List<Tile.Places> InvestigationAreas = new List<Tile.Places>() { Tile.Places.Trailer_Home, Tile.Places.Motel, Tile.Places.CityHall, Tile.Places.Library };
        //............This determines the size of the board...............................
        private int BoardSize = Enum.GetValues(typeof(Tile.Places)).Length; 
        public Board()
        {
            for (int i = 0; i < BoardSize; i++)
            {
                Tile tile = new Tile();
                tile.Name = (Tile.Places)i;//casts value i to a tiles name value
                //.................................................................................
                //.......This Creates the path relationships on the Board..........................
                //.................................................................................
                if (i > 0)
                    tile.AdjacentPaths.Add((Tile.Places)(i - 1));
                if (i < BoardSize-1)
                    tile.AdjacentPaths.Add((Tile.Places)(i + 1));
                if (i < 7)
                    tile.AdjacentPaths.Add((Tile.Places)(i + 9));
                if (i >= 9 && i < 16)
                    tile.AdjacentPaths.Add((Tile.Places)(i - 9));
                if (i == 11 || i == 0)
                    tile.AdjacentPaths.Add((Tile.Places)(i + 7));
                if (i == 7 || i == 18)
                    tile.AdjacentPaths.Add((Tile.Places)(i - 7));
                if (i == 13)
                    tile.AdjacentPaths.Add((Tile.Places)(i + 6));
                if (i == 19)
                    tile.AdjacentPaths.Add((Tile.Places)(i - 6));
                if (i == 9)
                    tile.AdjacentPaths.Add((Tile.Places)(i + 8));
                if (i == 17)
                    tile.AdjacentPaths.Add((Tile.Places)(i - 8));
                Tiles.Add(tile);
            }
            
        }
        public List<Tile.Places> GetAdjacentTiles(Tile.Places position)
            //returns a list of adjacent tiles to the selected tile
        {
            List<Tile.Places> result;
            foreach (Tile tile in Tiles)
                if (tile.Name == position)
                {
                    result = tile.AdjacentPaths;
                    return result;
                }
            throw new Exception();
        }
        public Tile.Panic GetPanic(Tile.Places position)
            //returns the panic level of the specified tile
        {
            Tile.Panic result;
            foreach (Tile tile in Tiles)
                if (tile.Name == position)
                {
                    result = tile.panic;
                    return result;
                }
            throw new Exception();
        }
        public void IncreasePanic(Tile.Places position)
            //increases the panic level of the specified tile
        {
            foreach (Tile tile in Tiles)
                if (tile.Name == position)
                {
                    switch (tile.panic) {
                        case Tile.Panic.Level_0:
                            tile.panic = Tile.Panic.Level_1;
                            break;
                        case Tile.Panic.Level_1:
                            tile.panic = Tile.Panic.Level_2;
                            break;
                        case Tile.Panic.Level_2:
                            tile.panic = Tile.Panic.Level_3;
                            break;
                        case Tile.Panic.Level_3:
                            tile.panic = Tile.Panic.Level_4;
                            break;
                        default:
                            break;
                    }
                    return;
                }
        }
        public void DecreasePanic(Tile.Places position)
            //decreases the panic level of the specified tile
        {
            foreach (Tile tile in Tiles)
                if (tile.Name == position)
                {
                    switch (tile.panic)
                    {
                        case Tile.Panic.Level_3:
                            tile.panic = Tile.Panic.Level_2;
                            break;
                        case Tile.Panic.Level_2:
                            tile.panic = Tile.Panic.Level_1;
                            break;
                        case Tile.Panic.Level_1:
                            tile.panic = Tile.Panic.Level_0;
                            break;
                        default:
                            break;
                    }
                    return;
                }
        }
        public void PlaceWeapon(Tile.Places place, Weapon weapon)
            //Places a weapon at a specified tile
        {
            foreach (var tile in Tiles)
                if (tile.Name == place)
                    tile.PlacedWeapons.Add(weapon);
        }
        public bool springTrap(Tile.Places place, Monster monster, List<Monster> monsters, Board board)
        {
            foreach (var tile in Tiles)
                if (place == tile.Name)
                {
                    WeaponAbilities.ActivateTraps(tile.PlacedWeapons, monster, monsters, board);
                    return true;
                }
            return false;
        }
    }
}
