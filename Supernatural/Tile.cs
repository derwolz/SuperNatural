﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Supernatural
{
    public class Tile
    {
        public enum Name
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
        public Dictionary<Name, Name> AdjacentPathA = new Dictionary<Name, Name>();
        public Dictionary<Name, Name> AdjacentPathB = new Dictionary<Name, Name>();
        public Dictionary<Name, Name> AdjacentPathC = new Dictionary<Name, Name>();
        public Dictionary<Name, Name> AdjacentPathD = new Dictionary<Name, Name>();
        Dictionary<Name, Panic> PanicLevels = new Dictionary<Name, Panic>();
        public Name TileName { get; set; }

        public Tile()
        {
            var Values = Enum.GetValues(typeof(Name));
            List<Object> NewValues = new List<object>();
            foreach (var Value in Values)
                NewValues.Add(Value);
            for (int i = 0; i < NewValues.Count; i++)
            {
                if (i < NewValues.Count-1)
                    AdjacentPathA.Add((Name)NewValues[i], (Name)NewValues[i] + 1);
                if (i > 0)
                    AdjacentPathB.Add((Name)NewValues[i], (Name)NewValues[i] - 1);
                if (i -6 <= 0) 
                { 
                    AdjacentPathC.Add((Name)NewValues[i], (Name)NewValues[i+9]);
                    AdjacentPathC.Add((Name)NewValues[i+9], (Name)NewValues[i]);
                }
                if (i == 11)
                {
                    AdjacentPathD.Add((Name)NewValues[i], (Name)NewValues[i + 7]);
                    AdjacentPathD.Add((Name)NewValues[i + 7], (Name)NewValues[i]);
                }
                if (i == 0)
                {
                    AdjacentPathB.Add((Name)NewValues[i], (Name)NewValues[i + 7]);
                    AdjacentPathC.Add((Name)NewValues[i + 7], (Name)NewValues[i]);
                }
                if (i == 9)
                {
                    AdjacentPathD.Add((Name)NewValues[i], (Name)NewValues[i + 8]);
                    AdjacentPathD.Add((Name)NewValues[i + 8], (Name)NewValues[i]);
                }
                if (i == 13)
                {
                    AdjacentPathD.Add((Name)NewValues[i], (Name)NewValues[i + 6]);
                    AdjacentPathD.Add((Name)NewValues[i+6], (Name)NewValues[i]);
                }
                
            }
           foreach (var path in AdjacentPathA)
                Console.WriteLine("{0} goes to {1}", path.Key, path.Value);
           foreach (var path in AdjacentPathB)
                Console.WriteLine("{0} goes to {1}", path.Key, path.Value);
            foreach (var path in AdjacentPathC)
                Console.WriteLine("{0} goes to {1}", path.Key, path.Value);
            foreach (var path in AdjacentPathD)
                Console.WriteLine("{0} goes to {1}", path.Key, path.Value);


        }
        public List<Name> GetAdjacentPaths(Tile.Name name)
        {
            List<Name> names = new List<Name>();
            AdjacentPathA.TryGetValue(name, out Tile.Name result);
            if (name.ToString() != result.ToString())
                names.Add(result);  
            AdjacentPathB.TryGetValue(name, out result);
            if (name.ToString() != result.ToString())
                names.Add(result);
            AdjacentPathC.TryGetValue(name, out result);
            if (name.ToString() != result.ToString())
                names.Add(result);
            AdjacentPathD.TryGetValue(name, out result);
            if (name.ToString() != result.ToString())
                names.Add(result);
            List<Name> results = names.Distinct().ToList();
            if (names.Contains(name))
                names.Remove(name);

            return results;
        }
        //....................................................................Panic Controls...........................................................
        public void IncreasePanic(Tile.Name tile)
        {
            PanicLevels.TryGetValue(tile, out Panic _panic);
            if (_panic == Panic.Level_0)
            {
                PanicLevels.Remove(tile);
                PanicLevels.Add(tile, Panic.Level_1);
            }
            if (_panic == Panic.Level_1)
            {
                PanicLevels.Remove(tile);
                PanicLevels.Add(tile, Panic.Level_2);
            }
            else if (_panic == Panic.Level_2)
            {
                PanicLevels.Remove(tile);
                PanicLevels.Add(tile, Panic.Level_3);
            }
            else if (_panic == Panic.Level_3)
            {
                PanicLevels.Remove(tile);
                PanicLevels.Add(tile, Panic.Level_4);
            }
            else return;
        }
        public void DecreasePanic(Tile.Name tile)
        {

            PanicLevels.TryGetValue(tile, out Panic _panic);    
            if (_panic == Panic.Level_1)
            {
                PanicLevels.Remove(tile);
                PanicLevels.Add(tile, Panic.Level_0);
            }
            else if (_panic == Panic.Level_2)
            {
                PanicLevels.Remove(tile);
                PanicLevels.Add(tile, Panic.Level_1);
            }
            else if (_panic == Panic.Level_3)
            {
                PanicLevels.Remove(tile);
                PanicLevels.Add(tile, Panic.Level_2);
            }
        }
        public Tile.Panic GetPanic(Tile.Name tile)
        {
            PanicLevels.TryGetValue(tile, out Tile.Panic panic);
                return panic;
        }
    }
}
