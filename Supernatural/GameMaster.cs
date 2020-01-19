using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Supernatural
{
    public class GameMaster
    {
        public ClueDeck clueDeck { get; set; }
        public bool IsMonsterRevealed {get; set;}
        private List<Clue> _WinCon1 = new List<Clue>();
        public List<Clue> WinCon1 { get { return _WinCon1; } set { value = _WinCon1; } }
        public GameMaster(Monster monster)
        {
            this.clueDeck = new ClueDeck(monster);
            IsMonsterRevealed = false;
            
        }
        public bool CheckWinCon(Monster monster, bool LoseCon)
        {
            if (monster.Health < 0 && monster.IsRevealed == true && !LoseCon)
            {
                return true;
            }
            else return false;
        }
        public bool CheckLoseCon (Tile tile)
        {
            int count = 0;
            foreach (var key in tile.AdjacentPathA.Keys)
            {
                if (tile.GetPanic(key) == Tile.Panic.Level_4)
                    count += 1;
            }
            foreach (var key in tile.AdjacentPathB.Keys)
            {
                if (tile.GetPanic(key) == Tile.Panic.Level_4)
                    count += 1;
            }
            foreach (var key in tile.AdjacentPathC.Keys)
            {
                if (tile.GetPanic(key) == Tile.Panic.Level_4)
                    count += 1;
            }
            foreach (var key in tile.AdjacentPathD.Keys)
            {
                if (tile.GetPanic(key) == Tile.Panic.Level_4)
                    count += 1;
            }
            if (count > (tile.AdjacentPathA.Count + tile.AdjacentPathB.Count + tile.AdjacentPathC.Count + tile.AdjacentPathD.Count) / 2)
                return true;
            else return false;
        }
        public void RevealMonster(Monster monster)
        {
            int TrueCheck = 0;
            foreach (Clue clue in WinCon1)
            {
                foreach (Clue.Type clue2 in monster.MonsterClues)
                    if (clue.Name == clue2)
                        TrueCheck += 1;

            }
            if (TrueCheck == monster.MonsterClues.Count)
            {
                monster.IsRevealed = true;
                if (monster.IsRevealed == true)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("The Mysterious Figure is revealed to be a {0}", monster.Name);
                    Console.ResetColor();
                    monster.MaxHealth *= 2;
                    monster.Health = monster.MaxHealth;
                }
                    
            }
                
        }

        public void Deal(List<Clue> Hand)
        {
            Hand.Add(clueDeck.Clues.First());
            clueDeck.Clues.RemoveAt(0);
        }

        public void Shuffle(int times = 1)
        {
            Random r = new Random();
            List<Clue> _tempList = new List<Clue>();
            for (int j = 0; j < times; j++)
                for (int i = 0; i < clueDeck.Clues.Count; i++)
                {
                    int x = r.Next(clueDeck.Clues.Count);
                    _tempList.Add(clueDeck.Clues[x]);
                    clueDeck.Clues.RemoveAt(x);
                }
            return;
        }
    }
}
