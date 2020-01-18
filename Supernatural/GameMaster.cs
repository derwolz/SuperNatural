using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Supernatural
{
    class GameMaster
    {
        ClueDeck clueDeck = new ClueDeck();
        public GameMaster()
        {
            
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
