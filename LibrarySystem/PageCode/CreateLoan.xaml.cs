using LibrarySystem.DataAccess;
using LibrarySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace LibrarySystem.Pages
{
    public partial class CreateLoan : Page
    {
        IEnumerable<Member> ALL_MEMBERS = SqliteDataAccess.Load<Member>();
        IEnumerable<Item> ALL_ITEMS = SqliteDataAccess.Load<Item>();
        IEnumerable<Loan> ALL_LOANS = SqliteDataAccess.Load<Loan>();
        List<int> SEARCH_ITEM_IDS = new();
        List<int> SEARCH_MEMBER_IDS = new();
        int SELECTED_ITEM_ID;
        int SELECTED_MEMBER_ID;
        int DATE_DUE;
        public CreateLoan()
        {
            InitializeComponent();

        }

        public List<Item> DBItemSearch(IEnumerable<Item> AllItems)
        {
            bool found = false;
            string SearchQuery = Loan_Product_Search.Text;
            List<Item> Result = new List<Item>();

            if (SearchQuery == string.Empty) return Result;

            foreach (Item item in AllItems)
            {
                if (item.Title.Contains(SearchQuery, StringComparison.CurrentCultureIgnoreCase))
                {
                    foreach (var l in ALL_LOANS)
                    {
                        if (l.Item.Id == item.Id)
                        {
                            found= true;
                        }
                    }

                    if(!found)
                    {
                        Result.Add(item);
                    }
                }
            }

            return Result;
        }

        public List<Member> DBMemberSearch(IEnumerable<Member> AllMembers)
        {
            string SearchQuery = Loan_Member_Search.Text;
            List<Member> Result = new List<Member>();

            if (SearchQuery == string.Empty) return Result;

            foreach (Member member in AllMembers)
            {

                if (member.FirstName.Contains(SearchQuery, StringComparison.CurrentCultureIgnoreCase))
                    Result.Add(member);
            }

            return Result;
        }

        private void Loan_Product_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Item> items = DBItemSearch(ALL_ITEMS);
            foreach (Item item in items) SEARCH_ITEM_IDS.Add(item.Id);
            ItemSearch.ItemsSource = Item.GetShortDetails(items);
        }

        private void Loan_Member_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Member> members = DBMemberSearch(ALL_MEMBERS);
            foreach (Member member in members) SEARCH_MEMBER_IDS.Add(member.Id);
            MemberSearch.ItemsSource = Member.GetShortDetails(members);
        }

        private void ItemSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int SelectedItemIndex = ItemSearch.SelectedIndex;
            int ItemId = SEARCH_ITEM_IDS[SelectedItemIndex];
            SELECTED_ITEM_ID = ItemId;

            //finding the object of the title selected
            foreach (Item item in ALL_ITEMS)
            {
                if (item.Id == ItemId)
                {
                    Item_Details.ItemsSource = Item.GetAllDetails(item);
                }
            }
        }

        private void MemberSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int SelectedMemberIndex = MemberSearch.SelectedIndex;
            int MemberId = SEARCH_MEMBER_IDS[SelectedMemberIndex];
            SELECTED_MEMBER_ID = MemberId;

            // Finding the object of name that was selected
            foreach (Member member in ALL_MEMBERS)
            {
                if (member.Id == MemberId)
                {
                    Member_Details.ItemsSource = Member.GetAllDetails(member);
                }
            }
        }

        private void LoanTime_SelectionChanged(object sender, RoutedEventArgs e)
        {
            int index = LoanTime.SelectedIndex;

            switch (index)
            {
                case 0:
                    Loan_Due_Date.Text = DateTime.Now.AddYears(1).ToString("dd/MM/yyyy");
                    DATE_DUE = 365;
                    break;
                case 1:
                    Loan_Due_Date.Text = DateTime.Now.AddMonths(1).ToString("dd/MM/yyyy");
                    DATE_DUE = 31;
                    break;
                case 2:
                    Loan_Due_Date.Text = DateTime.Now.AddDays(7).ToString("dd/MM/yyyy");
                    DATE_DUE = 7;
                    break;
                case 3:
                    Loan_Due_Date.Text = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
                    DATE_DUE = 1;
                    break;
                default:
                    Loan_Due_Date.Text = DateTime.Now.ToString();
                    DATE_DUE = 0;
                    break;
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            //adding the loan duration to the current date
            DateTime DateOut = DateTime.Now;
            DateTime DateDue = DateTime.Now.AddDays(DATE_DUE);
            Member member = ALL_MEMBERS.Where(x => x.Id == SELECTED_MEMBER_ID).First();
            Item item = ALL_ITEMS.Where(x => x.Id == SELECTED_ITEM_ID).First();

            Loan loan = new(member, item, DateOut, DateDue);

            SqliteDataAccess.Save(loan);

            //Reload the page to get rid of all of the date
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new Uri("Pages\\CreateLoan.xaml", UriKind.Relative));
        }
    }
}
