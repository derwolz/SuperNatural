using System;
using System.Collections.Generic;
using System.Text;

namespace SupernaturalLibrary
{
    public class Monster : Actor
    {
        
        public enum Type
        {
            WereWolf,
            Vampire,
            Ghoul,
            DoppelGanger,
            Ghosts,
            Wendigos,
            Banshees,
            Bat
        }
        public enum AbilityType
        {
            Vampirism,
            SummonBats,
            ExtremeSpeed,
            ExtremePanic,
            None
        }
        private enum AbilityTier 
        {
            Tier_0 = 40,
            Tier_1 = 35,
            Tier_2 = 28,
            Tier_3 = 22,
            Tier_4 = 16
        }
        public enum SubTypes
        {
            Undead,
            ShapeShifter,
            Unholy,
            Fairy
        }
        
        public SubTypes SubType { get; set; }
        public bool IsRevealed { get; set; }
        public bool IsActive { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int CountDown { get; set; } // determines how long a monster stays hidden
        public int MaxCountDown { get; set; } // used to limit how long a monster can stay hidden
        public bool isStunned { get; set; }
        
        //List of Abilities a monster is capable of using
        private List<AbilityType> _abilities = new List<AbilityType>();
        public List<AbilityType> Abilities { get { return _abilities; } set { value = _abilities; } }
        //Necessary clues required to find the identity of the monster

        public List<Clue.Type> _MonsterClues = new List<Clue.Type>();
        public List<Clue.Type> MonsterClues { get {return _MonsterClues; } set { value = _MonsterClues; } }
        public int CastSpeed { get; set; }
        public Monster()
        {
            this.CountDown = 3;
            this.MaxCountDown = 3;
            this.isStunned = false;
            
        }
        public void Move(List<Tile.Places> _list,List< Player> Players) // moves the monster
        {
            Random r = new Random();
            List < Tile.Places > _tempList = new List<Tile.Places>();
            foreach (var path in _list)
                _tempList.Add(path);
            foreach (var path in _list)
            {
                foreach (Player player in Players) // Ensures the monster moves away from the players
                {
                    if (player.Position.ToString() == path.ToString())
                        _tempList.Remove(path);
                }
            }
            int monsterMoveChoice = r.Next(_tempList.Count);
            Position = _tempList[monsterMoveChoice];
        }
        public AbilityType SelectAbility()
            //selects an ability that the monster will use
        {
            if (CastSpeed > 100)
            {
                Random r = new Random();
                AbilityType result;
                result = Abilities[r.Next(Abilities.Count)];
                CastSpeed -= 100;
                return result;
            }
            else return AbilityType.None;
            
        }
        public void UseAbility(Board board, List<Player> players, GameMaster gm)
            //Enacts the ability
        {
            List<Tile.Places> adjacent = board.GetAdjacentTiles(Position);
            switch (SelectAbility())
            {
                case AbilityType.ExtremePanic:
                    MonsterAbilities.ExtremePanic(board, this);
                    CastSpeed += (int)AbilityTier.Tier_2;
                    break;
                case AbilityType.ExtremeSpeed:
                    MonsterAbilities.ExtremeSpeed(board, this, players);
                    CastSpeed += (int)AbilityTier.Tier_1;
                    break;
                case AbilityType.Vampirism:
                    MonsterAbilities.Vampirism(board,this,players);
                    CastSpeed += (int)AbilityTier.Tier_3;
                    break;
                case AbilityType.SummonBats:
                    MonsterAbilities.SummonBats(board, gm);
                    CastSpeed += (int)AbilityTier.Tier_4;

                    break;
                case AbilityType.None: //default value, ensures no bugs
                    CastSpeed += (int)AbilityTier.Tier_0;
                    break;
            }
            
        }
        
    }
}
