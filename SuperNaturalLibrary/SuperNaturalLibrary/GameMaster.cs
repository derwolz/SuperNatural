using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SupernaturalLibrary
{
    public class GameMaster
    {
        //public pool for the players to draw from, as opposed to the action decks there will be a deck of
        //research cards instantiated here as well
        public ClueDeck clueDeck { get; set; }
        public WeaponDeck weaponDeck { get; set; }
        //Variabls that determine the phase of the game and whether the players win/lose
        public bool IsMonsterRevealed {get; set;}
        private List<Clue> _WinCon1 = new List<Clue>();
        public List<Clue> WinCon1 { get { return _WinCon1; } set { value = _WinCon1; } }
        public GameMaster(Monster monster)
        {
            this.clueDeck = new ClueDeck(monster);
            this.weaponDeck = new WeaponDeck();
            IsMonsterRevealed = false;
            
            Monsters.Add(monster);
            
        }
        //monsters kept track of here
        private List<Monster> _Monsters = new List<Monster>();
        public List<Monster> Monsters { get { return _Monsters; } set { value = _Monsters; } }
        //summoned monsters is a temporary list that stops summons from moving immediately and 
        //gets around the nasty can't modify a list you are currently iterating through
        private List<Monster> _Summoned = new List<Monster>();
        public List<Monster> Summoned { get { return _Summoned; } set { value = _Summoned; } }
        
        public bool CheckWinCon(Monster monster, bool LoseCon) 
            //player wins if monsters health drops below 0 and they haven't lost
        {
            if (monster.Health < 0 && monster.IsRevealed == true && !LoseCon)
            {
                return true;
            }
            else return false;
        }
        public bool CheckLoseCon(Board board)
            //player loses if Level 4 panic is reached on at least half of the areas
        {
            int count = 0;
            foreach (Tile tile in board.Tiles)
                if (board.GetPanic(tile.Name) == Tile.Panic.Level_4)
                    count += 1;
            if (count > (board.Tiles.Count / 2)) return true;
            else return false;
        }
        public void RevealMonster(Monster monster)
            //This will reveal the monster if the players have gathered enough clues
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
                    WinCon1.RemoveAll(x => x.IsReal == true); // empties win hand, forcing this to only be true once
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("The Mysterious Figure is revealed to be a {0}", monster.Name);
                    Console.ResetColor();
                    monster.MaxHealth *= 2;
                    monster.Health = monster.MaxHealth;
                }
            }
        }

        public void Deal(List<Clue> Hand)
            //Deals a clue Card
        {
            if (clueDeck.Clues.Count >= 0)
            {
                Hand.Add(clueDeck.Clues.First());
                clueDeck.Clues.RemoveAt(0);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("There are no more clues to be found!");
                Console.ResetColor();
            }
                
        }
        public void Deal(List<Weapon.WeaponParts> Hand, int times = 3)
        {
            if (weaponDeck.Deck.Count > 0)
            {
                for (int i = 0; i < times; i++)
                {
                    Console.WriteLine("{0}). {1}", i+1, weaponDeck.Deck.First().ToString());
                    Hand.Add(weaponDeck.Deck.First());
                    weaponDeck.Deck.RemoveAt(0);
                }
                Int32.TryParse(Console.ReadLine(), out int numQuery);
                if (numQuery < 1 || numQuery > times) numQuery = 1;
                Hand.RemoveAt(Hand.Count - times + numQuery - 1);
            }
            else Console.WriteLine("There are no more parts to work with");
        }

        public void Shuffle(int times = 4)
            //Shuffles clue deck -- this and previous need different names for when the Weapon card is here
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
            List<Weapon.WeaponParts> _tempList2 = new List<Weapon.WeaponParts>();
            for (int j = 0; j < times; j++)
                for (int i = 0; i < weaponDeck.Deck.Count; i++)
                {
                    int x = r.Next(weaponDeck.Deck.Count);
                    _tempList2.Add(weaponDeck.Deck[x]);
                    weaponDeck.Deck.RemoveAt(x);
                }
            foreach (Weapon.WeaponParts weapon in _tempList2)
                weaponDeck.Deck.Add(weapon);
            return;
        }
        
    }
}
