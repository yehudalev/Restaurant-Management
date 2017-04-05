using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BE;
using System.IO;
using System.Reflection;

namespace DAL
{
    class DalWithXml : Idal
    {
        static int orderCount;
        static int dishCount;
        static int branchCount;

        public static XElement myRoot;


        public static string dishPath = @"Dishes.xml";
        public static string orderPath = @"Orders.xml";
        public static string branchPath = @"Branches.xml";
        public static string orderedDishPath = @"OrderedDishes.xml";


        //singleton pattern implementation.
        private static readonly DalWithXml instance = new DalWithXml();

        static DalWithXml()
        {

        }
        public static DalWithXml Instance { get { return instance; } }

        private DalWithXml()
        {
            openFile("Dishes.xml", "Dishes");
            openFile("Orders.xml", "Orders");
            openFile("Branches.xml", "Branches");
            openFile("OrderedDishes.xml", "OrderedDishes");

            //to set the lastest number of number proporty. 
            orderCount = setCounter(orderPath);
            dishCount = setCounter(dishPath);
            branchCount = setCounter(branchPath);
        }
        #region open file
        public static XElement openFile(string path, string rootName)
        {
            XElement tmp;
            try
            {

                if (!File.Exists(path))
                {

                    tmp = new XElement(rootName);
                    tmp.Save(path);
                }
                else
                {
                    tmp = XElement.Load(path);
                }
                return tmp;
            }
            catch
            {
                throw new Exception("Couldn't open file");
            }
        }
        #endregion

        #region set the correct numbers for number's propoty 
        public int setCounter(string path)
        {
            int correctNumber = 0;
            myRoot = XElement.Load(path);
            foreach (XElement item in myRoot.Elements())
            {
                if (correctNumber < Convert.ToInt32(item.Element("number").Value))
                    correctNumber = Convert.ToInt32(item.Element("number").Value);
            }
            return ++correctNumber;

        }
        #endregion

        #region add function 

        public void addBeObject(Object inst, string path)
        {
            myRoot = XElement.Load(path);
            XElement propertyX;
            XElement classX = new XElement((inst.GetType()).ToString());
            foreach (PropertyInfo property in inst.GetType().GetProperties())
            {
                propertyX = new XElement(property.Name, property.GetValue(inst, new object[] { }));
                classX.Add(propertyX);
            }
            myRoot.Add(classX);
            myRoot.Save(path);
        }

        public void addBranch(Branch inst)
        {
            inst.number = branchCount++;
            myRoot = XElement.Load(branchPath);
            bool exist = myRoot.Elements().Any<XElement>(x => x.Element("name").Value == inst.name);
            if (!exist)
                addBeObject(inst, branchPath);
            else
                throw new Exception("branch with this name already exist");
        }

        public void addDish(Dish inst)
        {
            inst.number = dishCount++;
            myRoot = XElement.Load(dishPath);
            bool exist = myRoot.Elements().Any<XElement>(x => x.Element("name").Value == inst.name);
            if (!exist)
                addBeObject(inst, dishPath);
            else
                throw new Exception("dish with this name already exist");
        }

        public void addOrder(Order inst)
        {
            inst.number = orderCount++;
            myRoot = XElement.Load(orderPath);
            bool exist = myRoot.Elements().Any<XElement>(x => x.Element("name").Value == inst.name);
            if (!exist)
                addBeObject(inst, orderPath);
            else
                throw new Exception("order with this name already exist");
        }

        public void addOrdered_Dish(Ordered_Dish inst)
        {
            myRoot = XElement.Load(orderedDishPath);
            bool exist = myRoot.Elements().Any<XElement>(x => x.Element("orderNumber").Value == inst.orderNumber.ToString() && x.Element("dishNumber").Value == inst.dishNumber.ToString());
            if (!exist)
                addBeObject(inst, orderedDishPath);
            else
                throw new Exception("ordered dish already exist");
        }
        #endregion

        #region delete function
        public void deleteBranch(int x)
        {
            myRoot = XElement.Load(branchPath);
            XElement xbr = myRoot.Elements().FirstOrDefault(item => item.Element("number").Value == x.ToString());
            if (xbr != null)
            {
                xbr.Remove();
                myRoot.Save(branchPath);
            }
            else
                throw new Exception("Cannot delete. Instance doesn't exist");

        }

        //Just delete without wrapping logic.
        private void basicDishDelete(int x)
        {
            myRoot = XElement.Load(dishPath);
            XElement xbr = myRoot.Elements().FirstOrDefault(item => item.Element("number").Value == x.ToString());
            if (xbr != null)
            {
                xbr.Remove();
                myRoot.Save(dishPath);
            }
            else
                throw new Exception("Cannot delete. Instance doesn't exist");
        }

        public void basicOrderDelete(int x)
        {
            myRoot = XElement.Load(orderPath);
            XElement xbr = myRoot.Elements().FirstOrDefault(item => item.Element("number").Value == x.ToString());
            if (xbr != null)
            {
                xbr.Remove();
                myRoot.Save(orderPath);
            }
            else
                throw new Exception("Cannot delete. Instance doesn't exist");
        }

        public void deleteDish(int x)
        {
            //Check if there are still ordered dishes with this dish.
            myRoot = XElement.Load(orderedDishPath);
            if (myRoot.Elements().Any<XElement>(item => item.Element("dishNumber").Value == x.ToString()))
                throw new Exception("Cannot delete dish. It appears in ordered dishes.");

            basicDishDelete(x);
        }

        public void deleteOrder(int x)
        {
            myRoot = XElement.Load(orderedDishPath);
            if (myRoot.Elements().Any<XElement>(item => item.Element("orderNumber").Value == x.ToString()))
                throw new Exception("Cannot delete order. It appears in ordered dishes.");

            basicOrderDelete(x);

           
        }

        public void deleteOrdered_Dish(int orderNumber, int dishNumber)
        {
            myRoot = XElement.Load(orderedDishPath);
            XElement xbr = myRoot.Elements().FirstOrDefault(item => item.Element("orderNumber").Value == orderNumber.ToString() && item.Element("dishNumber").Value == dishNumber.ToString());
            if (xbr != null)
            {
                xbr.Remove();
                myRoot.Save(orderedDishPath);
            }
            else
                throw new Exception("Cannot delete. Instance doesn't exist");
        }

        #endregion


        #region get's functions
        public IEnumerable<Branch> getBranchList()
        {
            //foreach (XElement xBranch in myRoot.Elements())
            //{
            //    Branch myBranch = new Branch();
            //    foreach (PropertyInfo property in myBranch.GetType().GetProperties())
            //    {

            //        if (property.PropertyType == typeof(int))
            //        {
            //            property.SetValue(myBranch, int.Parse(xBranch.Element(property.Name).Value), null);
            //        }
            //        else
            //        if (property.PropertyType == typeof(string))
            //            property.SetValue(myBranch, xBranch.Element(property.Name).Value, null);
            //    }
            //    myList.Add(myBranch);
            //}
            myRoot = XElement.Load(branchPath);
            return from item in myRoot.Elements()
                   select new Branch()
                   {
                       name = item.Element("name").Value,
                       number = int.Parse(item.Element("number").Value),
                       branchKashrut = (KASHRUT)Enum.Parse(typeof(KASHRUT), item.Element("branchKashrut").Value),
                       address = item.Element("address").Value,
                       phoneNumber = int.Parse(item.Element("phoneNumber").Value),
                       manager = item.Element("manager").Value,
                       numberOfEmployees = int.Parse(item.Element("numberOfEmployees").Value),
                       availableDeliveryGuys = int.Parse(item.Element("availableDeliveryGuys").Value),
                   };
        }

        public IEnumerable<Dish> getDishList()
        {
            myRoot = XElement.Load(dishPath);
            return from item in myRoot.Elements()
                   select new Dish()
                   {
                       name = item.Element("name").Value,
                       number = int.Parse(item.Element("number").Value),
                       size = (SIZE)Enum.Parse(typeof(SIZE), item.Element("size").Value),
                       price = double.Parse(item.Element("price").Value),
                       kashrut = (KASHRUT)Enum.Parse(typeof(KASHRUT), item.Element("kashrut").Value),
                       discount = double.Parse(item.Element("discount").Value)
                   };
        }

        public IEnumerable<Ordered_Dish> getOrdered_DishList()
        {
            myRoot = XElement.Load(orderedDishPath);
            return from item in myRoot.Elements()
                   select new Ordered_Dish()
                   {
                       orderNumber = int.Parse(item.Element("orderNumber").Value),
                       dishNumber = int.Parse(item.Element("dishNumber").Value),
                       amount = int.Parse(item.Element("amount").Value)
                   };
        }

        public IEnumerable<Order> getOrderList()
        {



        
        //public DateTime date { get; set; }


        myRoot = XElement.Load(orderPath);
            return from item in myRoot.Elements()
                   select new Order()
                   {
                       name = item.Element("name").Value,
                       number = int.Parse(item.Element("number").Value),
                       requiredKashrut = (KASHRUT)Enum.Parse(typeof(KASHRUT), item.Element("requiredKashrut").Value),
                       city = item.Element("city").Value,
                       address = item.Element("address").Value,
                       place = item.Element("place").Value,
                       branchNumber = int.Parse(item.Element("branchNumber").Value),
                       creditCard = int.Parse(item.Element("creditCard").Value),
                       phoneNumber = int.Parse(item.Element("phoneNumber").Value),
                       age = int.Parse(item.Element("age").Value),
                       deliverQuickly = bool.Parse(item.Element("deliverQuickly").Value),
                       tipIncluded = bool.Parse(item.Element("tipIncluded").Value),
                       date = DateTime.Parse(item.Element("date").Value)
                   };
        }

        #endregion



        #region updates functions 
        public void updateBranch(Branch b)
        {
            myRoot = XElement.Load(branchPath);
            XElement xBr = myRoot.Elements().FirstOrDefault(x => x.Element("number").Value == b.number.ToString());
            if (xBr != null)
            {
                deleteBranch(b.number);
                addBeObject(b, branchPath);
            }
            else throw new Exception("Cannot update instance doesn't exist");
        }

        public void updateDish(Dish d)
        {
            myRoot = XElement.Load(dishPath);
            XElement xBr = myRoot.Elements().FirstOrDefault(x => x.Element("number").Value == d.number.ToString());
            if (xBr != null)
            {
                basicDishDelete(d.number);
                addBeObject(d, dishPath);
            }
            else throw new Exception("Cannot update instance doesn't exist");
        }

        public void updateOrder(Order o)
        {
            myRoot = XElement.Load(orderPath);
            XElement xBr = myRoot.Elements().FirstOrDefault(x => x.Element("number").Value == o.number.ToString());
            if (xBr != null)
            {
                basicOrderDelete(o.number);
                addBeObject(o, orderPath);
            }
            else throw new Exception("Cannot update instance doesn't exist");
        }

        public void updateOrdered_Dish(Ordered_Dish od)
        {
            myRoot = XElement.Load(orderedDishPath);
            XElement xBr = myRoot.Elements().FirstOrDefault(x => x.Element("dishNumber").Value == od.dishNumber.ToString() && x.Element("orderNumber").Value == od.orderNumber.ToString());
            if (xBr != null)
            {
                deleteOrdered_Dish(od.orderNumber,od.dishNumber);
                addBeObject(od, orderedDishPath);
            }
            else throw new Exception("Cannot update instance doesn't exist");
        }

        #endregion
    }
}
