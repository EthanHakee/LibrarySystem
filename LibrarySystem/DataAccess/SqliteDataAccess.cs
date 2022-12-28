using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using LibrarySystem.Models;
using System;
using System.Windows;

namespace LibrarySystem.DataAccess
{
    public class SqliteDataAccess
    {

        public static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        public static IEnumerable<Member> LoadMembers()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                IEnumerable<Member> output = cnn.Query<Member>("SELECT * FROM Member", new DynamicParameters());
                return output;
            }
        }
        public static void SaveMember(Member member)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query($"INSERT INTO Member(FirstName, LastName, Address, EmailAddress, PhoneNumber) VALUES" +
                    $" '{member.FirstName}','{member.LastName}','{member.Address}','{member.EmailAddress}','{member.PhoneNumber}' "
                , new DynamicParameters());
            }
        }
        public static void UpdateMember(Member member)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query($"UPDATE Member SET FirstName = '{member.FirstName}' , LastName = '{member.LastName}', " +
                    $"Address = '{member.Address}', EmailAddress = '{member.EmailAddress}', PhoneNumber = '{member.PhoneNumber}'" +
                    $"WHERE Id = {member.Id} ", new DynamicParameters());
            }
        }
        public static void DeleteItem(Member member)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query($"DELETE FROM Member WHERE Id ='{member.Id}'", new DynamicParameters());
            }
        }

        public static IEnumerable<Item> LoadItems()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                IEnumerable<Item> output = cnn.Query<Item>("SELECT * FROM Item", new DynamicParameters());
                return output;
            }
        }
        public static void SaveItem(Item item)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query($"INSERT INTO Item(Type, Genre,Title, Author) VALUES" +
                     $" '{item.Type}','{item.Genre}','{item.Title}','{item.Author}' "
                , new DynamicParameters());

                cnn.Close();
            }
        }
        public static void UpdateItem(Item item)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query($"UPDATE Item SET Type = '{item.Type}' , Genre = '{item.Genre}', " +
                    $"Title = '{item.Title}', Author = '{item.Author}' WHERE Id = {item.Id} ", new DynamicParameters());
            }
        }
        public static void DeleteItem(Item item)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query($"DELETE FROM Item WHERE Id ='{item.Id}'", new DynamicParameters());
            }
        }

        public static IEnumerable<Loan> LoadLoans()
        {
            
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                IEnumerable<Loan> output = cnn.Query<Loan>("SELECT * FROM Loan");
                return output;
            }
        }
        public static void SaveLoan(Loan loan)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query($"INSERT INTO Loan(Member, Item, DateOut, DateDue) VALUES" +
                     $"( {loan.Member},{loan.Item},'{loan.DateOut}','{loan.DateDue}') ");
            }
        }
        public static void UpdateLoan(Loan loan)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query($"UPDATE Loan SET Member = '{loan.Member.Id}' , Item = '{loan.Item.Id}', " +
                    $"DateOut = '{loan.DateOut}', DateDue = '{loan.DateDue}' WHERE Id = {loan.Id} ", new DynamicParameters());
            }
        }
        public static void DeleteLoan(Loan loan)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query($"DELETE FROM Loan WHERE Id ='{loan.Id}'", new DynamicParameters());
            }
        }
    }
}
