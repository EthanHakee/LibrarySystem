using LibrarySystem.Models;
using System;

namespace LibrarySystem.ViewModels
{
    public class DataGridLoan
    {
        public string Member { get; set; }
        public string Item { get; set; }
        public string DateOut { get; set; }
        public string DateDue { get; set; }

        public DataGridLoan(Member member, Item item, DateTime dateOut, DateTime dateDue)
        {
            Member = member.FirstName;
            Item = item.Title;
            DateOut = dateOut.ToShortDateString();
            DateDue = dateDue.ToShortDateString();
        }
    }
}
