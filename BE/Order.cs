using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace BE
{
    

    public class Order
    {
        public Order() { }

        public Order(int number, DateTime date, int branchNumber, KASHRUT requiredKashrut,string name,
            string city,string address, string place, int creditCard, int phoneNumber, bool deliverQuickly, bool tipIncluded, int age)
        {
            this.number = number;
            this.date = date;
            this.branchNumber = branchNumber;
            this.requiredKashrut = requiredKashrut;
            this.name = name;
            this.address = address;
            this.place = place;
            this.creditCard = creditCard;
            this.phoneNumber = phoneNumber;
            this.deliverQuickly = deliverQuickly;
            this.tipIncluded = tipIncluded;
            this.age = age;
            this.city = city;
        }

        public int number { get; set; }
        public string name { get; set; }
        public KASHRUT requiredKashrut { get; set; }
        public DateTime date{ get; set; }
        public int branchNumber { get; set; }       
        public string city { get; set; }
        public string address { get; set; }
        public string place { get; set; }
        public int creditCard { get; set; }
        public int phoneNumber { get; set; }
        public bool deliverQuickly { get; set; }
        public bool tipIncluded { get; set; }
        public int age { get; set; }
        

        public override string ToString()
        {
            string tmp="";
            foreach (PropertyInfo property in this.GetType().GetProperties())
            {
                tmp += string.Format("{0}: {1}\n", property.Name, property.GetValue(this,new object[] { }));
            }

            return tmp;
        }

        //Assuming all properties are value type.
        public Order cloneOrder()
        {
            return (Order)this.MemberwiseClone();
        }

    }
}
