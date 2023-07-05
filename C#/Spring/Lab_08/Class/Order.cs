using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Lab_8.Class
{
    [Table("Orders")]
    public class Orders
    {
        [Key]
        public int OrderID { get; set; }
        public int UsersUserID { get; set; }
        public string OrderData { get; set; }
        public decimal TotalAmount { get; set; }
        public virtual Users User { get; set; }
    }
    public class Users
    {
        [Key]
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public byte[] Image { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }

        public Users()
        {
            UserID = new Random().Next(1, 10000);
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            Phone = string.Empty;
            Address = string.Empty;
        }

        public Users(int customerID, string firstName, string lastName, string email, string phone, string address, byte[] image)
        {
            UserID = customerID;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Address = address;
            Image = image;
        }

        public override string ToString()
        {
            return $"CustomerID - {UserID}\n" +
                   $"FirstName - {FirstName} \n" +
                   $"LastName - {LastName}\n" +
                   $"Email - {Email}\n" +
                   $"Phone - {Phone}\n" +
                   $"Address - {Address}";
        }
    }
}