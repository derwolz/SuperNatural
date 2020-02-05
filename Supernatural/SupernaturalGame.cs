using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace Supernatural
{
    public class SupernaturalGame : Game
    {
        const int MaxHandSize = 3;
        public override void Play()
        {
            Board board = new Board();
            int boardNum = Enum.GetValues(typeof(Tile.Places)).Length;
            int monsNum = Enum.GetValues(typeof(Monster.Type)).Length;
            Monster Initialmonster = new Monster();
            MonsterFactory factory = new MonsterFactory();
            Random random = new Random();
            int rand = random.Next(monsNum);
            Monster.Type mType = (Monster.Type)rand;
            Initialmonster = factory.Factory(mType, Players);
            GameMaster gm = new GameMaster(Initialmonster);
            Random r = new Random();
            gm.Monsters[0].Position = (Tile.Places)(r.Next(boardNum-Players.Count) + Players.Count);
            gm.Monsters[0].IsActive = true;
            
            
            
            bool playing = false;
            foreach (Player player in Players)
                player.Shuffle();
            //gm.Shuffle();

            while (!playing)
            {
                
                foreach (Player player in Players)
                {
                    //.................................................................................................................................................///
                    //................................................................TurnInitialization...............................................................///
                    //.................................................................................................................................................///
                    string canAttack = "";
                    string monsterName = "";
                    bool playercanAttack = false;
                    int cost = 1;
                    bool endTurn = false;
                    int numQuery = 0;//...................................................Reset Variables.................................................................///
                    if (player.Deck.Actions.Count <= MaxHandSize)
                        player.Reshuffle();
                    for (int handsize = player.ActionHand.Count; handsize <= MaxHandSize; handsize++)
                        player.DrawCard();
                    while (!endTurn)//.......................................................................Turn Start..................................................//
                    
                    {
                        foreach (Monster monster in gm.Monsters)//.............................................Display Start.............................................//
                        {
                            playercanAttack = player.CanAttack(monster.Position, board.GetAdjacentTiles(player.Position));
                            if (playercanAttack && monster.IsActive) canAttack = "(A)ttack";
                        }
                        string canInvestigate = (board.InvestigationAreas.Contains(player.Position)) ? "(I)nvestigate" : "";
                        string canSearch = ((board.GetPanic(player.Position) != Tile.Panic.Level_0)) ? "(S)earch" : "";
                        string canResearch = board.ResearchAreas.Contains(player.Position) ? "(R)esearch" : "";
                        Console.ForegroundColor = player.Color;
                        Console.Write("\n"+player.Name);
                        Console.ResetColor();
                        Console.WriteLine("'s turn (M)ove {0} {1} {2} {3} s(K)ip (U)se Card", canSearch,canResearch, canInvestigate, canAttack);//.........Display Player Choices.......//
                        Console.WriteLine("{0} is located at {1}", player.Name, player.Position);//............................show Player Position.......................//
                        foreach (Clue clue in player.ClueHand)
                            Console.WriteLine("{0}", clue.Name);
                        Console.WriteLine("The Panic at {0} is at {1}", player.Position, board.GetPanic(player.Position));//..................Show Current Panic..........//
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
                        string strQuery = Console.ReadLine();
                        switch (strQuery.ToLower())
                        {
                            case "m"://.........................................................................Movement..................................//
                                int count = 0;
                                cost = 1;
                                player.Move(board, cost);
                                break;
                            case "a"://..........................................................................Attack....................................//
                                int monsterNum = 0;
                                    foreach (Monster monster in gm.Monsters)
                                {
                                    if (monster.Position == player.Position) monsterNum += 1;
                                    monsterNum += board.GetAdjacentTiles(player.Position).FindAll(x => x == monster.Position).Count;
                                }   
                                if (monsterNum > 0)
                                {
                                    Console.WriteLine("Attack which monster?");
                                    gm.DisplayMonsters();
                                    Int32.TryParse(Console.ReadLine(), out numQuery);

                                    if (numQuery == 0 || numQuery > monsterNum) numQuery = 1;
                                    Monster monster = gm.Monsters[numQuery - 1];
                                    player.Damage(monster, 1);
                                    if (monster.Health < 0)
                                    {
                                        Console.WriteLine("The {0} Falls", monsterName);
                                        monster.IsActive = false;
                                    }
                                    Console.ResetColor();
                                    player.DiscardCard();
                                }
                                
                                    break;
                                
                            case "i"://............................................................................Investigate..............................//
                                cost = 1;
                                int times = 1;
                                player.InvestigateClue(cost, gm, times);
                                break;
                            case "s"://.............................................................................Search..........................//
                                Console.WriteLine("{0} is at {1}", player.Position, board.GetPanic(player.Position));
                                Console.WriteLine("Do you want to search for clues? (y/n) (level_1 and above)");
                                strQuery = Console.ReadLine().ToLower();
                                if (strQuery == "y" && (board.GetPanic(player.Position) != Tile.Panic.Level_0 ))
                                {
                                    gm.Deal(player.ClueHand);
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine("{0} drew {1}",player.Name, player.ClueHand.Last().Name.ToString());
                                    Console.ResetColor();
                                    board.DecreasePanic(player.Position);
                                    player.DiscardCard();
                                }
                                else Console.WriteLine("Nothing to Search");
                                break;
                            case "r":
                                if (board.ResearchAreas.Contains(player.Position))
                                {

                                }
                                break;
                            case "u"://.............................................................Use A card..........................................//
                                count = 1;
                                foreach (Action card in player.ActionHand)
                                {
                                    Console.Write("{0}). {1} ", count, card.Name.ToString());
                                    count += 1;
                                }
                                Int32.TryParse(Console.ReadLine(), out numQuery);
                                if (numQuery == 0) break;
                                Action selectedAction = player.ActionHand[numQuery - 1];
                                Monster selectedMonster = null;
                                switch (selectedAction.Name)
                                {
                                    case Action.Type.Rifle://.....................................Rifle.................................................//
                                        List<Monster> rangedMonsters = WeaponAbilities.Rifle(board, player, gm.Monsters);
                                        count = 1;
                                        foreach (Monster monster1 in rangedMonsters)
                                        {
                                            if (monster1.IsRevealed == true) { monsterName = monster1.Name; }
                                            else { monsterName = "A figure"; }
                                        
                                            Console.WriteLine("{0}). {1} is at {2}",count, monsterName, monster1.Position);
                                            count += 1;
                                        }
                                        Int32.TryParse(Console.ReadLine(), out numQuery);
                                        if (numQuery == 0 || numQuery > rangedMonsters.Count)      numQuery = 1; // default number
                                        selectedMonster = gm.Monsters[numQuery - 1];
                                        player.Damage(selectedMonster, WeaponAbilities.RifleDamage);
                                        player.DiscardCard(selectedAction);
                                        break;
                                    case Action.Type.Shotgun://..................................Shotgun...............................................//
                                        gm.DisplayMonsters();
                                        Int32.TryParse(Console.ReadLine(), out numQuery);
                                        if (numQuery == 0)      numQuery = 1;
                                            selectedMonster = gm.Monsters[numQuery - 1];
                                            if (player.Position == selectedMonster.Position)
                                            {
                                                player.Damage(selectedMonster, WeaponAbilities.ShotgunDamage);
                                                player.DiscardCard(selectedAction);
                                            }
                                            break;
                                    case Action.Type.doubleMove://..............................Double Movement..........................................//
                                            Console.WriteLine("{0} sprints",player.Name);
                                            cost = 0;
                                            times = 2;
                                            player.Move(board, cost, times);
                                        player.DiscardCard(selectedAction);
                                        break;
                                    case Action.Type.doubleSearch://......................Double Search.......................................//
                                        for (int i = 0; i < 2; i++)
                                            if (board.GetPanic(player.Position) != Tile.Panic.Level_0)
                                            {
                                                gm.Deal(player.ClueHand);
                                                Console.WriteLine("{0} drew {1}", player.Name, player.ClueHand.Last().Name.ToString());
                                                board.DecreasePanic(player.Position);
                                                player.DiscardCard(selectedAction);
                                            }
                                        break;
                                    case Action.Type.doubleInvestigate://.............................Double Investigate......................................//
                                        cost = 0;
                                        times = 2;
                                        player.InvestigateClue(cost, gm, times);
                                        player.DiscardCard(selectedAction);
                                        break;
                                }    
                                break;
                            case "k"://....................................................................Skip Turn..............................................//
                                endTurn = true;
                                break;
                            default: break;
                        }
                        if (player.ActionHand.Count == 0)
                            endTurn = true;
                        //.........................................................................................................................................//
                        //......................................................End Player Turn Sequence...........................................................//
                        //.........................................................................................................................................//
                        Monster bossMonster = gm.Monsters.First();
                            gm.RevealMonster(bossMonster);
                            if (gm.CheckLoseCon(board))
                            {
                                Console.BackgroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("{0} Has destroyed the City! You Have Failed!", bossMonster.Name);
                                Console.WriteLine("Press enter to exit");
                                Console.ReadLine();
                                endTurn = true;
                                playing = true;
                                endProgram = true;
                                return;
                            }
                            if (gm.CheckWinCon(bossMonster, gm.CheckLoseCon(board)))
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("You have slain the {0} and saved the City!", bossMonster.Name);
                                Console.WriteLine("Press enter to exit");
                                Console.ReadLine();
                                endTurn = true;
                                playing = true;
                                endProgram = true;
                                return;
                            }
                        gm.Monsters.RemoveAll(x => x.Health < 1 && x != gm.Monsters[0]);
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
                            monster.Move(board.GetAdjacentTiles(monster.Position), Players);
                            board.IncreasePanic(monster.Position);
                            Console.WriteLine("A commotion is heard coming from {0}", monster.Position);
                        }
                        Console.ResetColor();
                        if (monster.IsRevealed)
                        {
                            monster.UseAbility(board, Players, gm);
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
                foreach (Monster monster in gm.Summoned)
                    gm.Monsters.Add(monster);
                gm.Summoned.Clear();
                
            }
        }
    }
}
