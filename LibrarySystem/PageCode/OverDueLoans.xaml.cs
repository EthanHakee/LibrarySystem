using LibrarySystem.DataAccess;
using LibrarySystem.Models;
using LibrarySystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace LibrarySystem.Pages
{
    public partial class OverDueLoans : Page
    {
        IEnumerable<Loan> ALL_LOANS = SqliteDataAccess.Load<Loan>();
        public OverDueLoans()
        {
            InitializeComponent();

            List<DataGridLoan> result = new List<DataGridLoan>();

            foreach (var item in ALL_LOANS)
            {
                if (item.DateDue < DateTime.Now)
                {
                    DataGridLoan loan = new DataGridLoan(item.Member, item.Item, item.DateOut, item.DateDue);
                    result.Add(loan);

                }
            }

            LoanGrid.ItemsSource = result;
        }

        private void LoanGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGridLoan loan = (DataGridLoan)LoanGrid.SelectedItem;
            int days = Convert.ToInt32(Math.Round((DateTime.Now - Convert.ToDateTime(loan.DateDue)).TotalDays, 0));
            double Fine = (days * 0.25);
            AmountDue.Text = $"Amount Due: £{Math.Round(Fine,2)}";
        }

    }
}
