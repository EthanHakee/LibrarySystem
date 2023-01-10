using System;
using System.Windows;
using System.Windows.Controls;
using LibrarySystem.DataAccess;
using LibrarySystem.Models;

namespace LibrarySystem.Pages
{
    public partial class Create : Page
    {
        readonly MainWindow MAIN_WINDOW = (MainWindow)Application.Current.MainWindow;

        public Create()
        {
            InitializeComponent();
        }

        private void Member_Submit_Click(object sender, RoutedEventArgs e)
        {
            if (!CreateMember()) MessageBox.Show("There Was an Error has Occured While trying to Create the Member");
        }

        private void Item_Submit_Click(object sender, RoutedEventArgs e)
        {
            //Return a different error message for each fault
            int result = CreateItem();

            switch (result)
            {
                case 1:
                    MessageBox.Show("Make Sure all Boxes are Filled");
                    break;
                case 2:
                    MessageBox.Show("The Media Type Could Not be Found");
                    break;
                case 3:
                    MessageBox.Show("The Genre Could Not be Found");
                    break;
                default:
                    MessageBox.Show("");
                    break;
            }
        }

        public int CreateItem()
        {
            //Checking all of the input boxes are filled
            if (Item_Title_Input.Text == "" || Item_Author_Input.Text == "") return 1;

            //Checking the Type and Genre exist
            if (!Enum.TryParse(Item_Type_Input.Text, out Models.Type type)) return 2;

            if (!Enum.TryParse(Item_Genre_Input.Text, out Genre genre)) return 3;

            //Create a new Item object and saving it
            Item item = new(type, genre, Item_Title_Input.Text, Item_Author_Input.Text, 1);
            SqliteDataAccess.Save(item);

            //Clearing all the values on the page
            Frame frame = MAIN_WINDOW.Main;
            frame.Content = new Create();

            return 0;
        }

        public bool CreateMember()
        {
            try
            {
                Member member = new(
                    Member_FirstName_Input.Text,
                    Member_LastName_Input.Text, 
                    Member_Address_Input.Text, 
                    Member_EmailAddress_Input.Text, 
                    Member_PhoneNumber_Input.Text
                    );

                SqliteDataAccess.Save(member);
            }
            catch
            {
                return false;
            }

            //Clearing all the values on the page
            Frame frame = MAIN_WINDOW.Main;
            frame.Content = new Create();

            return true;
        }
    }
}
