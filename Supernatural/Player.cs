using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Supernatural
{
    public class Player : Actor
    {
        //...................................................................................
        //...............................Start Player Hands..................................
        //...................................................................................
        private List<Clue> _ClueHand = new List<Clue>();
        public List<Clue> ClueHand { get { return _ClueHand; } set { value = _ClueHand; } }
        private List<Action> _ActionHand = new List<Action>();
        public List<Action> ActionHand { get { return _ActionHand; } set { value = _ActionHand; } }
        private List<Weapon.WeaponParts> _WeaponHand = new List<Weapon.WeaponParts>();
        public List<Weapon.WeaponParts> WeaponHand { get { return _WeaponHand; } set { value = _WeaponHand; } }
        private List<Weapon> _Weapons = new List<Weapon>();
        public List<Weapon> Weapons { get { return _Weapons; } set { value = _Weapons; } }
        //............................Action Deck to store Action Cards......................
        //............Action Cards determine how many actions a player may take in a round...
        public ActionDeck Deck = new ActionDeck();
        //.............Discard for temporary Card storage to remove them from play...........
        public List<Action> _Discard = new List<Action>();
        public List<Action> Discard { get { return _Discard; } set { value = _Discard; } }
        public ConsoleColor Color { get; set; } //Player Color on the Console................
        //.............................Begin Functions.......................................
        public void DrawCard() //....................Draw action cards.......................
        {
            ActionHand.Add(Deck.Actions.First()); 
            Deck.Actions.RemoveAt(0);
        }
        public void DiscardCard() //...............Discard Action cards......................
        {
            int count = 1;
            Console.WriteLine("Choose a card to Discard");
            
            foreach (Action card in ActionHand)
            {
                Console.Write("{0}). {1}, ", count, card.Name.ToString());
                count += 1;
            }
            Int32.TryParse(Console.ReadLine(), out int query2);
            //Instead of backing out, which is difficult, it assumes the first card will be dropped
            if (query2 < 1 || query2 > ActionHand.Count) query2 = 1;
            Discard.Add(ActionHand[query2-1]);
            ActionHand.RemoveAt(query2 - 1);
        }
        public void DiscardCard(Action card) //base method to discard card....................
        {
            Discard.Add(card);
            ActionHand.Remove(card);
            Shuffle(1);
        }
        public void Reshuffle() //Reshuffles Action Card deck by adding discard to it........
        {
            Random r = new Random();
            for (int i = 0; i < Discard.Count; i++)
            {
                int _temp = r.Next(Discard.Count - 1);
                Deck.Actions.Add(Discard[_temp]);
            }
            Shuffle();
        }
        
        public void Shuffle(int times = 1) //Shuffles the action deck........................
        {
            Random r = new Random();
            List<Action> _tempList = new List<Action>();
            for (int j = 0; j < times; j++)
                for (int i = 0; i < Deck.Actions.Count; i++)
                {
                    int x = r.Next(Deck.Actions.Count);
                    _tempList.Add(Deck.Actions[x]);
                    Deck.Actions.RemoveAt(x);
                }
            foreach (Action card in _tempList)
                Deck.Actions.Add(card);
            return;
        }
        public bool CanAttack(Tile.Places _tile, List<Tile.Places> tiles)
            // outdated ranging method Use GameActions.Target() instead
        {
            bool result = false;
            foreach (var tile in tiles)
            {
                if (tile.ToString() == _tile.ToString())
                    result = true;
            }
            if (Position == _tile)
                result = true;
            return result;
        }
        public void InvestigateClue(int cost, GameMaster gm, int times)
            //removes clue from players hand and adds it to the list of found cluese
        {
            for (int i = 0; i < times; i++)
            {
                Console.WriteLine("Which Clue do you wish to research?");
                int count = 1;
                foreach (Clue clue in ClueHand)
                {
                    Console.Write("{0}) {1} ", count, clue.Name.ToString());
                    count += 1;
                }
                Int32.TryParse(Console.ReadLine(), out int query2);
                if (query2 == 0 || query2 > ClueHand.Count) return;
                if (query2 <= ClueHand.Count)
                {
                    string Isreal = "";
                    if (ClueHand[query2 - 1].IsReal) Isreal = "Was a Real Clue!";
                    else Isreal = "Was a fake!";
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("{0} Investigateed {1} {2}.", Name, ClueHand[query2 - 1].Name, Isreal);
                    Console.ResetColor();
                    if (ClueHand[query2 - 1].IsReal == true)
                        gm.WinCon1.Add(ClueHand[query2 - 1]);
                    ClueHand.RemoveAt(query2 - 1);
                    if (cost > 0)
                        for (int j = 0; j < cost; j++)
                            DiscardCard();
                }
            }
            return;
        }
        public void Move(Board board, int cost, int times = 1) 
            
            // Move a player This action is shared by the monster in theory, but the ability for the player to choose is why it is seperate
        {
            for (int i = 0; i < times; i++)
            {
                List<Tile.Places> tempPath = new List<Tile.Places>();
                tempPath = board.GetAdjacentTiles(Position);
                int count = 0;
                foreach (Tile.Places item in tempPath)
                {
                    Console.Write("{0}) {1}\n", count + 1, item.ToString());
                    count += 1;
                }
                if (Int32.TryParse(Console.ReadLine(), out int query2) && query2 > 0 && query2 < 5)
                {
                    Position = tempPath[query2 - 1];
                    Console.WriteLine("{0} moved to {1}", Name, Position);
                    if (cost > 0)
                        for (int j = 0; j < cost; j++)
                            DiscardCard();
                }
                else
                {
                    Console.WriteLine("Not correct input.");
                }
            }
                return;
        }
        public void ConsumeWeapon(Weapon.WeaponName weaponName)
            //decreases Uses until weapon no longer has any, then removes the weapon
        {
            Weapon _temp = null;
            foreach(var weapon in Weapons)
            {
                if (weapon.Name == weaponName)
                    if (weapon.Uses > 0)
                    {
                        weapon.Uses -= 1;
                        Console.WriteLine("{0} has {1} uses left", weapon.Name, weapon.Uses);
                    }
                    else
                    {
                        Console.WriteLine("{0} is used up", weapon.Name);
                        _temp = weapon;
                    }
            }
            if (_temp != null)
            {
                Weapons.Remove(_temp);
            }
        }
    }
}
