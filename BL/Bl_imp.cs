using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

using dishAndProfit = System.Tuple<BE.Dish, double>;
using monthAndProfit = System.Tuple<int, double>;
using addressAndProfit = System.Tuple<string, double>;

namespace BL
{
    public class Bl_imp : IBL
    {
        //DAL connection for BL.
        DAL.Idal myDal = DAL.DalFactory.getDal();

        //oredered_Dish related constant.
        private static readonly double MaxOrderPrice = 450;

        #region Reading, deleting and updating functions.
        public void addBranch(Branch b)
        {
            myDal.addBranch(b);
        }

        public void addDish(Dish d)
        {
            myDal.addDish(d);
        }

        public void addOrder(Order o)
        {
            if (o.age < 18)
                throw new Exception("Customer is under 18");
            myDal.addOrder(o);
        }

        public void addOrdered_Dish(Ordered_Dish od)
        {
            Dish tmpDish = dishByNumber(od.dishNumber);
            Order tmpOrder = OrderByNumber(od.orderNumber);
            if (tmpDish == null)
                throw new Exception("Adding ordered dish for dish that doesn't exist");
            if (tmpOrder == null)
                throw new Exception("Adding ordered dish for order that doesn't exist");

            //Check if kashrut level is sufficient.
            if (tmpDish.kashrut < tmpOrder.requiredKashrut)
                throw new Exception("Dish kashrut is too low");

            //Calculate if total cost up to now +cost of current order exceeds max.
            double currentPrice = orderCost(od.orderNumber);
            double newOrderPrice = tmpDish.price * od.amount;
            if (currentPrice + newOrderPrice > MaxOrderPrice)
                throw new Exception("Order price can't be above " + MaxOrderPrice);
            myDal.addOrdered_Dish(od);

        }

        public void deleteBranch(int x)
        {
            myDal.deleteBranch(x);
        }

        public void deleteDish(int x)
        {
            myDal.deleteDish(x);
        }

        public void deleteOrder(int x)
        {
            myDal.deleteOrder(x);
        }

        public void deleteOrdered_Dish(int orderNumber, int dishNumber)
        {
            myDal.deleteOrdered_Dish(orderNumber, dishNumber);
        }

        public Dish dishByNumber(int dishNumber)
        {
            IEnumerable<Dish> myList = myDal.getDishList();
            return myList.FirstOrDefault(d => d.number == dishNumber);
        }

        public Order OrderByNumber(int OrderNumber)
        {
            IEnumerable<Order> myList = myDal.getOrderList();
            return myList.FirstOrDefault(d => d.number == OrderNumber);
        }

        public IEnumerable<Branch> getBranchList()
        {
            return myDal.getBranchList();
        }

        public IEnumerable<Dish> getDishList()
        {
            return myDal.getDishList();
        }

        public IEnumerable<Ordered_Dish> getOrdered_DishList()
        {
            return myDal.getOrdered_DishList();
        }

        public IEnumerable<Order> getOrderList()
        {
            return myDal.getOrderList();
        }

        public void updateBranch(Branch b)
        {
            myDal.updateBranch(b);
        }

        public void updateDish(Dish d)
        {
            myDal.updateDish(d);
        }

        public void updateOrder(Order o)
        {
            myDal.updateOrder(o);
        }

        public void updateOrdered_Dish(Ordered_Dish od)
        {
            myDal.updateOrdered_Dish(od);
        }

        #endregion


        #region Dish related methods.

        /* 
        Function gets a delegate keyMaker.
        It produces a key for each item by using keyMaker on it.
        Because keys can have different types, the function is generic. 
        Example of use: groupDishBySomething<SIZE>(x=>x.size);
        */
        public IEnumerable<IGrouping<K, Dish>> groupDishBySomething<K>(Func<Dish, K> keyMaker)
        {
            var v = from item in myDal.getDishList()
                    group item by keyMaker(item) into g
                    select g;
            return v;
        }

       

        //A. count the times dish was ordered B. multiply by price.
        public double dishProfit(Dish d)
        {
            IEnumerable<Ordered_Dish> myList = myDal.getOrdered_DishList();
            int timesOrdered = myList.Where(item => item.dishNumber == d.number).Sum(x => x.amount);
            return timesOrdered * d.price;
        }

        /*for each dish
        A. calculate its profit and its profit's range (let)
        B. create an object containing information about the dish and its profit (group)
        C. add this object to a group whose key is a -string- representing -range- (by)
        הגישה המקובלת היא ליצור אובייקט אנונימי. הבעיה היא  שאי אפשר להחזיר אובייקט אנונימי מפונקציה
        ניתן היה ליצור מבנה המכיל "מנה" ו"רווח". במקום זאת השתמשנו במחלקה קיימת המיועדת לייצוג זוגות 
        notice that dish and profit is an alias for Tuple<Dish,Double> (see using directives)
        */
        public IEnumerable<IGrouping<string, dishAndProfit>> groupDishAndProfitByProfitRange()
        {
            IEnumerable<Dish> myList = myDal.getDishList();
            var v = from item in myList
                    let profit = dishProfit(item)
                    let range = profit - profit % 50
                    group new dishAndProfit(item, profit) by (string.Format("{0} - {1}", range, range + 50)) into g
                    select g;
            return v;
        }

        public IEnumerable<IGrouping<string, Dish>> groupByProfitRange()
        {
            IEnumerable<Dish> myList = myDal.getDishList();
            var v = from item in myList
                    let profit = dishProfit(item)
                    let range = profit - profit % 50
                    group item by (string.Format("{0} - {1}", range, range + 50)) into g
                    select g;
            return v;
        }

        public IEnumerable<Dish> DishWhichIs(Predicate<Dish> predicate)
        {
            IEnumerable<Dish> myList = myDal.getDishList();
            var v = from item in myList
                    where predicate(item)
                    select item;
            return v;
        }

        #endregion


        #region order related methods.

        public IEnumerable<Dish> getorderinfo(Order or)
        {
            IEnumerable<Ordered_Dish> myList = myDal.getOrdered_DishList();
            var v = from item in myList
                    where (item.orderNumber == or.number)
                    let dishItem = dishByNumber(item.dishNumber)
                    where dishItem != null
                    select dishItem;
            return v;
        }

        public IEnumerable<Order> OrderWhichIs(Predicate<Order> predicate)
        {
            IEnumerable<Order> myList = myDal.getOrderList();
            var v = from item in myList
                    where predicate(item)
                    select item;
            return v;
        }
      
        /*
        A. create a collection with the profit of every item (factor in the discount)
        B. Run a sum over all profits. 
        */
        public double orderCost(int orderNumber)
        {
            IEnumerable<Ordered_Dish> myList = myDal.getOrdered_DishList();
            var dishesPrices = from item in myList
                               where item.orderNumber == orderNumber
                               let tmpDish = (dishByNumber(item.dishNumber))
                               select tmpDish.price * item.amount * tmpDish.discount / 100;
            double totalCost = dishesPrices.Sum();
            //Sunday discount
            if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                totalCost *= 0.95;
            return totalCost;
        }

        //See comment for groupDishBySomething
        public IEnumerable<IGrouping<K, Order>> groupOrderBySomething<K>(Func<Order, K> keyMaker)
        {
            var v = from item in myDal.getOrderList()
                    group item by keyMaker(item) into g
                    select g;
            return v;
        }

        public IEnumerable<IGrouping<string, Order>> groupByCity()
        {
            return groupOrderBySomething<string>(x => x.city);
        }

        #endregion

        #region profits functions.
        public IEnumerable<monthAndProfit> profitsByMonth()
        {
            IEnumerable<Order> myList = myDal.getOrderList();
            var v = from item in myList
                    group item by item.date.Month into g
                    select g;
            var s = from item in v
                    let profit = item.Sum(x => orderCost(x.number))
                    select new monthAndProfit(item.Key, profit);
            return s;
        }

        public IEnumerable<addressAndProfit> profitsByAcdress()
        {
            IEnumerable<Order> myList = myDal.getOrderList();
            var v = from item in myList
                    group item by item.address into g
                    select g;
            var s = from item in v
                    let profit = item.Sum(x => orderCost(x.number))
                    select new addressAndProfit(item.Key, profit);
            return s;
        }
        #endregion



    }


}
