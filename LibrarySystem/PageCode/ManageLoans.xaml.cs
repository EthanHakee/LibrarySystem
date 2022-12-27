using LibrarySystem.DataAccess;
using LibrarySystem.Models;
using System.ComponentModel;
using System.Windows;
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

        private void Delete_Loan_Submit(object sender, RoutedEventArgs e)
        {

        }
        private void Update_Loan_Submit(object sender, RoutedEventArgs e)
        {

        }



        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor propertyDescriptor = (PropertyDescriptor)e.PropertyDescriptor;
            e.Column.Header = propertyDescriptor.DisplayName;
            if (propertyDescriptor.DisplayName == "Id")
            {
                e.Cancel = true;
            }
        }
    }
}
