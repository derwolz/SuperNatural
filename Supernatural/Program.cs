using System;

namespace Supernatural
{
    class Program
    {
        static void Main(string[] args)
        {
            bool endProgram = false;
            Console.WriteLine("How Many Players? Max 4");
            Int32.TryParse(Console.ReadLine(), out int numquery);
            if (numquery == 0 || numquery > 4)
            {
                Console.WriteLine("Number was out of range or non-number. Exiting");
                endProgram = true;
            }
            else
            {
                Game game = new SupernaturalGame();
                for (int i = 0; i < numquery; i++)
                {
                    Player player = new Player();
                    Console.WriteLine("Pick a name for Player {0}", i + 1);
                    player.Name = Console.ReadLine();
                    game.Players.Add(player);
                }

                try
                {
                    while (!endProgram)
                    {
                        game.Play();
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Answer was in the wrong format");
                    return;
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("Oops Something Broke!");
                    Console.ResetColor();
                    return;
                }
                catch (Exception)
                {
                    Console.WriteLine("Try listening to directions next time");
                    return;
                }
            }
            
            
        }
    }
}
