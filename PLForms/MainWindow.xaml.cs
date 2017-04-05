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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BE;

namespace PLForms
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BL.IBL myIbl = new BL.Bl_imp();
        Ordered_Dish myOrderedDish;
        Ordered_Dish OdToAdd;
        public bool buttonsEnabled;

        public MainWindow()
        {
            myOrderedDish = new Ordered_Dish(-1, -1, 0);
            InitializeComponent();

            ordersListBox.ItemsSource = myIbl.getOrderList();
            ordersListBox.DisplayMemberPath = "name";
            dishesComboBox.ItemsSource = myIbl.getDishList();
            dishesComboBox.DisplayMemberPath = "name";

            DataContext = myOrderedDish;
        }

        private void AddOrderClick(object sender, RoutedEventArgs e)
        {
            AddOrderWindow myOrderWindow = new AddOrderWindow();
            myOrderWindow.ShowDialog();
        }

        private void SearchClick(object sender, RoutedEventArgs e)
        {
            string searched = searchTextBox.Text;
            ordersListBox.ItemsSource = myIbl.OrderWhichIs(x => string.Compare(x.name, 0, searched, 0, searched.Length) == 0);
        }

        private void orderListBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            //New order chosen
            if (ordersListBox.SelectedItem != null)
            {
                orderPickExpander.IsExpanded = false;

                Order or = (Order)ordersListBox.SelectedItem;
                dishToOrderDataGrid.ItemsSource = myIbl.getorderinfo(or);
                //Update name in text block according to chosen order.
                nameTextBlock.Text = myIbl.OrderByNumber((int)ordersListBox.SelectedValue).name;
            }

        }

        private void ChooseClick(object sender, RoutedEventArgs e)
        {
            ////update order number in myOrder.
            //myOrderedDish.orderNumber = (int)ordersListBox.SelectedValue;

            //orderPickExpander.IsExpanded = false;
            //dishesComboBox.IsEnabled = true;

            //Order or = (Order)ordersListBox.SelectedItem;
            //dishToOrderDataGrid.ItemsSource = myIbl.getorderinfo(or);

            ////Update name in text block according to chosen order.
            //nameTextBlock.Text = myIbl.OrderByNumber(myOrderedDish.orderNumber).name;

        }

        private void searchBoxTextChanged(object sender, RoutedEventArgs e)
        {
            if (searchTextBox.Text == "")
                ordersListBox.ItemsSource = myIbl.getOrderList();
            else

            {
                string searched = searchTextBox.Text;
                ordersListBox.ItemsSource = myIbl.OrderWhichIs(x => string.Compare(x.name, 0, searched, 0, searched.Length) == 0);
            }
        }


        private void addDishToOrderButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OdToAdd = new Ordered_Dish(myOrderedDish.orderNumber, myOrderedDish.dishNumber, int.Parse(dishAmountTextBox.Text));
                myIbl.addOrdered_Dish(OdToAdd);

                Order tmpOd = new Order();
                tmpOd.number = myOrderedDish.orderNumber;
                IEnumerable<Dish> myList = myIbl.getorderinfo(tmpOd);
                dishToOrderDataGrid.ItemsSource = myList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void managerClick(object sender, RoutedEventArgs e)
        {
            Manager myManager = new Manager();
            myManager.ShowDialog();
            refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddOrderWindow myAddWindow = new AddOrderWindow();
            myAddWindow.ShowDialog();
            refresh();
        }


        public void refresh()
        {
            dishesComboBox.ItemsSource = myIbl.getDishList();
            ordersListBox.ItemsSource = myIbl.getOrderList();
            refreshDataGrid();
            nameTextBlock.Text = "";
        }

        private void statisticsClick(object sender, RoutedEventArgs e)
        {
            statistics myWindow = new statistics();
            myWindow.ShowDialog();
        }

        private void removeClick(object sender, RoutedEventArgs e)
        {
            myIbl.deleteOrdered_Dish(myOrderedDish.orderNumber, (int)dishToOrderDataGrid.SelectedValue);
            Order tmp = new Order();
            tmp.number = (int)ordersListBox.SelectedValue;
            dishToOrderDataGrid.ItemsSource = myIbl.getorderinfo(tmp);
        }

        public void refreshDataGrid()
        {
            if (ordersListBox.SelectedIndex != -1)
                dishToOrderDataGrid.ItemsSource = myIbl.getorderinfo((Order)ordersListBox.SelectedItem);
            else
                dishToOrderDataGrid.ItemsSource = null;
        }

        private void dishAmountPlusButton_Click(object sender, RoutedEventArgs e)
        {
            int x = int.Parse(dishAmountTextBox.Text) + 1;
            dishAmountTextBox.Text = x.ToString();
        }

        private void dishAmountMinusButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(dishAmountTextBox.Text) > 0)
            {
                    int x = int.Parse(dishAmountTextBox.Text) - 1;
                    dishAmountTextBox.Text = x.ToString();
               
            }

        }

        private void updateDishAmountPlusButton_Click(object sender, RoutedEventArgs e)
        {
            int x = int.Parse(updateDishAmountTextBox.Text) + 1;
            updateDishAmountTextBox.Text = x.ToString();
        }

        private void updateDishAmountMinusButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(updateDishAmountTextBox.Text) > 0)
            {
                int x = int.Parse(updateDishAmountTextBox.Text) - 1;
                updateDishAmountTextBox.Text = x.ToString();
            }
        }

        private void updateAmountClick(object sender, RoutedEventArgs e)
        {
            try
            {
                int orNumber = (int)ordersListBox.SelectedValue;
                int diNumber = ((Dish)dishToOrderDataGrid.SelectedItem).number;
                int amount = int.Parse(updateDishAmountTextBox.Text);
                myIbl.updateOrdered_Dish(new Ordered_Dish(orNumber, diNumber, amount));
                refreshDataGrid();
                updateDishAmountTextBox.Text = "0";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    public class toAmountConverter : IMultiValueConverter
    {
        BL.IBL myIbl = new BL.Bl_imp();
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int oNum = (int)values[0];
            int dNum = (int)values[1];
            Ordered_Dish tmp = myIbl.getOrdered_DishList().FirstOrDefault(item => item.orderNumber == oNum && item.dishNumber == dNum);
            if (tmp != null)
                return (tmp.amount).ToString();
            else
                return -1;

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    //Check in two lists if something has been chosen.
    public class indexToButtonConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            //Check if order dish and amount are all picked.
            if ((int)values[0] != -1 && (int)values[1] != -1)
                return true;
            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

    //Check if any item in list has been chosen.
    public class oneIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value!=-1)
                return true;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
