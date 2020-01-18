using System;
using System.Collections.Generic;
using System.Text;

namespace Supernatural
{
    public class Monster
    {
        public Tile.Name Position { get; set; }
        public string Name { get; set; }

        public bool IsRevealed { get; set; }
        public bool IsActive { get; set; }
        public int Health { get; set; }
        public int CountDown { get; set; }
        public int MaxCountDown { get; set; }
        public Monster()
        {
            this.Health = 5;
            this.CountDown = 3;
            this.MaxCountDown = 3;
        }
    }
}
