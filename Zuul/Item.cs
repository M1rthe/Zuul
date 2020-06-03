namespace Zuul
{
    public class Item
    {
        public string name = "";
        public int weight = 0;
        public string description = "";

        public Item(string _name, int _weight, string _description)
        {
            name = _name;
            weight = _weight;
            description = _description;
        }

        public string getName()
        {
            return name;
        }

        public int getWeight()
        {
            return weight;
        }

        public string getDescription()
        {
            return description;
        }
    }
}
