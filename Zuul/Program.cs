using System;

namespace Zuul
{
    class Program
    {
        /* The Document class */
        public static void Main(string[] args)
        {
            Game game = new Game();
            game.play();

            Console.ReadLine();
        }
    }
}
