using System;
using System.Collections.Generic;
using System.Text;

namespace Supernatural
{
    public class Monster
    {
        public Tile.Name Position { get; set; }
        public enum Type
        {
            WereWolf,
            Vampire
        }
        public enum AbilityType
        {
            Vampirism,
            SummonBats,
            ExtremeSpeed,
            ExtremePanic,
            None
        }
        public string Name { get; set; }

        public bool IsRevealed { get; set; }
        public bool IsActive { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int CountDown { get; set; }
        public int MaxCountDown { get; set; }
        public int Speed { get; set; }
        public AbilityType Ability1 { get; set; }
        public AbilityType Ability2 { get; set; }

        public List<Clue.Type> _MonsterClues = new List<Clue.Type>();
        public List<Clue.Type> MonsterClues { get {return _MonsterClues; } set { value = _MonsterClues; } }
        public Monster()
        {
            
            
            
            this.CountDown = 3;
            this.MaxCountDown = 3;
            
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
        public AbilityType SelectAbility()
        {
            Random r = new Random();
            switch (r.Next(2)-1)
            {
                case 1:
                    return Ability2;
                default:
                    return Ability1;
            }
        }
        public void UseAbility(Tile tile, List<Player> players, GameMaster gm)
        {
            List<Tile.Name> enemyAdjacent = new List<Tile.Name>();
            foreach (Player player in players)
            {
                List<Tile.Name> _list = tile.GetAdjacentPaths(player.Position);
                foreach (Tile.Name name in _list)
                    enemyAdjacent.Add(name);
            }
            List<Tile.Name> adjacent = tile.GetAdjacentPaths(Position);
            switch (SelectAbility())
            {
                case AbilityType.ExtremePanic:
                    foreach (Tile.Name name in adjacent)
                        tile.IncreasePanic(name);
                    break;
                case AbilityType.ExtremeSpeed:
                    for (int i = 0; i < Speed; i++)
                    {
                        Move(adjacent, players);
                        tile.IncreasePanic(Position);
                    }
                    break;
                case AbilityType.Vampirism:
                    int count = 0;
                    foreach (Tile.Name name in adjacent)
                    {
                        tile.IncreasePanic(name);
                        count += Convert.ToInt32(tile.GetPanic(name));
                    }
                    Health += count;
                    break;
                case AbilityType.SummonBats:
                    MonsterAbilities.SummonBats(adjacent, gm);
                    break;
                case AbilityType.None:
                    break;
            }
                    
        }
        
    }
}
