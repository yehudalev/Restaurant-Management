using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace debugXml
{
    class Program
    {

        static void Main(string[] args)
        //{
        //    Idal myIdal = DalFactory.getDal();
        //    Branch tmpBranch = new Branch();
        //    tmpBranch.name = "Jerusalem";
        //    tmpBranch.number = 100;


        //    myIdal.addBranch(tmpBranch);

        //    tmpBranch = new Branch();
        //    tmpBranch.name = "Tel Aviv";
        //    tmpBranch.branchKashrut = KASHRUT.Meadrin;
        //    tmpBranch.number = 200;

        //    myIdal.addBranch(tmpBranch);

            IEnumerable <Branch> myList= myIdal.getBranchList();
            Console.WriteLine();

            Dish tmpDish = new Dish();
            tmpDish.name = "pizza";
            tmpDish.number = 1;

            tmpDish = new Dish();
            tmpDish.name = "falafel";
            tmpDish.number = 2;

            myIdal.addDish(tmpDish);

            Order tmpOrder = new Order();
            tmpOrder.name = "Avi";
            tmpOrder.number = 10;
            tmpOrder.date = new DateTime(1992, 1, 1);

            tmpOrder = new Order();
            tmpOrder.name = "Ezra";
            tmpOrder.number = 20;

            Ordered_Dish tmpOrderedDish = new Ordered_Dish();
            tmpOrderedDish.orderNumber = 10;
            tmpOrderedDish.dishNumber = 1;


            tmpOrderedDish = new Ordered_Dish();
            tmpOrderedDish.orderNumber = 20;
            tmpOrderedDish.dishNumber = 2;

        }
    }
}
