using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Data;
using LibrarySystem.Models;

namespace LibrarySystem.DataAccess
{
    public class SqliteDataAccess
    {
        public static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        //Depending on the type of object provided it will execute the corresponding SQL query
        public static IEnumerable<T> Load<T>()
        {
            using IDbConnection cnn = new SQLiteConnection(LoadConnectionString());
            return cnn.Query<T>($"SELECT * FROM {typeof(T).Name}", new DynamicParameters());
        }

        public static void Save(object obj)
        {
            string sql = "";

            switch (obj.GetType().Name)
            {
                case "Member":
                    Member member = (Member)obj;
                    sql = $"INSERT INTO Member(FirstName, LastName, Address, EmailAddress, PhoneNumber) VALUES" +
                          $" ('{member.FirstName}','{member.LastName}','{member.Address}','{member.EmailAddress}','{member.PhoneNumber}') ";
                    break;
                case "Item":
                    Item item = (Item)obj;
                    sql = $"INSERT INTO Item(Type, Genre,Title, Author) VALUES" +
                          $" ('{item.Type}','{item.Genre}','{item.Title}','{item.Author}') ";
                    break;
                case "Loan":
                    Loan loan = (Loan)obj;
                    sql = $"INSERT INTO Loan(Member, Item, DateOut, DateDue) VALUES" +
                          $"({loan.Member.Id},{loan.Item.Id},'{loan.DateOut}','{loan.DateDue}') ";
                    break;
            }

            using IDbConnection cnn = new SQLiteConnection(LoadConnectionString());
                var output = cnn.Query(sql, new DynamicParameters());
        }

        public static void Update(object obj)
        {
            string sql = "";

            switch (obj.GetType().Name)
            {
                case "Member":
                    Member member = (Member)obj;
                    sql = $"UPDATE Member SET FirstName = '{member.FirstName}', LastName = '{member.LastName}', Address = '{member.Address}'," +
                        $" EmailAddress = '{member.EmailAddress}', PhoneNumber = '{member.PhoneNumber}' WHERE ID = {member.Id}";
                    break;
                case "Item":
                    Item item = (Item)obj;
                    int res = 1;
                    if (item.IsAvailable == false) res = 0; 
                    sql = $"UPDATE Item SET Type = '{item.Type}', Genre = '{item.Genre}', Title = '{item.Title}', Author = '{item.Author}', IsAvailable = '{res}' " +
                        $" WHERE ID = {item.Id}";
                    break;
                case "Loan":
                    Loan loan = (Loan)obj;
                    sql = $"UPDATE Loan SET Member = {loan.Member.Id}, Item = {loan.Item.Id}, DateOut = '{loan.DateOut}', DateDue = '{loan.DateDue}'" +
                        $" WHERE ID = {loan.Id}";
                    break;
            }

            using IDbConnection cnn = new SQLiteConnection(LoadConnectionString());
                cnn.Execute(sql);
        }

        public static void Delete(object obj)
        {
            string sql = "";

            switch (obj.GetType().Name)
            {
                case "Member":
                    Member member = (Member)obj;
                    sql = $"DELETE FROM Member WHERE ID = {member.Id}";
                    break;
                case "Item":
                    Item item = (Item)obj;
                    sql = $"DELETE FROM Item WHERE ID = {item.Id}";
                    break;
                case "Loan":
                    Loan loan = (Loan)obj;
                    sql = $"DELETE FROM Loan WHERE ID = {loan.Id}";
                    break;
            }

            using IDbConnection cnn = new SQLiteConnection(LoadConnectionString());
                cnn.Execute(sql);
        }
    }
}
