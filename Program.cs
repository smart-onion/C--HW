
using System.Collections;

namespace Example6
{
    public class Program
    {
        struct Item
        {
            private String name;
            private uint price;
            private double discount;

            public Item(String name, uint price, double discount = 0)
            {
                SetDiscount(discount);
                SetName(name);
                SetPrice(price);
            }

            public void SetName(String name)
            {
                this.name = name.ToUpper();
            }

            public void SetPrice(uint price)
            {
                this.price = (uint)(price - price * this.discount);
            }

            public void SetDiscount(double discount)
            {
                if(discount <= 1 && discount >= 0)
                {
                    this.discount = discount;
                }
                else
                {
                    throw new ArgumentException("Discount Value shout be between 0 and 1");
                }
            }

            public String GetName()
            {
                return this.name;
            }

            public uint GetPrice()
            {
                return this.price;
            }

            public double GetDiscount()
            {
                return this.discount;
            }
        }
        struct Check
        {
            private Hashtable items;
            private uint totalBill;
            public Check()
            {
                this.items = new Hashtable();
                this.totalBill = 0;
            }

            public void AddItem(Item item)
            {
                this.totalBill += item.GetPrice();

                if (items.ContainsKey(item))
                {
                    items[item] = (int)items[item] + 1;
                }
                else
                {
                    items[item] = 1;
                }
            }

            public void AddItems(List<Item> itemList)
            {
                foreach  (Item item in itemList)
                {
                    this.totalBill += item.GetPrice();

                    if (items.ContainsKey(item))
                    {
                        items[item] = (int)items[item] + 1;
                    }
                    else
                    {
                        items[item] = 1;
                    }
                }
               
            }

            public void PrintCheck()
            {
                Console.WriteLine("Your check:");
                Console.WriteLine("---------------------------");
                Console.WriteLine("{0,-10} {1,8} {2,8}", "Name", "Count", "Price");
                Console.WriteLine("---------------------------");
                foreach (DictionaryEntry item in this.items)
                {
                    Item key = (Item)item.Key;
                    int price = (int)key.GetPrice() * (int)item.Value;
                    Console.WriteLine("{0,-10} {1,5} {2,9}$", key.GetName(), (int)item.Value, price);
                }

                Console.WriteLine("---------------------------");
                Console.WriteLine("Total: " + this.totalBill + "$");
            }

            public uint GetTotalBill()
            {
                return this.totalBill;
            }

            public Hashtable GetItems()
            {
                return this.items;
            }

        }
        public static void Main()
        {
            Check check = new Check();
            Item apple = new Item("Apple", 10);
            Item orange = new Item("Orange", 15, 0.2);
            List<Item> apples = new List<Item>();
            apples.Add(apple);
            apples.Add(apple);
            apples.Add(apple);
            apples.Add(apple);
            apples.Add(apple);
            check.AddItems(apples);
            check.AddItem(orange);
            check.PrintCheck();
        }
    }
}