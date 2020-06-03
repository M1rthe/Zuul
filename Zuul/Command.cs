namespace Zuul
{
	public class Command
	{
		private string commandWord;
		private string secondWord;
        private string thirdWord;

		public Command(string firstWord, string secondWord, string thirdWord)
		{
			this.commandWord = firstWord;
			this.secondWord = secondWord;
            this.thirdWord = thirdWord;
		}

        /* Get string */
		public string getCommandWord()
		{
			return commandWord;
		}

		public string getSecondWord()
		{
			return secondWord;
		}

        public string getThirdWord()
        {
            return thirdWord;
        }

        /* Return bool */
        public bool isUnknown()
		{
			return (commandWord == null);
		}

		public bool hasSecondWord()
		{
			return (secondWord != null);
		}

        public bool hasThirdWord()
        {
            return (thirdWord != null);
        }
    }
}
