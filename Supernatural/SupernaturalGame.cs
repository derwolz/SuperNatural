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
            
            Monster monster = new Monster();
            MonsterFactory factory = new MonsterFactory();
            Random random = new Random();
            int rand = random.Next(1,2)-1;
            Monster.Type mType = (Monster.Type)rand;
            monster = factory.Factory(mType, Players);
            GameMaster gm = new GameMaster(monster);
            Random r = new Random();
            monster.Position = (Tile.Name)(r.Next(9) + 1);
            monster.IsActive = true;
            bool playing = false;
            foreach (Player player in Players)
                player.Shuffle(3);
            gm.Shuffle(3);

            while (!playing)
            {
                
                foreach (Player player in Players)
                {
                    string canAttack = "";
                    string canResearch = "";
                    string canInvestigate = "";
                    string monsterName = "";
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
                        if (monster.IsRevealed) monsterName = "" + monster.Name + " is rampaging at the ";
                        else monsterName = "It sounds like something may be";

                        Console.WriteLine("\n{0}'s turn (M)ove {1} {2} {3} (S)kip (U)se Card", player.Name, canInvestigate, canResearch, canAttack);
                        Console.WriteLine("{0} is located at {1}", player.Name, player.Position);
                        foreach (Clue clue in player.ClueHand)
                            Console.WriteLine("{0}", clue.Name);
                        Console.WriteLine("The Panic at {0} is at {1}", player.Position, tile.GetPanic(player.Position));
                        if (monster.IsActive)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                            Console.WriteLine("{0} happening at {1}", monsterName, monster.Position);
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
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("{0} Attacks the Figure, Damage Total {1}", player.Name,monster.MaxHealth-monster.Health);
                                    monster.Health -= 1;
                                    if (monster.Health < 0)
                                    {
                                        Console.WriteLine("The Monster Falls");  
                                        monster.IsActive = false;
                                    }
                                    Console.ResetColor();
                                    player.DiscardCard();
                                    break;
                                }
                                break;
                            case "r":
                                cost = 1;
                                player.ResearchClue(cost, gm);
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
                                    case Action.Type.Rifle:
                                        if (WeaponAbilities.Rifle(tile, player, monster))
                                        {
                                            player.Damage(monster, WeaponAbilities.RifleDamage);
                                            player.DiscardCard(query4);
                                        }
                                        break;
                                    case Action.Type.Shotgun:
                                        if (player.Position == monster.Position)
                                        {
                                            player.Damage(monster, WeaponAbilities.ShotgunDamage);
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
                                            player.ResearchClue(cost, gm);
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
                        gm.RevealMonster(monster);
                        if (gm.CheckLoseCon(tile))
                        {
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("{0} Has destroyed the City! You Have Failed!", monster.Name);
                            Console.WriteLine("Press enter to exit");
                            Console.ReadLine();
                            endTurn = true;
                            playing = true;
                            endProgram = true;
                            return;
                        }
                        if (gm.CheckWinCon(monster, gm.CheckLoseCon(tile)))
                        {
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.WriteLine("You have slain the {0} and saved the City!", monster.Name);
                            Console.WriteLine("Press enter to exit");
                            Console.ReadLine();
                            endTurn = true;
                            playing = true;
                            endProgram = true;
                            return;
                        }
                            
                        
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
