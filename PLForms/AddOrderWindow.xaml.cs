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
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class AddOrderWindow : Window
    {
        BL.IBL myIbl = new BL.Bl_imp();
        BE.Order myOrder;

        //used to notify of wrong input.
        public bool validInput=true;
        

        public AddOrderWindow()
        {
            InitializeComponent();
            myOrder = new BE.Order();
            this.DataContext = myOrder;
            myOrder.date=DateTime.Now;
            this.requiredKashrutComboBox.ItemsSource = Enum.GetValues(typeof(BE.KASHRUT));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            #region Validate Input
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

                if (creditCardTextBox.Text.Length != 4 || !creditCardTextBox.Text.All(char.IsDigit))
            {
                validInput = false;
                showErrorMessage(Grid.GetRow(creditCardTextBox), "Enter 4 digits");
            }
            else
                showErrorMessage(Grid.GetRow(creditCardTextBox), "");

            if (!nameTextBox.Text.All(item => char.IsLetter(item) || item == ' '))
            {
                validInput = false;
                showErrorMessage(Grid.GetRow(nameTextBox), "Name can only contain letters");
            }
            else
                showErrorMessage(Grid.GetRow(nameTextBox), "");
                #endregion

                if (validInput)
                try
                {
                    myIbl.addOrder(myOrder);
                    myOrder = new BE.Order();
                    this.DataContext = myOrder;
                    myOrder.date = DateTime.Now;
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            else
                validInput = true;
            
        }

        //Show error message in given row.
        public void showErrorMessage(int row,string message)
        {
            foreach(UIElement item in grid1.Children)
            {
                if (Grid.GetRow(item) == row && Grid.GetColumn(item) == 2 && item is TextBlock)
                {
                    ((TextBlock)item).Text = message;
                    break;
                }
            }
        }

    }
}
