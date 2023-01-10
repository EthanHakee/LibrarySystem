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
        readonly IEnumerable<Loan> ALL_LOANS = SqliteDataAccess.Load<Loan>();

        public OverDueLoans()
        {
            InitializeComponent();
            GenerateGrid();
        }

        public void GenerateGrid()
        {
            List<DataGridLoan> result = new();

            foreach (var item in ALL_LOANS)
            {
                if (item.DateDue < DateTime.Now)
                {
                    DataGridLoan loan = new(item.Member, item.Item, item.DateOut, item.DateDue);
                    result.Add(loan);
                }
            }
            LoanGrid.ItemsSource = result;
        }

        private void LoanGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGridLoan SelectedLoan = (DataGridLoan)LoanGrid.SelectedItem;
            int DaysOverDue = Convert.ToInt32(Math.Round((DateTime.Now - Convert.ToDateTime(SelectedLoan.DateDue)).TotalDays, 0));
            AmountDue.Text = $"Amount Due: £{Math.Round(DaysOverDue * 0.25, 2)}";
        }
    }
}

