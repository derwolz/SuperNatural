using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Supernatural
{
    public class Player
    {
        public Tile.Places Position { get; set; }
        public string Name { get; set; }
        private List<Clue> _ClueHand = new List<Clue>();
        public List<Clue> ClueHand { get { return _ClueHand; } set { value = _ClueHand; } }
        private List<Action> _ActionHand = new List<Action>();
        public List<Action> ActionHand { get { return _ActionHand; } set { value = _ActionHand; } }
        public ActionDeck Deck = new ActionDeck();
        public List<Action> _Discard = new List<Action>();
        public List<Action> Discard { get { return _Discard; } set { value = _Discard; } }
        public ConsoleColor Color { get; set; }
        public void DrawCard()
        {
            ActionHand.Add(Deck.Actions.First());
            Deck.Actions.RemoveAt(0);
        }
        public void DiscardCard()
        {
            int count = 1;
            Console.WriteLine("Choose a card to Discard");
            
            foreach (Action card in ActionHand)
            {
                Console.Write("{0}). {1}, ", count, card.Name.ToString());
                count += 1;
            }
            Int32.TryParse(Console.ReadLine(), out int query2);
            
            if (query2 == 0)
            {
                Console.WriteLine("Incorrect Input, Discarding first card.");
                query2 = 1;
            }
            Discard.Add(ActionHand[query2-1]);
            ActionHand.RemoveAt(query2 - 1);
        }
        public void DiscardCard(Action card)
        {
            Discard.Add(card);
            ActionHand.Remove(card);
            Shuffle(1);
        }
        public void Reshuffle()
        {
            Random r = new Random();
            for (int i = 0; i < Discard.Count; i++)
            {
                int _temp = r.Next(Discard.Count - 1);
                Deck.Actions.Add(Discard[_temp]);
            }
            Shuffle();
        }
        
        public void Shuffle(int times = 1)
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
        public void ResearchClue(int cost, GameMaster gm, int times)
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
                    Console.WriteLine("{0} Researched {1} {2}.", Name, ClueHand[query2 - 1].Name, Isreal);
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
        public void Damage(Monster monster, int damage)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("{0} Takes a shot with the shotgun and strikes the figure dealing {1} damage.", Name, damage);
            monster.Health -= damage;
            if (monster.Health < 0)
            {
                Console.WriteLine("The {0} Falls", monster.Name);
                monster.IsActive = false;
            }
            Console.ResetColor();
            
        }
        public void Move(Board board, int cost, int times = 1)
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
    }
}
