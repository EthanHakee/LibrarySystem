using LibrarySystem.DataAccess;
using LibrarySystem.Models;
using System.Windows.Controls;

namespace LibrarySystem.Pages
{
    public partial class ManageLoans : Page
    {
        public ManageLoans()
        {
            InitializeComponent();

            var name = SqliteDataAccess.LoadLoans();
            LoanGrid.ItemsSource = name;
        }

        private void LoanGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Loan loan = (Loan)LoanGrid.SelectedItem;
            Loan_Out_Input.Text = loan.DateDue.ToString();
        }
    }
}
