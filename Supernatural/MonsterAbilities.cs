using System;
using System.Collections.Generic;
using System.Text;

namespace Supernatural
{
    public static class MonsterAbilities
    {
        public static void SummonBats(List<Tile.Name> tiles, GameMaster gm)
        {
            foreach (Tile.Name tile in tiles)
            {
                Monster monster = new Monster();
                monster.Name = "Bat";
                monster.Health = 2;
                monster.Speed = 0;
                monster.IsActive = true;
                monster.IsRevealed = true;
                monster.Ability1 = Monster.AbilityType.None;
                monster.Ability2 = Monster.AbilityType.None;
                gm.Monsters.Add(monster);
            }
        }
    }
}
