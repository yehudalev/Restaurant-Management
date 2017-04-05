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
using BE;

namespace PLForms
{
    /// <summary>
    /// Interaction logic for DishWindow.xaml
    /// </summary>
    public partial class AddDishWindow : Window
    {
        BL.IBL myIbl = new BL.Bl_imp();
        Dish myDish;
        public bool validInput;

        public AddDishWindow()
        {
            InitializeComponent();
            myDish = new Dish();
            this.DataContext = myDish;
            this.kashrutComboBox.ItemsSource = Enum.GetValues(typeof(BE.KASHRUT));
            this.sizeComboBox.ItemsSource = Enum.GetValues(typeof(BE.SIZE));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Check if input is ok.
            foreach (UIElement item in grid1.Children)
            {
                if (item is TextBox)
                {
                    TextBox myBox = item as TextBox;
                    //All text boxes were filled.
                    if (myBox.Text.Length == 0)
                    {
                        validInput = false;
                        myBox.BorderBrush = new SolidColorBrush(Colors.Red);
                    }
                    //Return original colour.
                    else
                        myBox.BorderBrush = SystemColors.ControlDarkBrush;
                }

                if (item is ComboBox)
                {
                    ComboBox myCombo = item as ComboBox;
                    if (myCombo.SelectedIndex == -1)
                    {
                        validInput = false;
                        myCombo.Background = new SolidColorBrush(Colors.LightPink);
                    }
                    else
                        myCombo.BorderBrush = SystemColors.ControlDarkBrush;
                }
            }

            if (!nameTextBox.Text.All(item => char.IsLetter(item) || item == ' '))
            {
                validInput = false;
                showErrorMessage(Grid.GetRow(nameTextBox), "Name can only contain letters");
            }
            else
                showErrorMessage(Grid.GetRow(nameTextBox), "");

            if (discountTextBox.Text != "")
                if (int.Parse(discountTextBox.Text) < 0 || int.Parse(discountTextBox.Text) > 100)
                {
                    validInput = false;
                    showErrorMessage(Grid.GetRow(discountTextBox), "Discount can only be between 0 and 100");
                }
                else
                showErrorMessage(Grid.GetRow(discountTextBox), "");

            if (priceTextBox.Text != "")
                if (int.Parse(priceTextBox.Text) < 0)
            {
                validInput = false;
                showErrorMessage(Grid.GetRow(priceTextBox), "Enter positive number");
            }
            else
                showErrorMessage(Grid.GetRow(priceTextBox), "");


            if (validInput)
            {
                myIbl.addDish(myDish);
                MessageBox.Show(string.Format("{0} added succesfully", myDish.name));
                myDish = new BE.Dish();
                this.DataContext = myDish;
            }
            else
                validInput = true;

        }

        //Shows error message in given row.
        public void showErrorMessage(int row, string message)
        {
            foreach (UIElement item in grid1.Children)
            {
                if (Grid.GetRow(item) == row && Grid.GetColumn(item) == 2 && item is TextBlock)
                {
                    ((TextBlock)item).Text = message;
                }
            }
        }
    }
}
