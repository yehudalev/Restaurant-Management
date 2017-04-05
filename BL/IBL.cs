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
    public interface IBL
    {
        void addDish(Dish d);
        void deleteDish(int x);
        void updateDish(Dish d);       //Here and in all update methods, old instance is found by "d"'s number property.

        void addBranch(Branch b);
        void deleteBranch(int x);
        void updateBranch(Branch b);

        void addOrder(Order o);
        void deleteOrder(int x);
        void updateOrder(Order o);

        void addOrdered_Dish(Ordered_Dish od);
        void deleteOrdered_Dish(int orderNumber, int dishNumber);
        void updateOrdered_Dish(Ordered_Dish od);

        IEnumerable<Branch> getBranchList();
        IEnumerable<Dish> getDishList();
        IEnumerable<Order> getOrderList();
        IEnumerable<Ordered_Dish> getOrdered_DishList();

        //calculate cost of order [sum of price*amount over all dishes of order]
        double orderCost(int orderNumber);

        //Return dish with this number.
        Dish dishByNumber(int dishNumber);
        //return order with this number
        Order OrderByNumber(int OrderNumber);

        //get list of dishes in this order.
        IEnumerable<Dish> getorderinfo(Order or);

        //Return all orders\dishes satisfying a condition.
        IEnumerable<Order> OrderWhichIs(Predicate<Order> predicate);
        IEnumerable<Dish> DishWhichIs(Predicate<Dish> predicate);

        //Calculate profit made by a dish [amount*price over all orders with this dish]
        double dishProfit(Dish d);

        /*return a collection of groups
        each group has:
        key: string in the form xxx-xxx representing range of numbers e.g. "0-50", "50-100".
        items: a collection of dishAndProfit.
        dishAndProfit contains item1: Dish. item2: profit made by this dish.*/
        IEnumerable<IGrouping<string,dishAndProfit>> groupDishAndProfitByProfitRange();

        //Simple grouping by profit range.
        IEnumerable<IGrouping<string, Dish>> groupByProfitRange();

        IEnumerable<IGrouping<K, Dish>> groupDishBySomething<K>(Func<Dish, K> keyMaker);
        IEnumerable<IGrouping<K, Order>> groupOrderBySomething<K>(Func<Order, K> keyMaker);
        IEnumerable<IGrouping<string, Order>> groupByCity();

        //Return a list of profits made in month\address. 
        IEnumerable<monthAndProfit> profitsByMonth();
        IEnumerable<addressAndProfit> profitsByAcdress();
        
    }
}
