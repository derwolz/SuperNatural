﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Supernatural
{
    public static class MonsterAbilities
    {
        public static void SummonBats(Board board, GameMaster gm)
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
