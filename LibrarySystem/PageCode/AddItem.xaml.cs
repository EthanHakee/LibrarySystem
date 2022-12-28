using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LibrarySystem.DataAccess;
using LibrarySystem.Models;

namespace LibrarySystem.Pages
{
    public partial class AddItem : Page
    {
        public AddItem()
        {
            InitializeComponent();
        }

        private void Member_Submit_Click(object sender, RoutedEventArgs e)
        {
            CreateMember();
        }

        private void Item_Submit_Click(object sender, RoutedEventArgs e)
        {
            if (!CreateItem()) MessageBox.Show("There Was an Error has Occured While trying to Create the Item");
        }

        public bool CreateItem()
        {
            //Checking all of the input boxes are filled
            if (Item_Title_Input.Text == null || Item_Author_Input.Text == null) return false;

            //Checking the Type and Genre exist
            if (!Enum.TryParse(Item_Type_Input.Text, out Models.Type type) || !Enum.TryParse(Item_Genre_Input.Text, out Genre genre))
            {
                MessageBox.Show("The Media Type or Genre Could Not be Found");
                return false;
            }

            //Create a new Item object and saving it
            Item item = new(type, genre, Item_Title_Input.Text, Item_Author_Input.Text, 1);
            SqliteDataAccess.Save(item);

            //Clearing all the values in the text boxes
            foreach (UIElement element in ItemPanel.Children)
            {
                if (element is TextBox textbox)
                {
                    textbox.Text = String.Empty;
                }
            }

            return true;
        }

        public bool CreateMember()
        {
            try
            {
                Member member = new Member(Member_FirstName_Input.Text, Member_LastName_Input.Text, Member_Address_Input.Text, Member_EmailAddress_Input.Text, Member_PhoneNumber_Input.Text);
                SqliteDataAccess.Save(member);
            }
            catch
            {
                MessageBox.Show("Ensure all the Input Boxes Have Been Filled");
                return false;
            }

            //Clearing all the values in the text boxes
            foreach (UIElement element in ItemPanel.Children)
            {
                if (element is TextBox textbox)
                {
                    textbox.Text = String.Empty;
                }
            }

            return true;
        }
    }
}
