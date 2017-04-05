using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace BE
{
    public class Branch
    {
        public Branch() { }

        public Branch(int number, string name, string address, int phoneNumber, string manager, int numberOfEmployees, int availableDeliveryGuys, KASHRUT branchKashrut)
        {
            this.number = number;
            this.name = name;
            this.address = address;
            this.phoneNumber = phoneNumber;
            this.manager = manager;
            this.numberOfEmployees = numberOfEmployees;
            this.availableDeliveryGuys = availableDeliveryGuys;
            this.branchKashrut = branchKashrut;
        }

        public int number{get; set;}
        public string name { get; set; }      
        public string address { get; set; }
        public int phoneNumber { get; set; }
        public string manager { get; set; }
        public int numberOfEmployees { get; set; }
        public int availableDeliveryGuys { get; set; }
        public KASHRUT branchKashrut { get; set; }


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
        public Branch cloneBranch()
        {
            return (Branch)this.MemberwiseClone();
        }

    }
}
