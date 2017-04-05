using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BE;
using System.IO;

namespace DS
{
    public class DataSource
    {
            static DataSource()
        {
            creatFile(dishPath, "Dishes");
            creatFile(orderPath, "Orders");
            creatFile(branchPath, "Branches");
            creatFile(orderedDishPath, "OrderedDishes");
        }
            public static XElement dishRoot;
            public static XElement orderRoot;
            public static XElement branchRoot;
            public static XElement orderedDishRoot;

            public static string dishPath = @"Dishes.xml";
            public static string orderPath = @"Orders.xml";
            public static string branchPath = @"Branches.xml";
            public static string orderedDishPath = @"OrderedDishes";

            public static XElement creatFile(string path, string rootName)
            {
                XElement tmp;
                if (!File.Exists(path))
                {

                    tmp = new XElement(rootName);
                    tmp.Save(path);
                    return tmp;
                }
                else
                {
                    tmp = loadData(path);
                    return tmp;
                }
            }

            public static XElement loadData(string path)
            {
                try
                {
                    XElement tmp;
                    tmp = XElement.Load(path);
                    return tmp;
                }
                catch
                {
                    throw new Exception("File upload problem");
                }
            }






            public static List<Dish> dishList = new List<Dish>();
        public static List<Order> orderList = new List<Order>();
        public static List<Branch> branchList = new List<Branch>();
        public static List<Ordered_Dish> order_dish_List = new List<Ordered_Dish>();
    }
}
