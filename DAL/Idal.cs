using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public interface Idal
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
        void deleteOrdered_Dish(int orderNumber,int dishNumber );
        void updateOrdered_Dish(Ordered_Dish od);

       IEnumerable<Branch> getBranchList();
       IEnumerable<Dish> getDishList();
       IEnumerable<Order> getOrderList();
       IEnumerable<Ordered_Dish> getOrdered_DishList();

    }
}
