using System;
using System.Collections.Generic;
using System.Text;

namespace Supernatural
{
    public class ClueDeck
    {
        /// <summary>
        /// Instantiates a clue deck adding the necessary clues a monster needs to be found out
        /// </summary>
        private List<Clue> _Clues = new List<Clue>();
        public List<Clue> Clues { get { return _Clues; } set { value = _Clues; } }
        public ClueDeck(Monster monster)
        {
            foreach (Clue.Type _item in monster.MonsterClues)
            {
                Clue clue = new Clue();
                clue.Name = _item;
                clue.IsReal = true;
                Clues.Add(clue);

            }
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Clue clue = new Clue();
                    clue.Name = (Clue.Type)j;
                    clue.IsReal = false;
                    Clues.Add(clue);
                }
            }
        }
        
    }
}
