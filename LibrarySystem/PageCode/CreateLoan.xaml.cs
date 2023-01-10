using LibrarySystem.DataAccess;
using LibrarySystem.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace LibrarySystem.Pages
{
    public partial class CreateLoan : Page
    {
        readonly MainWindow MAIN_WINDOW = (MainWindow)Application.Current.MainWindow;
        readonly IEnumerable<Member> ALL_MEMBERS = SqliteDataAccess.Load<Member>();
        readonly IEnumerable<Item> ALL_ITEMS = SqliteDataAccess.Load<Item>();
        List<Member> MEMBER_SUGGESTIONS = new();
        List<Item> ITEM_SUGGESTIONS = new();
        int SELECTED_ITEM_INDEX;
        int SELECTED_MEMBER_INDEX;
        int LOAN_DURATION;
        
        public CreateLoan()
        {
            InitializeComponent();
        }

        public List<Item> DBItemSearch(IEnumerable<Item> AllItems)
        {
            string SearchQuery = Item_Search_Box.Text;
            List<Item> Result = new();

            if (SearchQuery == string.Empty) return Result;

            foreach (Item item in AllItems)
            {
                if (item.Title.Contains(SearchQuery, StringComparison.CurrentCultureIgnoreCase) && item.IsAvailable == true)
                    Result.Add(item);
            }

            return Result;
        }

        public List<Member> DBMemberSearch(IEnumerable<Member> AllMembers)
        {
            string SearchQuery = Member_Search.Text;
            List<Member> Result = new();

            if (SearchQuery == string.Empty) return Result;

            foreach (Member member in AllMembers)
            {
                if (member.FirstName.Contains(SearchQuery, StringComparison.CurrentCultureIgnoreCase))
                    Result.Add(member);
            }

            return Result;
        }

        private void LoanTime_SelectionChanged(object sender, RoutedEventArgs e)
        {
            int index = LoanTime.SelectedIndex;

            switch (index)
            {
                case 0:
                    Loan_Due_Date.Text = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
                    LOAN_DURATION = 365;
                    break;
                case 1:
                    Loan_Due_Date.Text = DateTime.Now.AddDays(7).ToString("dd/MM/yyyy");
                    LOAN_DURATION = 31;
                    break;
                case 2:
                    Loan_Due_Date.Text = DateTime.Now.AddMonths(1).ToString("dd/MM/yyyy");
                    LOAN_DURATION = 7;
                    break;
                case 3:
                    Loan_Due_Date.Text = DateTime.Now.AddYears(1).ToString("dd/MM/yyyy");
                    LOAN_DURATION = 1;
                    break;
                default:
                    Loan_Due_Date.Text = DateTime.Now.ToString();
                    LOAN_DURATION = 0;
                    break;
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            Loan loan = new(MEMBER_SUGGESTIONS[SELECTED_MEMBER_INDEX], ITEM_SUGGESTIONS[SELECTED_ITEM_INDEX], DateTime.Now, DateTime.Now.AddDays(LOAN_DURATION));

            Item item = ITEM_SUGGESTIONS[SELECTED_ITEM_INDEX];
            item.IsAvailable = false;
            SqliteDataAccess.Update(item);

            SqliteDataAccess.Save(loan);

            Frame frame = MAIN_WINDOW.Main;
            frame.Content = new CreateLoan();
        }

        private void Loan_Item_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            ITEM_SUGGESTIONS = DBItemSearch(ALL_ITEMS);
            Item_Search_Results.ItemsSource = Item.GetShortDetails(ITEM_SUGGESTIONS);
        }

        private void Loan_Member_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            MEMBER_SUGGESTIONS = DBMemberSearch(ALL_MEMBERS);
            Member_Search_Results.ItemsSource = Member.GetShortDetails(MEMBER_SUGGESTIONS);
        }

        private void ItemSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SELECTED_ITEM_INDEX = Item_Search_Results.SelectedIndex;
            Item_Details.ItemsSource = Item.GetAllDetails(ITEM_SUGGESTIONS[SELECTED_ITEM_INDEX]);
        }

        private void MemberSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SELECTED_MEMBER_INDEX = Member_Search_Results.SelectedIndex;
            Member_Details.ItemsSource = Member.GetAllDetails(MEMBER_SUGGESTIONS[SELECTED_MEMBER_INDEX]);
        }
    }
}
