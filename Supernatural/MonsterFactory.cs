using System;
using System.Collections.Generic;
using System.Text;

namespace Supernatural
{
    public class MonsterFactory
    {
        public List<Clue.Type> WerewolfClues = new List<Clue.Type>() { Clue.Type.Blood, Clue.Type.Hair, Clue.Type.Bones, Clue.Type.Claw };
        public List<Clue.Type> VampireClues = new List<Clue.Type>() { Clue.Type.Blood, Clue.Type.Arcane_Symbols, Clue.Type.Cold_Air, Clue.Type.Broken_Holy_Symbols };
        public Monster Factory(Monster.Type name, List<Player> players)
        {
            Monster monster = new Monster();
            monster.MaxHealth = 0;
            switch (name)
            {
                case Monster.Type.Vampire:
                    foreach (var clue in VampireClues)
                        monster.MonsterClues.Add(clue);
                    foreach (Player player in players)
                        monster.MaxHealth += 8;
                    monster.Speed = 3;
                    
                    monster.Name = "Vampire";
                    break;
                case Monster.Type.WereWolf:
                    foreach (var clue in WerewolfClues)
                        monster.MonsterClues.Add(clue);
                    foreach (Player player in players)
                        monster.MaxHealth += 8;
                    monster.Speed = 2;
                    monster.Name = "WereWolf";
                    break;
            }
            if (players.Count > 2)
                monster.Speed += 1;
            return monster;
        }
    }
}
