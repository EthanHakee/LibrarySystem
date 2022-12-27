using System;
using System.Windows;

namespace LibrarySystem
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Main.Content = new Pages.OverDueLoans();
        }

        private void Loan_Item_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Pages.CreateLoan();
        }

        private void Product_Search_Click(object sender, RoutedEventArgs e)
        {
            
            Main.Content = new Pages.ManageLoans();
        }

        private void Customer_Search_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Pages.OverDueLoans();
        }

        private void Add_Item_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Pages.AddItem();
        }
    }
}
