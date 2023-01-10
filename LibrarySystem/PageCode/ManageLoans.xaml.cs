using LibrarySystem.DataAccess;
using LibrarySystem.Models;
using LibrarySystem.ViewModels;
using System.Collections.Generic;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace LibrarySystem.Pages
{
    public partial class ManageLoans : Page
    {
        IEnumerable<Loan> ALL_LOANS = SqliteDataAccess.Load<Loan>();

        public ManageLoans()
        {
            InitializeComponent();
            LoanGrid.ItemsSource = PopulateGrid();
        }

        public List<DataGridLoan> PopulateGrid()
        {
            List<DataGridLoan> result = new();

            foreach (var item in ALL_LOANS)
            {
                DataGridLoan loan = new(item.Member, item.Item, item.DateOut, item.DateDue);
                result.Add(loan);
            }

            return result;
        }

        private void LoanGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGridLoan loan = (DataGridLoan)LoanGrid.SelectedItem;

            if (loan != null)
            {
                Loan_Out_Input.Text = loan.DateOut.ToString();
                ReturnDate.Text = loan.DateDue.ToString();
                Item_Title_Input.Text = loan.Item.ToString();
                Member_Name_Input.Text = loan.Member.ToString();
            }
        }

        public void updateDataGrid()
        {
            ALL_LOANS = SqliteDataAccess.Load<Loan>();
            LoanGrid.ItemsSource = null;
            LoanGrid.ItemsSource = PopulateGrid();
        }

        private void Delete_Loan_Submit(object sender, RoutedEventArgs e)
        {
            Loan loan = ALL_LOANS.Where(x => x.Item.Title == Item_Title_Input.Text).First();

            loan.Item.IsAvailable = true;
            SqliteDataAccess.Update(loan.Item);

            SqliteDataAccess.Delete(loan);
            updateDataGrid();
        }

        private void Update_Loan_Submit(object sender, RoutedEventArgs e)
        {
            Loan loan = ALL_LOANS.Where(x => x.Item.Title == Item_Title_Input.Text).First();
            loan.DateDue = Convert.ToDateTime(ReturnDate.Text);
            SqliteDataAccess.Update(loan);
            updateDataGrid();
        }
    }
}
