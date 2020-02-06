using System;
using System.Collections.Generic;
using System.Text;

namespace Supernatural
{
    public static class MonsterAbilities
    {
        /// <summary>
        /// These are abilities used by monsters, it is stored as static to facilitate free use of these abilities
        /// but it requires that a monster has the enum of the ablity in order to use them.
        /// Stored seperately for ease of reading
        /// </summary>
        public static void SummonBats(Board board, GameMaster gm) //summons up to 4 bats in surrounding areas
        {
            int count = 0;
            List<Monster> result = new List<Monster>();
            Console.ForegroundColor = ConsoleColor.Red;
            foreach (Tile.Places tile in board.GetAdjacentTiles(gm.Monsters[0].Position))
            {
                
                Monster monster = new Monster();
                monster.Name = "Bat";
                monster.Health = 1;
                monster.Speed = 1;
                monster.IsActive = true;
                monster.IsRevealed = true;
                monster.Abilities.Add(Monster.AbilityType.None);
                monster.Position = tile;
                gm.Summoned.Add(monster);
                count += 1;
            }
            Console.WriteLine("{0} summons {1} hoards of bats", gm.Monsters[0].Name, count);
            Console.ResetColor();
        }
        public static void Vampirism(Board board, Monster monster, List<Player> players) 
            //increases panic for surrounding area and increases the monsters health
        {
            int health = 0;
            Console.ForegroundColor = ConsoleColor.Red;
            List<Tile.Places> names = board.GetAdjacentTiles(monster.Position);
            foreach (Tile.Places name in names)
            {
                board.IncreasePanic(name);
                foreach (Player player in players)
                    health += 2;
            }
            Console.WriteLine("{0} Begins feeding on the Populace increasing his health by {1}", monster.Name, health);
            monster.Health += health;
        }
        public static void ExtremePanic(Board board, Monster monster)
            //Increases panic for surrounding area by 2
        {
            List<Tile.Places> adjacent = board.GetAdjacentTiles(monster.Position);
            foreach (Tile.Places name in adjacent)
            {
                board.IncreasePanic(name);
                board.IncreasePanic(name);
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Bodies are beginning to pile up, Everyone is getting scared");
            Console.ResetColor();
        }
        public static void ExtremeSpeed(Board board, Monster monster, List<Player> players) 
        {
            //increases moves the monster extra times increasing panic along the way
            List<Tile.Places> adjacent = board.GetAdjacentTiles(monster.Position);
        for (int i = 0; i < monster.Speed; i++)
            {
                monster.Move(adjacent, players);
                board.IncreasePanic(monster.Position);
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("{0} Runs through the town at an astounding speed", monster.Name);
            Console.ResetColor();
        }
    }
    
}
