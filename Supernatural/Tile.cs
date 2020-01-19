using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Supernatural
{
    public class Tile
    {
        public enum Name
        {
            BakerStreet,
            Motel,
            Church,
            Graveyard,
            Shops,
            Library,
            WinchesterAve,
            Bar,
            CornField,
            TrailerHome
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
        Dictionary<Name, Panic> PanicLevels = new Dictionary<Name, Panic>();
        public Name TileName { get; set; }

        public Tile()
        {
            var Values = Enum.GetValues(typeof(Name));
            List<Object> NewValues = new List<object>();
            foreach (var Value in Values)
                NewValues.Add(Value);
            for (int i = 0; i < 10; i++)
            {
                if (i < 9)
                    AdjacentPathA.Add((Name)NewValues[i], (Name)NewValues[i] + 1);
                if (i > 0)
                    AdjacentPathB.Add((Name)NewValues[i], (Name)NewValues[i] - 1);
                if (i == 0 || i == 5) 
                { 
                AdjacentPathC.Add((Name)NewValues[i], (Name)NewValues[i+4]);
                AdjacentPathC.Add((Name)NewValues[i+4], (Name)NewValues[i]);
                }
                if (i == 3)
                {
                    AdjacentPathC.Add((Name)NewValues[i], (Name)NewValues[i + 3]);
                    AdjacentPathC.Add((Name)NewValues[i + 3], (Name)NewValues[i]);
                }
                if (i == 2)
                {
                    AdjacentPathC.Add((Name)NewValues[i], (Name)NewValues[i + 5]);
                    AdjacentPathC.Add((Name)NewValues[i + 5], (Name)NewValues[i]);
                }
                
            }
           foreach (var path in AdjacentPathA)
                Console.WriteLine("{0} goes to {1}", path.Key, path.Value);
           foreach (var path in AdjacentPathB)
                Console.WriteLine("{0} goes to {1}", path.Key, path.Value);
           foreach (var path in AdjacentPathC)
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
            List<Name> results = names.Distinct().ToList();

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
