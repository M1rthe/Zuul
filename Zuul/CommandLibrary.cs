using System;

namespace Zuul
{
	public class CommandLibrary
	{
		private string[] validCommands;
        private string[] validSecondCommands;

        public CommandLibrary()
		{
			validCommands = new string[] 
            {
				"help",
				"quit",
				"go",
                "show"
			};

            validSecondCommands = new string[]
            {
                "hp",
                "room",
                "inventory",
                "itemData"
            };
		}

		/* Check if it is a valid command */
		public bool isCommand(string instring)
		{
			for(int i = 0; i < validCommands.Length; i++)
            {
				if (validCommands[i] == instring)
                {
					return true;
				}
			}
            return false;
		}

        /* Check if it is a valid second command */
        public bool isSecondCommand(string instring)
        {
            for (int i = 0; i < validSecondCommands.Length; i++)
            {
                if (validSecondCommands[i] == instring)
                {
                    return true;
                }
            }
            return false;
        }

        /* Print all valid commands */
        public void showAll1()
		{
			for(int i = 0; i < validCommands.Length; i++)
            {
				Console.Write(validCommands[i]);

				if (i != validCommands.Length - 1)
                {
					Console.Write(", ");
				}
			}
			Console.WriteLine();
		}

        /* Print all valid commands */
        public void showAll2()
        {
            for (int i = 0; i < validSecondCommands.Length; i++)
            {
                Console.Write(validSecondCommands[i]);

                if (i != validSecondCommands.Length - 1)
                {
                    Console.Write(", ");
                }
            }
            Console.WriteLine();
        }
    }
}
