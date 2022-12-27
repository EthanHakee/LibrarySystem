using System;
using System.Collections.Generic;

namespace LibrarySystem.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }

        public Member(int id, string firstName, string lastName, string address, string emailAddress, string phoneNumber)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            EmailAddress = emailAddress;
            PhoneNumber = phoneNumber;
        }
        public Member(long id, string firstName, string lastName, string address, string emailAddress, string phoneNumber)
        {
            Id = Convert.ToInt32(id);
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            EmailAddress = emailAddress;
            PhoneNumber = phoneNumber;
        }
        public Member(string firstName, string lastName, string address, string emailAddress, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            EmailAddress = emailAddress;
            PhoneNumber = phoneNumber;
        }

        public static List<string> GetShortDetails(List<Member> members)
        {
            List<string> result = new List<string>();
            foreach (Member member in members)
            {
                result.Add($"{member.FirstName} {member.LastName} : {member.EmailAddress}");
            }

            return result;
        }

        public static List<string> GetAllDetails(Member member)
        {
            List<string> result = new List<string>()
            {
               member.FirstName,
               member.LastName,
               member.Address,
               member.EmailAddress,
               member.PhoneNumber
            };

            return result;
        }
    }
}
