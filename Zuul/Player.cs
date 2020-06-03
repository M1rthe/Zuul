using System;

namespace Zuul
{
    public class Player
    {
        public Inventory inventory = new Inventory();
        public Room currentRoom;
        private Room entrance, lobby, reception, storage, meetingHall, meetingRoom1, meetingRoom2, meetingRoom3, administration, canteen, dataArchive, vault;
        public int hp = 100;
        private int killedGuards = 0;
        private int killedRecptionist = 0;
        private bool administrationDoorIsLocked = true;

        public Player()
        {
            createRooms();
            inventory.equipedItems.Add(inventory.string2Item("knife"));
            //take("knife");
        }

        public void damage(int amount)
        {
            hp -= amount;
            if (hp < 0) { hp = 0; }
        }

        public void heal(int amount)
        {
            hp += amount;
            if (hp > 100) { hp = 100; }
        }

        public bool isAlive()
        {
            if (hp > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /* Drop item*/
        public void drop(Item item)
        {
            if (inventory.isValidItem(item))
            {
                inventory.equipedItems.Remove(item);
            }
        }

        /* Take item */
        public void take(string _item)
        {
            Item item = inventory.string2Item(_item);

            if (inventory.isValidItem(item) && currentRoom.item == item) //If the item exists AND is in this room
            {
                if (inventory.getWeight() + item.weight <= 50) //Check weight
                {
                    inventory.equipedItems.Add(item);
                    currentRoom.item = null;
                }
                else { Console.WriteLine("Your inventory is to heavy"); }
            }
            else { Console.WriteLine("That is not an valid item"); }
        }

        /* Use item */
        public void use(Item item)
        {
            if (inventory.isValidItem(item) && inventory.isEquipedItem(item))
            {
                if (item.name == "knife")
                {
                    if (currentRoom == reception) { printStab("receptionist"); }
                    else if (currentRoom == administration) { printStab("guard"); }
                }

                if (item.name == "pistol")
                {
                    if (currentRoom == administration)
                    {
                        if (killedGuards < 3) { printShoot("guard"); }
                        else { Console.WriteLine("There are no more guards"); }
                    }
                }

                if (item.name == "bandage")
                {
                    heal(30);
                    Console.WriteLine("You used the bandage, your hp is " + hp);
                }

                if (item.name == "donut")
                {
                    damage(10);
                    Console.WriteLine("That was jummy, but it is poisoned, your hp is " + hp);
                }

                if (item.name == "key")
                {
                    if(currentRoom == lobby)
                    {
                        administrationDoorIsLocked = false;
                        Console.WriteLine("You unlocked the door of the administration");
                    }
                }

                if (item.name == "money")
                {
                    Console.WriteLine("You can't buy anything in the bank");
                }

                if (item.name == "banana peel")
                {
                    Console.WriteLine("You ate the banana peel and it tastes terrible, your hp is "+hp);
                }
            }
        }

        /* Go to room */
        public void goRoom(Command command)
        {
            if (!command.hasSecondWord())
            {
                //If no destination is given ask 'Go where'
                Console.WriteLine("Go where?");
                return;
            }

            string direction = command.getSecondWord();

            //Gets directions
            Room nextRoom = currentRoom.getExit(direction);

            if (nextRoom != null)
            {
                //Goto administration door that is locked
                if (currentRoom == lobby && nextRoom == administration && administrationDoorIsLocked)
                {
                    Console.WriteLine("The door is locked, you need to use a key to open it"); 
                }
                //Goto next room
                else
                {
                    if (hp <= 70) { damage(5); Console.WriteLine("Because you are wounded you lost some blood, your hp is "+hp); }

                    currentRoom = nextRoom;
                    Console.WriteLine(currentRoom.getRoomDescription());
                    if (currentRoom.item == inventory.bananaPeel) { damage(5); Console.WriteLine("Oh no you slipped over a bananapeel & fell on you butt, your hp is "+hp); }
                }
            }
            //If there is no exit in that direction
            else { Console.WriteLine("There is no door to " + direction + "!"); }
        }

        private void createRooms()
        {
            //Create the rooms
            entrance = new Room("in the entrace of a bank", null);
            lobby = new Room("in the lobby", null);
            reception = new Room("in the recepting", inventory.key);
            storage = new Room("in the storage, it looks like the weapon stock of the guards", inventory.pistol);
            meetingHall = new Room("in the meeting hall, there hangs a first aid kit with bandages on the wall", inventory.bandage);
            meetingRoom1 = new Room("in meeting room 1", null);
            meetingRoom2 = new Room("in meeting room 2", null);
            meetingRoom3 = new Room("in meeting room 3", inventory.bananaPeel);
            administration = new Room("in the administration", null);
            canteen = new Room("in the staff canteen", inventory.donut);
            dataArchive = new Room("in the data archive", null);
            vault = new Room("in the vault", inventory.money);

            //Initialise room exits
            //entrance
            entrance.setExit("east", lobby);

            //lobby
            lobby.setExit("west", entrance);
            lobby.setExit("north", reception);
            lobby.setExit("east", administration);
            lobby.setExit("up", meetingHall);

            //reception
            reception.setExit("south", lobby);
            reception.setExit("west", storage);

            //storage
            storage.setExit("east", reception);

            //meetingHall
            meetingHall.setExit("down", lobby);
            meetingHall.setExit("east", meetingRoom3);
            meetingHall.setExit("south", meetingRoom2);
            meetingHall.setExit("west", meetingRoom1);

            //meetingRoom1
            meetingRoom1.setExit("east", meetingHall);

            //meetingRoom2
            meetingRoom2.setExit("north", meetingHall);

            //meetingRoom3 
            meetingRoom3.setExit("west", meetingHall);

            //administration
            administration.setExit("west", lobby);
            administration.setExit("north", vault);
            administration.setExit("east", dataArchive);
            administration.setExit("south", canteen);

            //canteen
            canteen.setExit("north", administration);

            //dataArchive 
            dataArchive.setExit("west", administration);

            //vault
            vault.setExit("south", administration);

            currentRoom = entrance;  //Start game at the entrance
        }

        private void printStab(string victim)
        {
            Console.WriteLine();
            Console.WriteLine("                                ######");
            Console.WriteLine("                              ##### %%###");
            Console.WriteLine("                             #############%");
            Console.WriteLine("                            ##########%%");
            Console.WriteLine("                           #########%%");
            Console.WriteLine("                          #######%%");
            Console.WriteLine("                        .######%%");
            Console.WriteLine("                       #######%");
            Console.WriteLine("                     (#######%");
            Console.WriteLine("                   ########%");
            Console.WriteLine("                 %%%%%%#####%");
            Console.WriteLine("                &%%%%%%%%%%%");
            Console.WriteLine("               %%%%%%%******");
            Console.WriteLine("              %%%%%%%*****");
            Console.WriteLine("             %%%%%%/*****");
            Console.WriteLine("            %%%%%%%*****");
            Console.WriteLine("           %%%%%%******");
            Console.WriteLine("          %%%%%******");
            Console.WriteLine("         %%%*******");
            Console.WriteLine("        ********");
            Console.WriteLine("      .****");
            Console.WriteLine();
            Console.WriteLine();
            if (victim == "receptionist")
            {
                killedRecptionist++;
                damage(20);
                Console.WriteLine("Before you killed her, she was able to stab you with a pencil on her desk");
                Console.WriteLine("You need to find bandages, because by every move you make you lose more blood");
            }
            if (victim == "guard")
            {
                killedGuards++;
                Console.WriteLine("You just stabbed a guard, nice!!");
            }
            Console.WriteLine();
        }

        private void printShoot(string victim)
        {
            bool hitGuard = false;

            //Hit victim or not
            Random random = new Random();
            int randomNumber = random.Next(0, 2);  //Number between 0 and 1
            if (randomNumber == 0) { hitGuard = true; }
            if (randomNumber == 1) { hitGuard = false; }
            //Get hit or not
            random = new Random();
            randomNumber = random.Next(0, 2);  //Number between 0 and 1
            if (randomNumber == 0)
            {
                damage(20);
                Console.WriteLine("You got hit by one of the guards, your hp is "+hp);
            }
            if (randomNumber == 1)
            {
                Console.WriteLine("You almost got hit yourself");
            }

            if (hitGuard)
            {
                Console.WriteLine();
                Console.WriteLine("                                                                      %%");
                Console.WriteLine("             %%%%%%%%%%%%%%                      %%%%%%%%%%%%%%%%%%%%%%%%%%");
                Console.WriteLine("          %%%%%@%@%&&%@%&&%@%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");
                Console.WriteLine("          %%%%@%@%@@%@%@@&@%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");
                Console.WriteLine("         %%%%@%@%@@%@%@@%@%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");
                Console.WriteLine("        %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");
                Console.WriteLine("       %%%%%@@%%%%@@%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");
                Console.WriteLine("         &%%%%%%%%%%%%%%%%%####           $$$/");
                Console.WriteLine("         %%%%%%%%%%%%%%%%   ####        $$/");
                Console.WriteLine("         %%%%%%%%%%%%%%%       ###  $$$/");
                Console.WriteLine("        %%%%%%%%%%%%%%%%$$$$$$$$$$$$$$*");
                Console.WriteLine("       %%%%%%%%%%%%%%%%$");
                Console.WriteLine("       %%%%%%%%%%%%%%%");
                Console.WriteLine("      %%%%%%%%%%%%%%%%");
                Console.WriteLine("     %%%%%%%%%%%%%%%%");
                Console.WriteLine("    #%%%%%%%%%%%%%%%");
                Console.WriteLine("      &%%%%%%%%%%%.");
                Console.WriteLine();
                Console.WriteLine();
                if (victim == "guard")
                {
                    if (killedGuards == 0) { Console.WriteLine("You killed one of the guard, nice!!"); }
                    if (killedGuards == 1) { Console.WriteLine("You only need to shoot one guard, you are killing it!!"); }
                    if (killedGuards == 2) { Console.WriteLine("You killed all the guard, GG's"); }
                }
                Console.WriteLine();
                killedGuards++;
            }
            else { Console.WriteLine("You missed him by an inch"); }
        }

        private void printUnlocked()
        {
            Console.WriteLine();
            Console.WriteLine("           ****///////&****");
            Console.WriteLine("        ,*&////*/*&////////**&");
            Console.WriteLine("      **(////*     */////////**");
            Console.WriteLine("     **//////&*(*////////////**");
            Console.WriteLine("    *&////////////////////////*&");
            Console.WriteLine("    *&///////////////////////*/");
            Console.WriteLine("    #*//////////////////////*/");
            Console.WriteLine("      &*%//////////////////***&");
            Console.WriteLine("        &**&(///////////********%");
            Console.WriteLine("            #(********@  ..**//(***");
            Console.WriteLine("                          &..* *//@**/");
            Console.WriteLine("                            *..* &/ (***");
            Console.WriteLine("                                &.* *&/ **&");
            Console.WriteLine("                                   *..* *&**/");
            Console.WriteLine("                                       @*.* ***");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("You unlocked the door to the administration");
            Console.WriteLine();
        }
    }
}
