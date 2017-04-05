using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;


namespace BE
{
    public class Ordered_Dish
    {
        public Ordered_Dish() { }

        public Ordered_Dish(int orderNumber, int dishNumber, int amount)
        {
            this.orderNumber = orderNumber;
            this.dishNumber = dishNumber;
            this.amount = amount;
        }

        public int orderNumber { get; set; }
        public int dishNumber { get; set; }
        public int amount { get; set; }

        public override string ToString()
        {
            string tmp = "";
            foreach (PropertyInfo property in this.GetType().GetProperties())
            {
                tmp += string.Format("{0}: {1}\n", property.Name, property.GetValue(this, new object[] { }));
            }
            return tmp;
        }

        //Assuming all properties are value type.
        public Ordered_Dish cloneOrdered_Dish()
        {
            return (Ordered_Dish)this.MemberwiseClone();
        }

    }
}
