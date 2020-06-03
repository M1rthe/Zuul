using System;

namespace Zuul
{
	public class Game
	{
		private Parser parser;
		private Player player;

        private string commandWord;
        private string secondWord;
        private string thirdWord;

        public Game ()
		{
			parser = new Parser();
            player = new Player();
		}

        /* The Game */
		public void play()
		{
			printWelcome();
            bool finished = false;

            //Loops until game is finished 
            while (! finished || player.isAlive())
            {
				Command command = parser.getCommand();
				finished = processCommand(command);
            }

            Console.Clear();
            if (player.isAlive()) { printEndMessage(); } //'quit'
            else if (!player.isAlive()) { printDeadMessage(); } //You died
		}

        /* Process commands & returns if you wantToQuit */
        private bool processCommand(Command command)
		{
			bool wantToQuit = false;

			if(command.isUnknown())
            {
				Console.WriteLine("I don't know what you mean...");
				return false;
			}
            else if (!parser.commands.isSecondCommand(command.getSecondWord()))
            {
                Console.WriteLine("I don't know what you mean...");
                return false;
            }

			commandWord = command.getCommandWord();
            secondWord = command.getSecondWord();
            thirdWord = command.getThirdWord();

            //Help
            if (commandWord == "help") { commandHelp(command); }

            //Quit
            if (commandWord == "quit") { wantToQuit = true; }

            //Go
            if (commandWord == "go") { player.goRoom(command); }

            //Show
            if (commandWord == "show") { commandShow(command); }

            /*
			switch (commandWord)
            {
				case "help":
					commandHelp();
					break;
				case "quit":
                    commandGo(command);
					break;
                case "go":
                    commandShow(command);
                    break;
                case "show":
                    wantToQuit = true;
                    break;
            }
            */

            return wantToQuit;
		}

        /**********************************************/
        /***print**************************************/
        /**********************************************/
        private void printWelcome()
        {
            Console.WriteLine();
            Console.WriteLine("Welcome to the game Zuul");
            Console.WriteLine("You are here to rob a bank");
            Console.WriteLine("Type 'help' if you need help.");
            Console.WriteLine();
            Console.WriteLine(player.currentRoom.getRoomDescription());
        }

        private void commandHelp(Command command)
		{
			Console.WriteLine();
			Console.WriteLine("Your command words are:");
            parser.commands.showAll1();
        }

        private void commandShow(Command command)
        {
            //Show all second commands
            if (command.hasSecondWord()) { parser.commands.showAll2(); }

            if (command.hasSecondWord() && parser.commands.isSecondCommand(secondWord))
            {
                //Hp
                if (secondWord == "hp") { Console.WriteLine("Your health is " + player.hp); }

                //Room
                if (secondWord == "room") { Console.WriteLine(player.currentRoom.getRoomDescription()); }

                //Inventory
                if (secondWord == "inventory") { player.inventory.printInventory(); }

                //Itemdata
                if (secondWord == "itemData")
                {
                    Item item = player.inventory.string2Item(thirdWord);

                    if (item != null) //Is valid
                    {
                        player.inventory.printItemData(item);
                    }
                }
            }
        }

        private void printDeadMessage()
        {
            Console.WriteLine();
            Console.WriteLine("    :::::::::         :::::::::::       :::::::::                                        ");
            Console.WriteLine("    :+:    :+:            :+:           :+:    :+:                                       ");
            Console.WriteLine("    +:+    +:+            +:+           +:+    +:+                                       ");
            Console.WriteLine("    +#++:++#:             +#+           +#++:++#+                                        ");
            Console.WriteLine("    +#+    +#+            +#+           +#+                                              ");
            Console.WriteLine("    #+#    #+# #+#        #+#     #+#   #+#      #+#                                     ");
            Console.WriteLine("    ###    ### ###    ########### ###   ###      ###                                     ");
            Console.WriteLine();
        }

        private void printEndMessage()
        {
            Console.WriteLine();
            Console.WriteLine("    ::::::::::: :::    :::  :::    :::             :::::                                 ");
            Console.WriteLine("        :+:     :+:    :+:  :+:    :+:            :+:+:+                                 ");
            Console.WriteLine("        +:+     +:+    +:+   +:+  +:+            +:+ +:+                                 ");
            Console.WriteLine("        +#+     +#++:++#++    +#++:+            +#+  +:+                                 ");
            Console.WriteLine("        +#+     +#+    +#+   +#+  +#+          +#+#+#+#+#+                               ");
            Console.WriteLine("        #+#     #+#    #+#  #+#    #+#               #+#                                 ");
            Console.WriteLine("        ###     ###    ###  ###    ###               ###                                 ");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("    :::::::::   :::             :::     :::   ::: :::::::::::  ::::    :::   ::::::::    ");
            Console.WriteLine("    :+:    :+:  :+:           :+: :+:   :+:   :+:     :+:      :+:+:   :+:  :+:    :+:   ");
            Console.WriteLine("    +:+    +:+  +:+          +:+   +:+   +:+ +:+      +:+      :+:+:+  +:+  +:+          ");
            Console.WriteLine("    +#++:++#+   +#+         +#++:++#++:   +#++:       +#+      +#+ +:+ +#+  :#:          ");
            Console.WriteLine("    +#+         +#+         +#+     +#+    +#+        +#+      +#+  +#+#+#  +#+   +#+#   ");
            Console.WriteLine("    #+#         #+#         #+#     #+#    #+#        #+#      #+#   #+#+#  #+#    #+#   ");
            Console.WriteLine("    ###         ##########  ###     ###    ###    ###########  ###    ####   ########    ");
            Console.WriteLine();
        }
    }
}
