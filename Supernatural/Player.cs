using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Supernatural
{
    public class Player
    {
        public Tile.Name Position { get; set; }
        public string Name { get; set; }
        private List<Clue> _ClueHand = new List<Clue>();
        public List<Clue> ClueHand { get { return _ClueHand; } set { value = _ClueHand; } }
        private List<Action> _ActionHand = new List<Action>();
        public List<Action> ActionHand { get { return _ActionHand; } set { value = _ActionHand; } }
        public ActionDeck Deck = new ActionDeck();
        public List<Action> _Discard = new List<Action>();
        public List<Action> Discard { get { return _Discard; } set { value = _Discard; } }
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
        }
        public void Reshuffle()
        {
            Random r = new Random();
            for (int i = 0; i < Discard.Count; i++)
            {
                int _temp = r.Next(Discard.Count - 1);
                Deck.Actions.Add(Discard[_temp]);
            }
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
            return;
        }
        public bool CanAttack(Tile.Name _tile, List<Tile.Name> tiles)
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
        public void ResearchClue(int cost, GameMaster gm)
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
                Console.WriteLine("{0} Researched {1} {2}.", Name, ClueHand[query2 - 1].Name,Isreal);
                Console.ResetColor();
                if (ClueHand[query2-1].IsReal == true)
                    gm.WinCon1.Add(ClueHand[query2 - 1]);
                ClueHand.RemoveAt(query2 - 1);
                if (cost > 0)
                    for (int i = 0; i < cost; i++)
                        DiscardCard();
            }
            return;
        }
        public void Move(Tile _tile, int cost)
        {

            List<Tile.Name> tempPath = new List<Tile.Name>();
            tempPath = _tile.GetAdjacentPaths(Position);
            int count = 0;
            foreach (Tile.Name item in tempPath)
            {
                Console.Write("{0}) {1}\n", count + 1, item.ToString());
                count += 1;
            }
            if (Int32.TryParse(Console.ReadLine(), out int query2) && query2 > 0 && query2 < 5)
            {
                Position = tempPath[query2 - 1];
                Console.WriteLine("{0} moved to {1}", Name, Position);
                if (cost > 0)
                    for (int i = 0; i < cost; i++)
                        DiscardCard();
            }
            else
            { 
                Console.WriteLine("Not correct input.");
            } return;
        }
    }
}
