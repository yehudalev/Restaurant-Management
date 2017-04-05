using System;
using System.Collections.Generic;
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

namespace PLForms
{
    /// <summary>
    /// Interaction logic for statistics.xaml
    /// </summary>
    public partial class statistics : Window
    {
        BL.IBL MyIbl = new BL.Bl_imp();

        public statistics()
        {
            InitializeComponent();

        }
        private void ordersComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem myItem = (ComboBoxItem)ordersComboBox.SelectedItem;
            string selected = (string)myItem.Content;
            switch (selected)
            {
                case "Profit Range":
                    {
                        
                    }
                    break;
                case "City":
                    {
                        orderListBox.ItemsSource = MyIbl.groupByCity();
                    }
                    break;
                case "Month":
                    {
                        //orderListBox.ItemsSource = MyIbl.groupOrderBySomething<string>(item => item.name.Substring(0, 2));
                    }
                    break;


            }
        }

        private void disheshComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBoxItem myItem = (ComboBoxItem)disheshComboBox.SelectedItem;
            string selected = (string)myItem.Content;
            switch (selected)
            {
                case "Kashrut":
                    {
                        dishListBox.ItemsSource = MyIbl.groupDishBySomething<string>(item => item.kashrut.ToString());
                    }
                    break;
                case "Size":
                    {
                        dishListBox.ItemsSource = MyIbl.groupDishBySomething(item => item.size);
                    }
                    break;
                case "Item 3":
                    {
                        //orderListBox.ItemsSource = MyIbl.groupOrderBySomething<string>(item => item.name.Substring(0, 2));
                    }
                    break;
            }
        }
    }
}
