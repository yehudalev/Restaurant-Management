using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace BE
{
    public class Dish
    {
        public Dish() { }

        public Dish(int number, string name, SIZE size, double price, KASHRUT kashrut,double discount=100)
        {
            this.number = number;
            this.name = name;
            this.size = size;
            this.price = price;
            this.kashrut = kashrut;
        }

        public int number { get; set; }
        public string name { get; set; }
        public SIZE size{ get; set; }
        public double price { get; set; }
        public KASHRUT kashrut { get; set; }
        public  double discount { get; set; }
     
        public override string ToString()
        {
            string tmp = "";
            foreach (PropertyInfo property in this.GetType().GetProperties())
            {
                tmp += string.Format("{0}: {1}\n", property.Name, property.GetValue(this, new object[] { }));
            }

            return tmp;
            //Console.WriteLine("DISH\nID: {0}\nName: {1}\nSize: {2}\nKosher: {3}\nPrize: {4}", id_dish, name_dish, size_dish, kosher_dish, prize_dish);
            //return "\n";
        }

        //Assuming all properties are value type.
        public Dish cloneDish()
        {
            return (Dish)this.MemberwiseClone();
        }

    }


}
