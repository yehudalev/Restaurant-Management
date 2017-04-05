using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using BL;

namespace PL
{
    class PL_Methods
    {
        public static void printList<T>(IEnumerable<T> myList)
        {
            foreach (T item in myList)
                Console.WriteLine(item.ToString());
        }
    }

    static class Program
    {
        static void Main(string[] args)
        {
            Bl_imp myBl = new Bl_imp();
            IEnumerable<Branch> myBranchList = myBl.getBranchList();
            IEnumerable<Order> myOrderList = myBl.getOrderList();
            IEnumerable<Dish> myDishList = myBl.getDishList();
            IEnumerable<Ordered_Dish> myOrdered_dishList = myBl.getOrdered_DishList();
            while (true)
            {
                try
                {
                    Console.WriteLine("Press:\n1. Dish\n2. Branch\n3. Order\n4. Ordered Dish\n5. group Dishes By Profit range\n6. Calculate order Cost\n7. print everything");
                    int choice = int.Parse(Console.ReadLine());
                    string name;
                    int number, orderNumber, dishNumber, amount;
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("1. to Add\n2. to remove\n3. to update");
                            int choice_1 = int.Parse(Console.ReadLine());
                            switch (choice_1)
                            {
                                case 1:
                                    Console.WriteLine("Enter name of item: ");
                                    name = Console.ReadLine();
                                    Dish a = new Dish(0, name, SIZE.Small, 10, KASHRUT.Rabanoot);
                                    myBl.addDish(a);
                                    break;
                                case 2:
                                    Console.WriteLine("Enter number of item: ");
                                    number = int.Parse(Console.ReadLine());
                                    myBl.deleteDish(number);
                                    break;
                                case 3:
                                    Console.WriteLine("Enter number of item: ");
                                    number = int.Parse(Console.ReadLine());
                                    Dish b = new Dish(number, "falae", SIZE.Big, 20, KASHRUT.Rabanoot);
                                    myBl.updateDish(b);
                                    break;
                            }
                            break;
                        case 2:
                            Console.WriteLine("1. to Add\n2. to remove\n3. to update");
                            int choice_2 = int.Parse(Console.ReadLine());
                            switch (choice_2)
                            {
                                case 1:
                                    Console.WriteLine("Enter name of item: ");
                                    name = Console.ReadLine();
                                    Branch a = new Branch(0, name, "Jafo", 055, "avi", 10, 5, KASHRUT.Rabanoot);
                                    myBl.addBranch(a);
                                    break;
                                case 2:
                                    Console.WriteLine("Enter number of item: ");
                                    number = int.Parse(Console.ReadLine());
                                    myBl.deleteBranch(number);
                                    break;
                                case 3:
                                    Console.WriteLine("Enter number of item: ");
                                    number = int.Parse(Console.ReadLine());
                                    Branch b = new Branch(number, "Jeru", "Jafo", 055, "avi", 10, 5, KASHRUT.Lando);
                                    myBl.updateBranch(b);
                                    break;
                            }
                            break;
                        case 3:
                            Console.WriteLine("1. to Add\n2. to remove\n3. to update");
                            int choice_3 = int.Parse(Console.ReadLine());
                            switch (choice_3)
                            {
                                case 1:
                                    Console.WriteLine("Enter name of item: ");
                                    name = Console.ReadLine();
                                    Order a = new Order(0, DateTime.Now, 123, KASHRUT.Rabanoot, name, "al", "kk", "hhh", 123456789, 058, true, true, 19);
                                    myBl.addOrder(a);
                                    break;
                                case 2:
                                    Console.WriteLine("Enter number of item: ");
                                    number = int.Parse(Console.ReadLine());
                                    myBl.deleteOrder(number);
                                    break;
                                case 3:
                                    Console.WriteLine("Enter number of item: ");
                                    number = int.Parse(Console.ReadLine());
                                    Order b = new Order(number, DateTime.Now, 123, KASHRUT.Rabanoot, "yosi", "al", "kk", "hhh", 987654321, 058, true, false, 23);
                                    myBl.updateOrder(b);
                                    break;
                            }
                            break;
                        case 4:
                            Console.WriteLine("1. to Add\n2. to remove\n3. to update");
                            int choice_4 = int.Parse(Console.ReadLine());
                            switch (choice_4)
                            {
                                case 1:
                                    Console.WriteLine("Enter number of order: ");
                                    orderNumber = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter number of dish: ");
                                    dishNumber = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter amount: ");
                                    amount = int.Parse(Console.ReadLine());
                                    Ordered_Dish a = new Ordered_Dish(orderNumber, dishNumber, amount);
                                    myBl.addOrdered_Dish(a);
                                    break;
                                case 2:
                                    Console.WriteLine("Enter number of order: ");
                                    orderNumber = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter number of dish: ");
                                    dishNumber = int.Parse(Console.ReadLine());
                                    myBl.deleteOrdered_Dish(orderNumber, dishNumber);
                                    break;
                                case 3:
                                    Console.WriteLine("Enter number of order: ");
                                    orderNumber = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter number of dish: ");
                                    dishNumber = int.Parse(Console.ReadLine());
                                    Ordered_Dish b = new Ordered_Dish(orderNumber, dishNumber, 5);
                                    myBl.updateOrdered_Dish(b);
                                    break;
                            }
                            break;
                        case 5:
                            foreach (var item in myBl.groupDishByProfitRange())
                            {
                                Console.WriteLine("Range: {0}", item.Key);
                                foreach (var inItem in item)
                                    Console.WriteLine("{0} has made {1} profit", inItem.Item1.name, inItem.Item2);
                            }
                            break;
                        case 6:
                            Console.WriteLine("Enter order number");
                            number = int.Parse(Console.ReadLine());
                            Console.WriteLine( myBl.orderCost(number));
                            break;
                        case 7:
                            PL_Methods.printList(myBranchList);
                            PL_Methods.printList(myDishList);
                            PL_Methods.printList(myOrderList);
                            PL_Methods.printList(myOrdered_dishList);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error "+ex.Message);
                }
            }
        }
    }
}



