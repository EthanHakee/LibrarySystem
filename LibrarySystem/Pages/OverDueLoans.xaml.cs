using LibrarySystem.DataAccess;
using LibrarySystem.Models;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace LibrarySystem.Pages
{
    public partial class OverDueLoans : Page
    {
        IEnumerable<Loan> ALL_LOANS = SqliteDataAccess.LoadLoans();
        public OverDueLoans()
        {
            InitializeComponent();

            List<Loan> result = new List<Loan>();
            
            foreach (var item in ALL_LOANS)
            {
                if(item.DateDue > DateTime.Now)
                {
                    result.Add(item);
                }
            }

            LoanGrid.ItemsSource = result;
        }
    }
}
