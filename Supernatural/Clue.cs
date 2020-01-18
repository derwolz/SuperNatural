using System;
using System.Collections.Generic;
using System.Text;

namespace Supernatural
{
    public class Clue
    {
        public enum Type
        {
            Claw, Blood,
            Hair, Goo,
            Arcane_Symbols, Bones,
            Inhuman_Footprints, Cold_Air
        }
        public Type Name { get; set; }
    }
}
