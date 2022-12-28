using LibrarySystem.DataAccess;
using LibrarySystem.Models;
using LibrarySystem.ViewModels;
using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace LibrarySystem.Pages
{
    public partial class ManageLoans : Page
    {
        IEnumerable<Loan> ALL_LOANS = SqliteDataAccess.LoadLoans();
        IEnumerable<Member> ALL_MEMBERS = SqliteDataAccess.LoadMembers();
        public ManageLoans()
        {
            InitializeComponent();
            LoanGrid.ItemsSource = PopulateGrid();
        }

        public List<DataGridLoan> PopulateGrid()
        {
            List<DataGridLoan> result = new List<DataGridLoan>();

            foreach (var item in ALL_LOANS)
            {
                DataGridLoan loan = new DataGridLoan(item.Member, item.Item, item.DateOut, item.DateDue);
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
            ALL_LOANS = SqliteDataAccess.LoadLoans();
            LoanGrid.ItemsSource = null;
            LoanGrid.ItemsSource = PopulateGrid();
        }

        private void Delete_Loan_Submit(object sender, RoutedEventArgs e)
        {
            Loan loan = ALL_LOANS.Where(x => x.Item.Title == Item_Title_Input.Text).First();
            SqliteDataAccess.DeleteLoan(loan);
            updateDataGrid();
        }
        private void Update_Loan_Submit(object sender, RoutedEventArgs e)
        {
            Loan loan = ALL_LOANS.Where(x => x.Item.Title == Item_Title_Input.Text).First();
            loan.DateDue = Convert.ToDateTime(ReturnDate.Text);
            SqliteDataAccess.UpdateLoan(loan);
            updateDataGrid();
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
