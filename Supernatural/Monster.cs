using System;
using System.Collections.Generic;
using System.Text;

namespace Supernatural
{
    public class Monster
    {
        public Tile.Places Position { get; set; }
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
        private List<AbilityType> _abilities = new List<AbilityType>();
        public List<AbilityType> Abilities { get { return _abilities; } set { value = _abilities; } }

        public List<Clue.Type> _MonsterClues = new List<Clue.Type>();
        public List<Clue.Type> MonsterClues { get {return _MonsterClues; } set { value = _MonsterClues; } }
        public Monster()
        {
            this.CountDown = 3;
            this.MaxCountDown = 3;
        }
        public void Move(List<Tile.Places> _list,List< Player> Players)
        {
            Random r = new Random();
            List < Tile.Places > _tempList = new List<Tile.Places>();
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
            AbilityType result;
            result = Abilities[r.Next(Abilities.Count)];
            return result;
        }
        public void UseAbility(Board board, List<Player> players, GameMaster gm)
        {
            List<Tile.Places> enemyAdjacent = new List<Tile.Places>();
            foreach (Player player in players)
            {
                List<Tile.Places> _list = board.GetAdjacentTiles(player.Position);
                foreach (Tile.Places name in _list)
                    enemyAdjacent.Add(name);
            }
            List<Tile.Places> adjacent = board.GetAdjacentTiles(Position);
            switch (SelectAbility())
            {
                case AbilityType.ExtremePanic:
                    MonsterAbilities.ExtremePanic(board, this);
                    break;
                case AbilityType.ExtremeSpeed:
                    MonsterAbilities.ExtremeSpeed(board, this, players);
                    break;
                case AbilityType.Vampirism:
                    MonsterAbilities.Vampirism(board,this,players);
                    break;
                case AbilityType.SummonBats:
                    MonsterAbilities.SummonBats(board, gm);
                    
                    break;
                case AbilityType.None:
                    break;
            }
            
        }
        
    }
}
