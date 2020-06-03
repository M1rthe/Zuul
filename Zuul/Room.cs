using System.Collections.Generic;

namespace Zuul
{
	public class Room
	{
		private string description;
        public Item item;
        public List<Item> items = new List<Item>();
        private Dictionary<string, Room> exits; //Stores exits of this room.

		/* Create a room with description */
		public Room(string description, Item item)
		{
			this.description = description;

            this.item = item;
            if (item != null) { items.Add(item); }

            exits = new Dictionary<string, Room>();
		}

		/* Define an exit from this room */
		public void setExit(string direction, Room neighbor)
		{
			exits[direction] = neighbor;
		}

        /* Give room description */
		public string getRoomDescription()
		{
			string returnstring;  
            
			returnstring = "You are " + description;        
			returnstring += "\n";
            returnstring += getExitstring();
            returnstring += "\n";
            returnstring += getItemString();

			return returnstring;
		}

		/* Return string with all the exits*/
		private string getExitstring()
		{
			string returnstring = "Exits:";

			// because 'exits' is a Dictionary, we can't use a `for` loop
			int commas = 0;
			foreach (string key in exits.Keys)
            {
				if (commas != 0 && commas != exits.Count)
                {
					returnstring += ",";
				}
				commas++;
				returnstring += " " + key;
			}

			return returnstring;
		}

        /* Return string with all the exits */
        private string getItemString()
        {
            string returnstring = "Items: ";

            if (items.Count > 0)
            {
                int commas = 0;
                for (int i = 0; i < items.Count; i++)
                {
                    if (commas != 0 && commas != items.Count)
                    {
                        returnstring += ",";
                    }
                    commas++;
                    returnstring += " " + items[i].name;
                }
            }
            else { returnstring += "-"; }

            return returnstring;
        }

		/* Get exits of room */
		public Room getExit(string direction)
		{
			if (exits.ContainsKey(direction))
            {
				return (Room)exits[direction];
			}

            else { return null;	}
		}
	}
}
