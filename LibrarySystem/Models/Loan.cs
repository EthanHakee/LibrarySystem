using System;

namespace LibrarySystem.Models
{
    public class Loan
    {
        public int Id { get; set; }
        public int Member { get; set; }
        public int Item { get; set; }
        public DateTime DateOut { get; set; }
        public DateTime DateDue { get; set; }

        public Loan(long id, long member, long item, string dateOut, string dateDue)
        {
            Id= Convert.ToInt32(id);
            Member = Convert.ToInt32(member);
            Item = Convert.ToInt32(item);
            DateOut = Convert.ToDateTime(dateOut);
            DateDue = Convert.ToDateTime(dateDue);
        }

        public Loan(int member, int item, DateTime dateOut, DateTime dateDue)
        {
            Member = member;
            Item = item;
            DateOut = dateOut;
            DateDue = dateDue;
        }
    }
}
