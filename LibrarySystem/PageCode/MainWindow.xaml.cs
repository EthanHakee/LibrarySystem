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

        private void Manage_Loans_Click(object sender, RoutedEventArgs e)
        {
            
            Main.Content = new Pages.ManageLoans();
        }

        private void OverDue_Loans_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Pages.OverDueLoans();
        }

        private void Add_Items_And_Members_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Pages.Create();
        }
    }
}
