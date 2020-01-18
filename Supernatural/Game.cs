using System;
using System.Collections.Generic;
using System.Text;

namespace Supernatural
{
    public abstract class Game
    {
        public abstract void Play();
        public List<Player> _Players = new List<Player>();
        public List<Player> Players { get { return _Players; } set { value = _Players; } }
        
    }
}
