using LibrarySystem.DataAccess;
using System;
using System.Linq;

namespace LibrarySystem.Models
{
    public class Loan
    {
        public int Id { get; set; }
        public Member Member { get; set; }
        public Item Item { get; set; }
        public DateTime DateOut { get; set; }
        public DateTime DateDue { get; set; }
        

        public Loan(long id, long member, long item, string dateOut, string dateDue)
        {
            Id= Convert.ToInt32(id);
            Member = SqliteDataAccess.Load<Member>().Where(x => x.Id == member).First();
            Item = SqliteDataAccess.Load<Item>().Where(x => x.Id == item).First();
            DateOut = Convert.ToDateTime(dateOut);
            DateDue = Convert.ToDateTime(dateDue);
        }

        public Loan(Member member, Item item, DateTime dateOut, DateTime dateDue)
        {
            Member = member;
            Item = item;
            DateOut = dateOut;
            DateDue = dateDue;
        }
    }
}
