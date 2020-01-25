using System;
using System.Collections.Generic;
using System.Text;

namespace Supernatural
{
    public class Action
    {
        public enum Type
        {
            doubleMove,
            Shotgun,
            doubleInvestigate,
            doubleSearch,
            filler,
            Rifle
            
        }
        public Type Name { get; set; }
    }
}
