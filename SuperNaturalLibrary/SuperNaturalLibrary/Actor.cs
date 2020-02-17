using System;
using System.Collections.Generic;
using System.Text;

namespace SupernaturalLibrary
{
     public abstract class Actor
    {
        public string Name { get; set; }
        public Tile.Places Position { get; set; }
        public int Speed { get; set; }
        public int Range { get; set; }
    }
}
