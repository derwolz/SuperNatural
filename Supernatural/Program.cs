using System;

namespace Supernatural
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("How Many Players? Max 4");
            Int32.TryParse(Console.ReadLine(), out int numquery);
            Game game = new SupernaturalGame();
            for (int i = 0; i < numquery; i++)
            {
                Player player = new Player();
                Console.WriteLine("Pick a name for Player {0}", i + 1);
                player.Name = Console.ReadLine();
                game.Players.Add(player);
            }
            bool endProgram = false;
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
        }
    }
}
