using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Supernatural
{
    public class SupernaturalGame : Game
    {
        public override void Play()
        {
            Tile tile = new Tile();
            GameMaster gm = new GameMaster();
            Monster monster = new Monster();
            Random r = new Random();
            monster.Position = (Tile.Name)(r.Next(9) + 1);
            monster.IsActive = true;
            bool playing = false;
            while (!playing)
            {
                
                foreach (Player player in Players)
                {
                    string canAttack = "";
                    bool endTurn = false;
                    while (!endTurn)
                    {
                        bool playercanAttack = player.CanAttack(monster.Position, tile.GetAdjacentPaths(player.Position));
                        if (playercanAttack)
                            canAttack = "(A)ttack";
                        Console.WriteLine("\n{0}'s turn (M)ove (R)esearch (I)nvestigate {1}", player.Name, canAttack);
                        Console.WriteLine("{0} is located at {1}", player.Name, player.Position);
                        foreach (Clue clue in player.ClueHand)
                            Console.WriteLine("{0}", clue.Name);
                        Console.WriteLine("The Panic at {0} is at {1}", player.Position, tile.GetPanic(player.Position));
                        if (monster.IsActive)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                            Console.WriteLine("It sounds like something may be happening at {0}", monster.Position);
                            Console.BackgroundColor = ConsoleColor.Black;
                        }
                        string query = Console.ReadLine();
                        switch (query.ToLower())
                        {
                            case "m":
                                int count = 0;
                                List<Tile.Name> tempPath = new List<Tile.Name>();
                                tempPath = tile.GetAdjacentPaths(player.Position);
                                foreach (Tile.Name item in tempPath)
                                {
                                    Console.Write("{0}) {1}\n", count + 1, item.ToString());
                                    count += 1;
                                }
                                Int32.TryParse(Console.ReadLine(), out int query2);
                                switch (query2)
                                {
                                    case 1:
                                        player.Position = tempPath[0];
                                        Console.WriteLine("{0} moved to {1}", player.Name, player.Position);
                                        endTurn = true;
                                        break;
                                    case 2:
                                        player.Position = tempPath[1];
                                        Console.WriteLine("{0} moved to {1}", player.Name, player.Position);
                                        endTurn = true;
                                        break;
                                    case 3:
                                        if (tempPath.Count > 2)
                                            player.Position = tempPath[2];
                                        Console.WriteLine("{0} moved to {1}", player.Name, player.Position);
                                        endTurn = true;
                                        break;
                                }
                                break;
                            case "a":
                                if (playercanAttack && monster.IsActive)
                                {
                                    monster.Health -= 1;
                                    endTurn = true;
                                    break;
                                }
                                break;
                            case "r":
                                Console.WriteLine("Which Clue do you wish to research?");
                                count = 1;
                                foreach (Clue clue in player.ClueHand)
                                {
                                    Console.Write("{0}) {1} ", count, clue.Name.ToString());
                                    count += 1;
                                }
                                Int32.TryParse(Console.ReadLine(), out query2);
                                if (query2 <= player.ClueHand.Count)
                                {
                                    Console.WriteLine("{0} Researched {1}.", player.Name, player.ClueHand[query2-1]);
                                    player.ClueHand.RemoveAt(query2-1);
                                    endTurn = true;
                                }
                                break;
                            case "i":
                                Console.WriteLine("{0} is at {1}", player.Position, tile.GetPanic(player.Position));
                                Console.WriteLine("Do you want to search for clues? (y/n) (level_1 and above)");
                                string query3 = Console.ReadLine().ToLower();
                                if (query3 == "y" && (tile.GetPanic(player.Position) != Tile.Panic.Level_0 ))
                                {
                                    gm.Deal(player.ClueHand);
                                    Console.WriteLine("{0} drew {1}",player.Name, player.ClueHand.Last().Name.ToString());
                                    tile.DecreasePanic(player.Position);
                                    endTurn = true;
                                }
                                else Console.WriteLine("Nothing to Investigate");
                                break;
                        }
                    }
                }
                //......................................................................Monster Turn.............................................................................//
                if (monster.Health <= 0)
                    monster.IsActive = false;
                if (monster.IsActive)
                {
                    List<Tile.Name> tempPaths = tile.GetAdjacentPaths(monster.Position);
                    List<Tile.Name> availablePaths = tile.GetAdjacentPaths(monster.Position);
                    foreach (var path in tempPaths)
                    {
                        foreach (Player player in Players)
                        {
                            if (player.Position.ToString() == path.ToString())
                                availablePaths.Remove(path);
                        }
                    }
                    int monsterMoveChoice = r.Next(tempPaths.Count);
                    monster.Position = tempPaths[monsterMoveChoice];
                    tile.IncreasePanic(monster.Position);
                } else
                {
                    monster.CountDown = r.Next(monster.CountDown);
                    
                    if (monster.CountDown == 0)
                    {
                        monster.IsActive = true;
                        monster.CountDown = monster.MaxCountDown;
                    }
                }
            }
        }
    }
}
