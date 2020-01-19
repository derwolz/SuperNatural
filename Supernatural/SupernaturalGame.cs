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
                    string canResearch = "";
                    string canInvestigate = "";
                    int cost = 1;
                    bool endTurn = false;
                    int query2 = 0;
                    if (player.Deck.Actions.Count <= 4)
                        player.Reshuffle();
                    for (int handsize = player.ActionHand.Count; handsize <= 4; handsize++)
                        player.DrawCard();
                    while (!endTurn)
                    {
                        bool playercanAttack = player.CanAttack(monster.Position, tile.GetAdjacentPaths(player.Position));
                        bool playercanResearch = (player.Position == Tile.Name.Library || player.Position == Tile.Name.Motel || player.Position == Tile.Name.TrailerHome);
                        bool playercanInvestigate = (tile.GetPanic(player.Position) != Tile.Panic.Level_0);

                        if (playercanResearch)                          canResearch = "(R)esearch";
                        if (playercanAttack && monster.IsActive)        canAttack = "(A)ttack";
                        if (playercanInvestigate)                       canInvestigate = "(I)nvestigate";
                        
                        Console.WriteLine("\n{0}'s turn (M)ove {1} {2} {3} (S)kip (U)se Card", player.Name, canInvestigate, canResearch, canAttack);
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
                                cost = 1;
                                player.Move(tile, cost);
                                break;
                            case "a":
                                if (playercanAttack && monster.IsActive)
                                {
                                    Console.WriteLine("{0} Attacks the Figure, Damage Total {1}", player.Name,monster.MaxHealth-monster.Health);
                                    monster.Health -= 1;
                                    if (monster.Health < 0)
                                    {
                                        Console.WriteLine("The Monster Falls");
                                        monster.IsActive = false;
                                    }
                                    player.DiscardCard();
                                    break;
                                }
                                break;
                            case "r":
                                cost = 1;
                                player.ResearchClue(cost);
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
                                    player.DiscardCard();
                                   
                                }
                                else Console.WriteLine("Nothing to Investigate");
                                break;
                            case "u":
                                count = 1;
                                foreach (Action card in player.ActionHand)
                                {
                                    Console.Write("{0}). {1} ", count, card.Name.ToString());
                                    count += 1;
                                }
                                Int32.TryParse(Console.ReadLine(), out query2);
                                if (query2 == 0) break;
                                Action query4 = player.ActionHand[query2 - 1];
                                switch (query4.Name)
                                {
                                    case Action.Type.Shotgun:
                                        if (WeaponAbilities.Shotgun(tile, player, monster))
                                        {
                                            Console.WriteLine("{0} Takes a shot with the shotgun and strikes the figure Damage Total: {1}",player.Name,monster.MaxHealth-monster.Health);
                                            monster.Health -= 1;
                                            if (monster.Health < 0)
                                            {
                                                Console.WriteLine("The Monster Falls");
                                                monster.IsActive = false;
                                            }
                                            player.DiscardCard(query4);
                                        }
                                        break;
                                    case Action.Type.doubleMove:
                                        for (int i = 0; i < 2; i++)
                                        {
                                            Console.WriteLine("{0} sprints",player.Name);
                                            cost = 0;
                                            player.Move(tile, cost);
                                        }
                                        player.DiscardCard(query4);
                                        break;
                                    case Action.Type.doubleInvestigate:
                                        for (int i = 0; i < 2; i++)
                                            if (tile.GetPanic(player.Position) != Tile.Panic.Level_0)
                                            {
                                                gm.Deal(player.ClueHand);
                                                Console.WriteLine("{0} drew {1}", player.Name, player.ClueHand.Last().Name.ToString());
                                                tile.DecreasePanic(player.Position);
                                            }
                                        break;
                                    case Action.Type.doubleResearch:
                                        cost = 0;
                                        for (int i = 0; i < 2; i++)
                                            player.ResearchClue(cost);
                                        player.DiscardCard(query4);
                                        break;
                                }    
                                break;
                            case "s":
                                endTurn = true;
                                break;
                            default: break;
                        }
                        if (player.ActionHand.Count == 0)
                            endTurn = true;
                    }
                }
                //......................................................................Monster Turn.............................................................................//
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\nEnemy Turn");
                
                if (monster.Health <= 0)
                    monster.IsActive = false;
                if (monster.IsActive)
                {
                    for (int move = monster.Speed; move > 0; move--)
                    {
                        monster.Move(tile.GetAdjacentPaths(monster.Position), Players);
                        tile.IncreasePanic(monster.Position);
                        Console.WriteLine("A commotion is heard coming from {0}", monster.Position);
                    }
                    Console.ResetColor();

                } else
                {
                    monster.CountDown = r.Next(monster.CountDown);
                    
                    if (monster.CountDown == 0)
                    {
                        monster.IsActive = true;
                        monster.Health = monster.MaxHealth;
                        monster.CountDown = monster.MaxCountDown;
                    }
                    Console.ResetColor();
                }
            }
        }
    }
}
