using System;
using System.Collections;
using System.Collections.Generic;

namespace Zuul
{
    public class Inventory
    {
        public Item knife, pistol, bandage, donut, key, money, bananaPeel;
        public List<Item> equipedItems = new List<Item>();
        public List<Item> items = new List<Item>();
        public int inventoryWeight;

        public Inventory()
        {
            /*
            validItems = new string[]
            {
                "knife", 
                "pistol",
                "bandage", 
                "donut", 
                "key",
                "a stack of money",
                "banana peel"
            };
            */

            knife = new Item("knife", 10, "Knifes are sharp objects that are meant for stabbing");
            pistol = new Item("pistol", 15, "Pistol is a good weapon for short range");
            bandage = new Item("bandage", 5, "Bandages can heal you");
            donut = new Item("donut", 5, "Donuts are delicous");
            key = new Item("key", 1, "Keys can open a specific doors");
            money = new Item("A stack of money", 30, "A piece of paper or a number on a bankaccount that allow you to have a life");
            bananaPeel = new Item("Banana peel", 5, "A banana peel is a slippery remainder of a banana");

            items.Add(knife);
            items.Add(pistol);
            items.Add(bandage);
            items.Add(donut);
            items.Add(key);
            items.Add(money);
            items.Add(bananaPeel);
        }

        /* Get weight of inventory */
        public int getWeight()
        {
            inventoryWeight = 0;

            for (int i = 0; i < equipedItems.Count; i++)
            {
                inventoryWeight += (equipedItems[i].getWeight());
            }

            return inventoryWeight;
        }

        /* Check whether a given string is a valid item */
        public bool isValidItem(Item item)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] == item)
                {
                    return true;
                }
            }
            //If we get here, the string was not found in the commands
            return false;
        }

        /* Check whether a given string is a equiped item */
        public bool isEquipedItem(Item item)
        {
            for (int i = 0; i < equipedItems.Count; i++)
            {
                if (equipedItems[i] == item)
                {
                    return true;
                }
            }
            //If we get here, the string was not found in the commands
            return false;
        }

        public void printInventory()
        {
            Console.WriteLine("Inventory (" + getWeight() + " lbs)");
            for (int i = 0; i < equipedItems.Count; i++)
            {
                Console.WriteLine("-" + equipedItems[i]);
            }
        }

        public void printItemData(Item item)
        {
            Console.WriteLine(item.name + " (" + item.weight + ")");
            Console.WriteLine("-" + item.description);
        }

        public Item string2Item(string itemString)
        {
            Item item = null;

            for (int i = 0; i < equipedItems.Count; i++)
            {
                if (items[i].name == itemString)
                {
                    item = items[i];
                }
            }
            return item;
        }
    }
}
