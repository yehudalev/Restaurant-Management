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
    /// Interaction logic for UpdateBranchWindow.xaml
    /// </summary>
    public partial class UpdateBranchWindow : Window
    {
        BL.IBL myIbl = new BL.Bl_imp();
        Branch oldBranch;
        Branch newBranch;
        public bool validInput = true;

        public UpdateBranchWindow(Branch inputBranch)
        {
            InitializeComponent();
            oldBranch = inputBranch;
            newBranch = oldBranch.cloneBranch();
            DataContext = newBranch;

            branchKashrutComboBox.ItemsSource = Enum.GetValues(typeof(BE.KASHRUT));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            #region validate input
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

            if (!managerTextBox.Text.All(item => char.IsLetter(item) || item == ' '))
            {
                validInput = false;
                showErrorMessage(Grid.GetRow(managerTextBox), "Name can only contain letters");
            }
            else
                showErrorMessage(Grid.GetRow(managerTextBox), "");

            if (availableDeliveryGuysTextBox.Text != "")
                if (int.Parse(availableDeliveryGuysTextBox.Text) < 0)
                {
                    validInput = false;
                    showErrorMessage(Grid.GetRow(availableDeliveryGuysTextBox), "Enter positive number");
                }
                else
                    showErrorMessage(Grid.GetRow(availableDeliveryGuysTextBox), "");

            if (numberOfEmployeesTextBox.Text != "")
                if (int.Parse(numberOfEmployeesTextBox.Text) < 0)
                {
                    validInput = false;
                    showErrorMessage(Grid.GetRow(numberOfEmployeesTextBox), "Enter positive number");
                }
                else
                    showErrorMessage(Grid.GetRow(numberOfEmployeesTextBox), "");
            #endregion

            if (validInput)
            {
                myIbl.updateBranch(newBranch);
                newBranch = new Branch();
                Close();
            }
            else
                validInput = false;
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
