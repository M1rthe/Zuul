using System;

namespace Zuul
{
	public class Parser
	{
		public CommandLibrary commands; //Holds all valid command words

		public Parser()
		{
			commands = new CommandLibrary();
		}

		/* Reads commands AND check if it is a command */
		public Command getCommand()
		{
			Console.Write("> "); 

			string word1 = null;
			string word2 = null;
            string word3 = null;

			string[] words = Console.ReadLine().Split(' ');
			if (words.Length > 0) { word1 = words[0]; }
			if (words.Length > 1) { word2 = words[1]; }
            if (words.Length > 2) { word3 = words[2]; }

            //If command is known, return the WORDS
            if (commands.isCommand(word1)) {
				return new Command(word1, word2, word3);
			}

			// If command is unknown, return NULL
			return new Command(null, null, null);
		}
	}
}
