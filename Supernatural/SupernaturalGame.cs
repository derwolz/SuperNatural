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
            //int monsNum = Enum.GetValues(typeof(Monster.Type)).Length;
            int monsNum = 1;
            Monster Initialmonster = new Monster();
            MonsterFactory factory = new MonsterFactory();
            WeaponFactory Weapfactory = new WeaponFactory();
            Random random = new Random();
            int rand = random.Next(monsNum);
            Monster.Type mType = (Monster.Type)rand;
            Initialmonster = factory.Factory(mType, Players);
            GameMaster gm = new GameMaster(Initialmonster);
            Random r = new Random();
            gm.Monsters[0].Position = (Tile.Places)(r.Next(boardNum-Players.Count) + Players.Count);
            gm.Monsters[0].IsActive = true;
            gm.Monsters[0].IsRevealed = false;
            bool playing = false;
            foreach (Player player in Players)
            {
                player.Position = Tile.Places.Junkyard;
                player.Shuffle();
            }
            gm.Shuffle();
            while (!playing)
            {
                
                foreach (Player player in Players)
                {
                    //.................................................................................................................................................///
                    //................................................................TurnInitialization...............................................................///
                    //.................................................................................................................................................///
                    string canAttack = "";
                    string monsterName = "";
                    List<Monster> targets;
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
                            canAttack = GameActions.Target(board,player,gm.Monsters, player.Range).Count > 0 ? "(A)ttack" : "";
                        string canInvestigate = (board.InvestigationAreas.Contains(player.Position)) ? "(I)nvestigate" : "";
                        string canSearch = ((board.GetPanic(player.Position) != Tile.Panic.Level_0)) ? "(S)earch" : "";
                        string canResearch = board.ResearchAreas.Contains(player.Position) ? "(R)esearch" : "";
                        string canWeapon = player.Weapons.Count > 0 ? "Use (W)eapon" : "";
                        Console.ForegroundColor = player.Color;
                        Console.Write("\n"+player.Name);
                        Console.ResetColor();
                        Console.WriteLine("'s turn (M)ove {0} {1} {2} {3} {4} s(K)ip (U)se Card", canSearch,canResearch, canInvestigate, canAttack, canWeapon);//.........Display Player Choices.......//
                        Console.WriteLine("{0} is located at {1}", player.Name, player.Position);//............................show Player Position.......................//
                        Console.WriteLine();
                        foreach (Clue clue in player.ClueHand)
                            Console.Write("{0} ", clue.Name);
                        Console.WriteLine();
                        foreach (Weapon.WeaponParts part in player.WeaponHand)
                            Console.Write("{0} ", part);
                        Console.WriteLine();
                        foreach (Weapon weapon in player.Weapons)
                            Console.Write("{0} ", weapon.Name);
                        Console.WriteLine("\nThe Panic at {0} is at {1}", player.Position, board.GetPanic(player.Position));//..................Show Current Panic..........//
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
                                targets = GameActions.Target(board, player, gm.Monsters, player.Range);
                                
                                if (targets.Count > 0)
                                {
                                    Console.WriteLine("Attack which monster?");
                                    GameActions.DisplayMonsters(targets);
                                    GameActions.Damage(GameActions.GetTarget(targets),player.Name, 1);
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
                                    Console.WriteLine("(R)esearch or (B)uild a weapon?");
                                    strQuery = Console.ReadLine().ToLower();
                                    if (strQuery == "r")
                                    {
                                        gm.Deal(player.WeaponHand);
                                        player.DiscardCard();
                                    }
                                    else if (strQuery == "b")
                                    {
                                        if (player.WeaponHand.Count > 0)
                                            if (Weapfactory.MakeWeapon(player, Players))
                                            {
                                                player.DiscardCard();
                                            }  
                                    }
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
                                if (numQuery < 1) break;
                                Action selectedAction = player.ActionHand[numQuery - 1];
                                switch (selectedAction.Name)
                                {
                                    case Action.Type.Rifle://.....................................Rifle.................................................//
                                        targets = GameActions.Target(board, player, gm.Monsters, player.Range + 1);
                                        if (targets.Count > 0)
                                        {
                                            GameActions.DisplayMonsters(targets);
                                            GameActions.Damage(GameActions.GetTarget(targets),player.Name, WeaponAbilities.RifleDamage);
                                            player.DiscardCard(selectedAction);
                                        }
                                        else 
                                            Console.WriteLine("Nothing to target");
                                        break;
                                    case Action.Type.Shotgun://..................................Shotgun...............................................//
                                        targets = GameActions.Target(board, player, gm.Monsters, 0);
                                        if (targets.Count > 0)
                                        {
                                            GameActions.DisplayMonsters(targets);
                                            GameActions.Damage(GameActions.GetTarget(targets), player.Name, WeaponAbilities.ShotgunDamage);
                                            player.DiscardCard(selectedAction);
                                        }
                                        else Console.WriteLine("Nothing to Target");
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
                            case "w": // Enables the use of Weapons
                                count = 0;
                                if (player.Weapons.Count > 0)
                                {
                                    if (player.Weapons.Count < 1) break;
                                    Console.WriteLine("Use which weapon?");
                                    foreach (Weapon weapon in player.Weapons)
                                    {
                                        count += 1;
                                        Console.WriteLine("{0}). {1}", count, weapon.Name.ToString());
                                    }
                                    if (Int32.TryParse(Console.ReadLine(), out numQuery))
                                    {
                                        if (numQuery < 0 || numQuery > player.Weapons.Count) numQuery = 1;
                                        targets = GameActions.Target(board, player, gm.Monsters, 1);
                                        switch (player.Weapons[numQuery - 1].Name)
                                        {
                                            case Weapon.WeaponName.Stun_Grenade:
                                                if (targets.Count > 0)
                                                {
                                                    GameActions.DisplayMonsters(targets);
                                                    WeaponAbilities.StunGrenade(GameActions.GetTarget(targets));
                                                    player.ConsumeWeapon(Weapon.WeaponName.Stun_Grenade);
                                                }
                                                break;
                                            case Weapon.WeaponName.Holy_Water:
                                                if (targets.Count > 0)
                                                {
                                                    GameActions.DisplayMonsters(targets);
                                                    WeaponAbilities.HolyWater(GameActions.GetTarget(targets));
                                                    player.ConsumeWeapon(Weapon.WeaponName.Holy_Water);
                                                }
                                                break;
                                            case Weapon.WeaponName.Silver_Bird_Shot:
                                                if (targets.Count > 0)
                                                {
                                                    GameActions.DisplayMonsters(targets);
                                                    WeaponAbilities.HolyWater(GameActions.GetTarget(targets));
                                                    player.ConsumeWeapon(Weapon.WeaponName.Silver_Bird_Shot);
                                                }
                                                break;
                                            case Weapon.WeaponName.Wooden_Slug:
                                                if (targets.Count > 0)
                                                {
                                                    GameActions.DisplayMonsters(targets);
                                                    WeaponAbilities.WoodenSlug(GameActions.GetTarget(targets));
                                                    player.ConsumeWeapon(Weapon.WeaponName.Wooden_Slug);
                                                }
                                                break;
                                            case Weapon.WeaponName.Trap_Kit:
                                                if (player.Weapons.Count > 1)
                                                {
                                                    if (WeaponAbilities.PlaceTrap(board, player))
                                                        player.ConsumeWeapon(Weapon.WeaponName.Trap_Kit);
                                                }
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "k"://....................................................................Skip Turn..............................................//
                                endTurn = true;
                                break;
                            default: break;
                        }
                        if (player.ActionHand.Count < 1)
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
                            if (!monster.isStunned)
                            {
                                monster.Move(board.GetAdjacentTiles(monster.Position), Players);
                                board.springTrap(monster.Position, monster, gm.Monsters, board);
                                board.IncreasePanic(monster.Position);
                            }
                            Console.WriteLine("A commotion is heard coming from {0}", monster.Position);
                        }
                        monster.isStunned = false;
                        Console.ResetColor();
                        if (monster.IsRevealed)
                        {
                            monster.UseAbility(board, Players, gm);
                        }
                    }
                    else
                    {
                        monster.CountDown = r.Next(monster.CountDown);
                        if (monster.CountDown < 1)
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
