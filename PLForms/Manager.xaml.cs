using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BE;

namespace PLForms
{
    #region converter
    public class ConvertFromDataGridSelectedToBoolian : IValueConverter
    {
        public object Convert(
          object value,
          Type targetType,
          object parameter,
          CultureInfo culture)
        {

            if ((int)value == -1)
                return false;
            else
                return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    #endregion


    /// <summary>
    /// Interaction logic for Manager.xaml
    /// </summary>
    public partial class Manager : Window
    {
        BL.IBL myIbl = new BL.Bl_imp();
        

        public Manager()
        {
            InitializeComponent();

            orderDataGrid.ItemsSource = myIbl.getOrderList();
            dishDataGrid.ItemsSource = myIbl.getDishList();
            IEnumerable<Branch> myList = myIbl.getBranchList();
            branchDataGrid.ItemsSource = myList;
        }
        
        #region Order Interface

        private void removeOrderButton(object sender, RoutedEventArgs e)
        { 
            try
            {
               int x = ((BE.Order)orderDataGrid.SelectedItem).number;
                myIbl.deleteOrder(x);
                orderDataGrid.ItemsSource = myIbl.getOrderList();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void UpdateOrderButton(object sender, RoutedEventArgs e)
        {
            Order myOrder = (BE.Order)orderDataGrid.SelectedItem;
            UpdateOrder myUpdateOrderWindow = new UpdateOrder(myOrder);
            myUpdateOrderWindow.ShowDialog();
            orderDataGrid.ItemsSource = myIbl.getOrderList();
        }
        #endregion


        #region Dish Interface
        private void addDishButton(object sender, RoutedEventArgs e)
        {
            AddDishWindow myDishWindow = new AddDishWindow();
            myDishWindow.ShowDialog();
            dishDataGrid.ItemsSource = myIbl.getDishList();
        }

        private void removeDishButton(object sender, RoutedEventArgs e)
        {
            try
            {
                int x = ((BE.Dish)dishDataGrid.SelectedItem).number;
                myIbl.deleteDish(x);
                dishDataGrid.ItemsSource = myIbl.getDishList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateDishButton(object sender, RoutedEventArgs e)
        {
            Dish myDish = (Dish)dishDataGrid.SelectedItem;
            UpdateDishWindow myWindow = new UpdateDishWindow(myDish);
            myWindow.ShowDialog();
            dishDataGrid.ItemsSource = myIbl.getDishList();

            
        }
        #endregion

        #region Branch Interface
        private void addBranchButton(object sender, RoutedEventArgs e)
        {
            AddBranchWindow myBranchWindow = new AddBranchWindow();
            myBranchWindow.ShowDialog();
            branchDataGrid.ItemsSource = myIbl.getBranchList();
        }

        private void removeBranchButton(object sender, RoutedEventArgs e)
        {
            try
            {
                int x = ((BE.Branch)branchDataGrid.SelectedItem).number;
                myIbl.deleteBranch(x);
                branchDataGrid.ItemsSource = myIbl.getBranchList();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateBranchButton(object sender, RoutedEventArgs e)
        {
            Branch myBranch = (Branch)branchDataGrid.SelectedItem;
            UpdateBranchWindow myWindow = new UpdateBranchWindow(myBranch);
            myWindow.ShowDialog();
            branchDataGrid.ItemsSource = myIbl.getBranchList();
        }


        #endregion

    }
}
