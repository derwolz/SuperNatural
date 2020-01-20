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
            
            Monsters.Add(monster);
            
        }
        private List<Monster> _Monsters = new List<Monster>();
        public List<Monster> Monsters { get { return _Monsters; } set { value = _Monsters; } }
        private List<Monster> _Summoned = new List<Monster>();
        public List<Monster> Summoned { get { return _Summoned; } set { value = _Summoned; } }
        public List<Monster> Dead { get { return _Monsters; } set { value = _Monsters; } }
        public bool CheckWinCon(Monster monster, bool LoseCon)
        {
            if (monster.Health < 0 && monster.IsRevealed == true && !LoseCon)
            {
                return true;
            }
            else return false;
        }
        public bool CheckLoseCon(Board board)
        {
            int count = 0;
            foreach (Tile tile in board.tiles)
                if (board.GetPanic(tile.Name) == Tile.Panic.Level_4)
                    count += 1;
            if (count > (board.tiles.Count / 2)) return true;
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
            foreach (Clue card in _tempList)
                clueDeck.Clues.Add(card);
            return;
        }
        public void DisplayMonsters()
        {
            int count = 0;
            foreach (Monster monster in Monsters)
            {
                count += 1;
                string monsterName = "";
                if (monster.IsRevealed == false) monsterName = "Monster";
                else monsterName = monster.Name;
                Console.WriteLine("\n{0}) {1} is at {2}", count, monsterName, monster.Position);
            }
        }
    }
}
