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
            Attack,
            doubleResearch,
            doubleInvestigate,
            filler,
            
        }
        public Type Name { get; set; }
    }
}
