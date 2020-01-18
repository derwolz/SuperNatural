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
        public void DiscardCard(Action card)
        {
            ActionHand.Remove(card);
            Discard.Add(card);
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
    }
}
