
//צריך לסיים פונקציות הוספה
// לבדוק אם המחיקות עובדות
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BE;
using DS;


namespace DAL
{
    public sealed class Dal_imp : Idal
    {
        // 
        static int orderCount;
        static int dishCount;
        static int branchCount;


        //singleton pattern implementation.
        private static readonly Dal_imp instance = new Dal_imp();

        private Dal_imp()
        {
            
        }
        static Dal_imp()
        {
           
        } 
        public  static Dal_imp Instance { get { return instance; }}




        //Adding functions.
        //As a rule there can't be two items with the same name.


        public void addOrder(Order b)
        {
            Order or = DataSource.orderList.FirstOrDefault(item => item.name == b.name);
            if (or != null)
                throw new Exception("Order with this number already exists");
            b.number = orderCount++;
            DataSource.orderList.Add(b);
        }

        public void addDish(Dish b)
        {
            Dish d = DataSource.dishList.FirstOrDefault(item => item.name == b.name);
            if (d != null)
                throw new Exception("Dish with this number already exists");
            b.number = dishCount++;
            DataSource.dishList.Add(b);
        }

        public void addBranch(Branch b)
        {
            Branch bran = DataSource.branchList.FirstOrDefault(item => item.name == b.name);
            if (bran != null)
                throw new Exception("Branch with this number already exists");
            b.number = branchCount++;
            DataSource.branchList.Add(b);

        }

        public void addOrdered_Dish(Ordered_Dish b)
        {
            //Check if dish and order both exist
            Order tmpOrder = DataSource.orderList.FirstOrDefault(x => x.number == b.orderNumber);
            if (tmpOrder == null)
                throw new Exception("Adding ordered dish for order that doesn't exist");
            Dish tmpDish= DataSource.dishList.FirstOrDefault(x => x.number == b.dishNumber);
            if(tmpDish==null)
                throw new Exception("Adding ordered dish for dish that doesn't exist");


            Ordered_Dish or = DataSource.order_dish_List.FirstOrDefault(item => item.dishNumber == b.dishNumber && 
            item.orderNumber == b.dishNumber);
            if (or != null)
                throw new Exception("Trying to add ordered dish which already exist. To change amount use update functions");
            DataSource.order_dish_List.Add(b);
        }

        //Deleting functions.
        public void deleteBranch(int x)
        {
            Branch b = DataSource.branchList.FirstOrDefault(item => item.number == x);
            if (b == null)
                throw new Exception("Cannot delete branch doesn't exist");
            //b contains a reference to the object that needs to be deleted.
            DataSource.branchList.Remove(b);
        }

        public void deleteDish(int x)
        {
            Dish b = DataSource.dishList.FirstOrDefault(item => item.number == x);
            if (b == null)
                throw new Exception("Cannot delete dish doesn't exist");
            //b contains a reference to the object that needs to be deleted.
            DataSource.dishList.Remove(b);
        }

        public void deleteOrder(int x)
        {
            Order b = DataSource.orderList.FirstOrDefault(item => item.number == x);
            if (b == null)
                throw new Exception("Cannot delete order doesn't exist");
            //b contains a reference to the object that needs to be deleted.
            DataSource.orderList.Remove(b);
        }

        public void deleteOrdered_Dish(int orderNumber,int dishNumber)
        {
            Ordered_Dish b = DataSource.order_dish_List.FirstOrDefault(item => item.dishNumber == dishNumber&&item.orderNumber==orderNumber);
            if (b == null)
                throw new Exception("Cannot delete ordered dish doesn't exist");
            //b contains a reference to the object that needs to be deleted.
            DataSource.order_dish_List.Remove(b);
        }

        public void updateBranch(Branch b)
        {
            int x = b.number;
            Branch br = DataSource.branchList.FirstOrDefault(item => item.number == x);
            if (br == null)
                throw new Exception("Branch doesn't exist");
            DataSource.branchList.Remove(br);
            DataSource.branchList.Add(b);
        }

        public void updateDish(Dish d)
        {
            int x = d.number;
            Dish di = DataSource.dishList.FirstOrDefault(item => item.number == x);
            if (di == null)
                throw new Exception("Dish doesn't exist");
            DataSource.dishList.Remove(di);
            DataSource.dishList.Add(d);
        }

        public void updateOrder(Order o)
        {
            int x=o.number;
            Order _o = DataSource.orderList.FirstOrDefault(item => item.number == x);
            if (_o == null)
                throw new Exception("Order doesn't exist");
            DataSource.orderList.Remove(_o);
            DataSource.orderList.Add(o);
        }

        public void updateOrdered_Dish(Ordered_Dish od)
        {
            int dishNumber = od.dishNumber, orderNumber = od.orderNumber;
            Ordered_Dish _od = DataSource.order_dish_List.FirstOrDefault(item => item.dishNumber == dishNumber && item.orderNumber == orderNumber);
            if (_od == null)
                throw new Exception("Ordered dish doesn't exist");
            _od.amount = od.amount;
        }

        public IEnumerable<Branch> getBranchList()
        {
            return new ReadOnlyCollection<Branch>(DataSource.branchList);
        }

        public IEnumerable<Dish> getDishList()
        {
            return new ReadOnlyCollection<Dish>(DataSource.dishList);
        }

        public IEnumerable<Ordered_Dish> getOrdered_DishList()
        {
            return new ReadOnlyCollection<Ordered_Dish>(DataSource.order_dish_List);
        }

        public IEnumerable<Order> getOrderList()
        {
            return new ReadOnlyCollection<Order>(DataSource.orderList);
        }
    }
}
