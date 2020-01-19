﻿using System;
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
        public int MaxHealth = 10;
        public int CountDown { get; set; }
        public int MaxCountDown { get; set; }
        public int Speed { get; set; }
        public Monster()
        {
            this.Health = MaxHealth;
            this.CountDown = 3;
            this.MaxCountDown = 3;
            this.Speed = 2;
        }
        public void Move(List<Tile.Name> _list,List< Player> Players)
        {
            Random r = new Random();
            List < Tile.Name > _tempList = new List<Tile.Name>();
            foreach (var path in _list)
                _tempList.Add(path);
            foreach (var path in _list)
            {
                foreach (Player player in Players)
                {
                    if (player.Position.ToString() == path.ToString())
                        _tempList.Remove(path);
                }
            }
            int monsterMoveChoice = r.Next(_tempList.Count);
            Position = _tempList[monsterMoveChoice];
        }
    }
}