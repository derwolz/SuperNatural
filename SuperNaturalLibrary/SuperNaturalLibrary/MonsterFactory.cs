using System;
using System.Collections.Generic;
using System.Text;

namespace SupernaturalLibrary
{
    public class MonsterFactory
    {
        //each Monster has its own list of clues assigned to it here
        public List<Clue.Type> WerewolfClues = new List<Clue.Type>() 
        { Clue.Type.Blood, Clue.Type.Hair, Clue.Type.Bones, Clue.Type.Claw };
        public List<Clue.Type> VampireClues = new List<Clue.Type>() 
        { Clue.Type.Blood, Clue.Type.Arcane_Symbols, Clue.Type.Cold_Air, Clue.Type.Broken_Holy_Symbols };
        public List<Clue.Type> BansheeClues = new List<Clue.Type>() 
        { Clue.Type.Blood, Clue.Type.Arcane_Symbols, Clue.Type.Cold_Air, Clue.Type.Broken_Holy_Symbols };
        public List<Clue.Type> DoppelGangerClues = new List<Clue.Type>() 
        { Clue.Type.Blood, Clue.Type.Arcane_Symbols, Clue.Type.Cold_Air, Clue.Type.Broken_Holy_Symbols };
        public List<Clue.Type> GhostClues = new List<Clue.Type>() 
        { Clue.Type.Blood, Clue.Type.Arcane_Symbols, Clue.Type.Cold_Air, Clue.Type.Broken_Holy_Symbols };
        public List<Clue.Type> GhoulClues = new List<Clue.Type>() 
        { Clue.Type.Blood, Clue.Type.Arcane_Symbols, Clue.Type.Cold_Air, Clue.Type.Broken_Holy_Symbols };
        public List<Clue.Type> WendigoClues = new List<Clue.Type>() 
        { Clue.Type.Blood, Clue.Type.Arcane_Symbols, Clue.Type.Cold_Air, Clue.Type.Broken_Holy_Symbols };
        public Monster Factory(Monster.Type name, List<Player> players = null)
        {
            Monster monster = new Monster();
            monster.MaxHealth = 0;
            switch (name)
            {
                case Monster.Type.Vampire:
                    foreach (var clue in VampireClues) // the clues are added to the monster here for clue deck checking
                        monster.MonsterClues.Add(clue);
                    foreach (Player player in players)
                        monster.MaxHealth += 8; // Variable health to ensure Difficulty
                    monster.Speed = 2;
                    monster.Name = "Vampire";
                    monster.Abilities.Add(Monster.AbilityType.SummonBats);//monster abilities added here
                    monster.Abilities.Add(Monster.AbilityType.Vampirism);
                    
                    break;
                case Monster.Type.WereWolf:
                    foreach (var clue in WerewolfClues)
                        monster.MonsterClues.Add(clue);
                    foreach (Player player in players)
                        monster.MaxHealth += 10;
                    monster.Speed = 3;
                    monster.Name = "WereWolf";
                    monster.Abilities.Add(Monster.AbilityType.ExtremePanic);
                    monster.Abilities.Add(Monster.AbilityType.ExtremeSpeed);
                    break;
                case Monster.Type.Banshees://Monsters here and below have no abilities currently
                    foreach (var clue in BansheeClues)
                        monster.MonsterClues.Add(clue);
                    foreach (Player player in players)
                        monster.MaxHealth += 4;
                    monster.Speed = 2;
                    monster.Name = "Banshee";
                    break;
                case Monster.Type.DoppelGanger:
                    foreach (var clue in DoppelGangerClues)
                        monster.MonsterClues.Add(clue);
                    foreach (Player player in players)
                        monster.MaxHealth += 6;
                    monster.Speed = 2;
                    monster.Name = "DoppelGanger";
                    break;
                case Monster.Type.Ghosts:
                    foreach (var clue in GhostClues)
                        monster.MonsterClues.Add(clue);
                    foreach (Player player in players)
                        monster.MaxHealth += 5;
                    monster.Speed = 3;
                    monster.Name = "Ghost";
                    break;
                case Monster.Type.Ghoul:
                    foreach (var clue in GhoulClues)
                        monster.MonsterClues.Add(clue);
                    foreach (Player player in players)
                        monster.MaxHealth += 6;
                    monster.Speed = 3;
                    monster.Name = "Ghoul";
                    break;
                case Monster.Type.Wendigos:
                    foreach (var clue in WendigoClues)
                        monster.MonsterClues.Add(clue);
                    foreach (Player player in players)
                        monster.MaxHealth += 6;
                    monster.Speed = 4;
                    monster.Name = "Wendigo";
                    break;
                case Monster.Type.Bat:
                    monster.Name = "Bat";
                    monster.MaxHealth = 1;
                    monster.Speed = 1;
                    monster.IsActive = true;
                    monster.IsRevealed = true;
                    monster.Abilities.Add(Monster.AbilityType.None);
                    
                    break;

            }
            if (players != null && players.Count > 2)//Variable speed to ensure difficulty
                monster.Speed += 1;
            monster.Health = monster.MaxHealth;
            return monster;
        }
    }
}
