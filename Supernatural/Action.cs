using System;
using System.Collections.Generic;
using System.Text;

namespace Supernatural
{
    public class Action
    {
        //List of available actions the action deck is made out of
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
