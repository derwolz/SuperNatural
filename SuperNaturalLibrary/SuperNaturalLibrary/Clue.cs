using System;
using System.Collections.Generic;
using System.Text;

namespace SupernaturalLibrary
{
    public class Clue
    {
        //List of clues with a value of validity
        public enum Type
        {
            Claw, Blood,
            Hair, Goo,
            Arcane_Symbols, Bones,
            Inhuman_Footprints, Cold_Air, 
            Broken_Holy_Symbols
        }
        public Type Name { get; set; }
        public bool IsReal { get; set; }
    }
}
