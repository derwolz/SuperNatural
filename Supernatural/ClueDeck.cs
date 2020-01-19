using System;
using System.Collections.Generic;
using System.Text;

namespace Supernatural
{
    class ClueDeck
    {
        private List<Clue> _Clues = new List<Clue>();
        public List<Clue> Clues { get { return _Clues; } set { value = _Clues; } }
        public ClueDeck()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Clue clue = new Clue();
                    clue.Name = (Clue.Type)j;
                    Clues.Add(clue);
                }
            }
        }
        
    }
}
