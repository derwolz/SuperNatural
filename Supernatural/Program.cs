using System;

namespace Supernatural
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Game game = new SupernaturalGame();
            Console.WriteLine("How Many Players? Max 4");
            Int32.TryParse(Console.ReadLine(), out int numquery);
            if (numquery < 1 || numquery > 4) // Check to see if number is in range
            {
                Console.WriteLine("Number was out of range or non-number. Exiting");
                game.endProgram = true;
            }
            else
            {
                
                for (int i = 0; i < numquery; i++) // Character Creation
                {
                    Player player = new Player();
                    player.Color = (ConsoleColor)(2+i * 3);
                    Console.ForegroundColor = player.Color;
                    Console.WriteLine("Pick a name for Player {0}", i + 1);
                    Console.ResetColor();
                    player.Name = Console.ReadLine();
                    player.Range = 1;
                    game.Players.Add(player);
                }

                game.endProgram = false;
                try
                {
                    while (!game.endProgram)
                    {
                        game.Play();
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Answer was in the wrong format");
                    return;
                }
                //catch (ArgumentOutOfRangeException)
                //{
                //    Console.BackgroundColor = ConsoleColor.Red;
                //    Console.WriteLine("Your answer landed the system out of bounds");
                //    Console.ResetColor();
                //    return;
                //}
                
            }
            
            
        }
    }
}
