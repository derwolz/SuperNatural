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
            
            Monster Initialmonster = new Monster();
            MonsterFactory factory = new MonsterFactory();
            Random random = new Random();
            int rand = random.Next(2);
            Monster.Type mType = (Monster.Type)rand;
            Initialmonster = factory.Factory(mType, Players);
            GameMaster gm = new GameMaster(Initialmonster);
            Random r = new Random();
            gm.Monsters[0].Position = (Tile.Name)(r.Next(18) + 1);
            gm.Monsters[0].IsActive = true;
            
            
            
            bool playing = false;
            foreach (Player player in Players)
                player.Shuffle(3);
            gm.Shuffle(3);

            while (!playing)
            {
                
                foreach (Player player in Players)
                {
                    //.................................................................................................................................................///
                    //................................................................TurnInitialization...............................................................///
                    //.................................................................................................................................................///
                    string canAttack = "";
                    string canResearch = "";
                    string canInvestigate = "";
                    string monsterName = "";
                    bool playercanAttack = false;
                    int cost = 1;
                    bool endTurn = false;
                    int query2 = 0;
                    if (player.Deck.Actions.Count <= 4)
                        player.Reshuffle();
                    for (int handsize = player.ActionHand.Count; handsize <= 3; handsize++)
                        player.DrawCard();
                    while (!endTurn)//.......................................................................Turn Start..................................................//
                    
                    {
                        foreach (Monster monster in gm.Monsters)//.............................................Display Start.............................................//
                        {
                            playercanAttack = player.CanAttack(monster.Position, tile.GetAdjacentPaths(player.Position));
                            if (playercanAttack && monster.IsActive) canAttack = "(A)ttack";
                        }
                        bool playercanResearch = (player.Position == Tile.Name.Library || player.Position == Tile.Name.Motel || player.Position == Tile.Name.TrailerHome);
                        bool playercanInvestigate = (tile.GetPanic(player.Position) != Tile.Panic.Level_0);
                        if (playercanResearch) canResearch = "(R)esearch";
                        if (playercanInvestigate) canInvestigate = "(I)nvestigate";
                        Console.ForegroundColor = player.Color;
                        Console.Write("\n"+player.Name);
                        Console.ResetColor();
                        Console.WriteLine("'s turn (M)ove {0} {1} {2} (S)kip (U)se Card", canInvestigate, canResearch, canAttack);//.........Display Player Choices.......//
                        Console.WriteLine("{0} is located at {1}", player.Name, player.Position);
                        foreach (Clue clue in player.ClueHand)
                            Console.WriteLine("{0}", clue.Name);
                        Console.WriteLine("The Panic at {0} is at {1}", player.Position, tile.GetPanic(player.Position));//..................Show Current Panic..........//
                        foreach (Monster monster in gm.Monsters)
                        {
                            if (monster.IsRevealed) monsterName = "" + monster.Name + " is rampaging at the ";
                            else monsterName = "It sounds like something may be happening at the ";

                            if (monster.IsActive)
                            {
                                Console.BackgroundColor = ConsoleColor.DarkBlue;
                                Console.WriteLine("{0} {1}", monsterName, monster.Position);//.....................................Show Current Monster Position..........//
                                Console.BackgroundColor = ConsoleColor.Black;
                            }
                        }
                        //................................................................................................................................................//
                        //................................................................Player Choice...................................................................//
                        //................................................................................................................................................//
                        string query = Console.ReadLine();
                        switch (query.ToLower())
                        {
                            case "m"://.........................................................................Movement..................................//
                                int count = 0;
                                cost = 1;
                                player.Move(tile, cost);
                                break;
                            case "a"://..........................................................................Attack....................................//
                                Console.WriteLine("Attack which monster?");
                                gm.DisplayMonsters();
                                Int32.TryParse(Console.ReadLine(), out query2);
                                {
                                    if (query2 == 0) query2 = 1;
                                    Monster monster = gm.Monsters[query2-1];
                                    player.Damage(monster, 1);
                                    if (monster.Health < 0)
                                    {
                                        Console.WriteLine("The {0} Falls", monsterName);
                                        monster.IsActive = false;
                                    }
                                    Console.ResetColor();
                                    player.DiscardCard();
                                    break;
                                }
                            case "r"://............................................................................Research..............................//
                                cost = 1;
                                player.ResearchClue(cost, gm);
                                break;
                            case "i"://.............................................................................Investigate..........................//
                                Console.WriteLine("{0} is at {1}", player.Position, tile.GetPanic(player.Position));
                                Console.WriteLine("Do you want to search for clues? (y/n) (level_1 and above)");
                                string query3 = Console.ReadLine().ToLower();
                                if (query3 == "y" && (tile.GetPanic(player.Position) != Tile.Panic.Level_0 ))
                                {
                                    gm.Deal(player.ClueHand);
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine("{0} drew {1}",player.Name, player.ClueHand.Last().Name.ToString());
                                    Console.ResetColor();
                                    tile.DecreasePanic(player.Position);
                                    player.DiscardCard();
                                }
                                else Console.WriteLine("Nothing to Investigate");
                                break;
                            case "u"://.............................................................Use A card..........................................//
                                count = 1;
                                foreach (Action card in player.ActionHand)
                                {
                                    Console.Write("{0}). {1} ", count, card.Name.ToString());
                                    count += 1;
                                }
                                Int32.TryParse(Console.ReadLine(), out query2);
                                if (query2 == 0) break;
                                Action query4 = player.ActionHand[query2 - 1];
                                Monster selectedMonster = null;
                                switch (query4.Name)
                                {
                                    case Action.Type.Rifle://.....................................Rifle.................................................//
                                        gm.DisplayMonsters();
                                        Int32.TryParse(Console.ReadLine(), out query2);
                                        if (query2 == 0)      query2 = 1;
                                        selectedMonster = gm.Monsters[query2 - 1];
                                        if (WeaponAbilities.Rifle(tile, player, selectedMonster))
                                        {
                                            player.Damage(selectedMonster, WeaponAbilities.RifleDamage);
                                            player.DiscardCard(query4);
                                        }
                                        break;
                                    case Action.Type.Shotgun://..................................Shotgun...............................................//
                                        gm.DisplayMonsters();
                                        Int32.TryParse(Console.ReadLine(), out query2);
                                        if (query2 == 0)      query2 = 1;
                                            selectedMonster = gm.Monsters[query2 - 1];
                                            if (player.Position == selectedMonster.Position)
                                            {
                                                player.Damage(selectedMonster, WeaponAbilities.ShotgunDamage);
                                                player.DiscardCard(query4);
                                            }
                                            break;
                                        
                                    case Action.Type.doubleMove://..............................Double Movement..........................................//
                                        for (int i = 0; i < 2; i++)
                                        {
                                            Console.WriteLine("{0} sprints",player.Name);
                                            cost = 0;
                                            player.Move(tile, cost);
                                        }
                                        player.DiscardCard(query4);
                                        break;
                                    case Action.Type.doubleInvestigate://......................Double Investigate.......................................//
                                        for (int i = 0; i < 2; i++)
                                            if (tile.GetPanic(player.Position) != Tile.Panic.Level_0)
                                            {
                                                gm.Deal(player.ClueHand);
                                                Console.WriteLine("{0} drew {1}", player.Name, player.ClueHand.Last().Name.ToString());
                                                tile.DecreasePanic(player.Position);
                                            }
                                        break;
                                    case Action.Type.doubleResearch://.............................Double Research......................................//
                                        cost = 0;
                                        for (int i = 0; i < 2; i++)
                                            player.ResearchClue(cost, gm);
                                        player.DiscardCard(query4);
                                        break;
                                }    
                                break;
                            case "s"://....................................................................Skip Turn..............................................//
                                endTurn = true;
                                break;
                            default: break;
                        }
                        if (player.ActionHand.Count == 0)
                            endTurn = true;
                        //.........................................................................................................................................//
                        //......................................................End Player Turn Sequence...........................................................//
                        //.........................................................................................................................................//
                        foreach (Monster monster in gm.Monsters)
                        {
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
                }
                //...............................................................................................................................................................//
                //......................................................................Monster Turn.............................................................................//
                //...............................................................................................................................................................//
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\nEnemy Turn");
                foreach (Monster monster in gm.Monsters)
                {
                    if (monster.Health <= 0)
                    {
                        monster.IsActive = false;
                        if (monster != gm.Monsters.First())
                            gm.Monsters.Remove(monster);
                    }
                        
                    if (monster.IsActive)
                    {
                        for (int move = monster.Speed; move > 0; move--)
                        {
                            monster.Move(tile.GetAdjacentPaths(monster.Position), Players);
                            tile.IncreasePanic(monster.Position);
                            Console.WriteLine("A commotion is heard coming from {0}", monster.Position);
                        }
                        Console.ResetColor();
                        if (monster.IsRevealed)
                        {
                            monster.UseAbility(tile, Players, gm);
                        }
                    }
                    else
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
}
