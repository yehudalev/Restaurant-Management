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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace dotNetWpf_03_3202
{
    
    public partial class PrinterUserControl : UserControl
    {
        static int counter = 1;
        static Random random;
        public PrinterUserControl()
        {
            InitializeComponent();
            this.PrinterNameLabel.Content = "Printer " + counter.ToString();
            counter++;
            this.PageCountSlider.Value = MAX_PAGES;//usaly when you first installing a printer its come with full ink and pages.
            this.InkCountProgressBar.Value = MAX_INK;
        }

        public void Print()
        {            
            int left_pages = (int)this.PageCountSlider.Value;
            int pages_to_print = random.Next(0,MAX_PRINT_PAGES);

            int left_ink = (int)this.InkCountProgressBar.Value;
            int ink_to_print = random.Next(0, MAX_PRINT_INK);
        }

        public void AddPages()
        {

        }

        public void AddInk()
        {

        }
        
        EventHandler<printerEventArgs> PageMissing;
        EventHandler<printerEventArgs> InkEmpty;
        public string printerName { get; set; }
        public double InkCount { get; set; }
        public int pageCount { get; set; }
        const int MAX_INK = 100;
        const int MIN_ADD_INK = 10;
        const int MAX_PRINT_INK = 20;
        const int MAX_PAGES = 400;
        const int MIN_ADD_PAGES = 10;
        const int MAX_PRINT_PAGES = 100;//maximum pages to print.

    }
}
