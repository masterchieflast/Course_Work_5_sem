using PlatinumKitchen.Infasturcture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PlatinumKitchen.Models.Database.Entityes
{
    public class Customers
    {
        public int Id { get; set; }
        private string first_Name;
        public string First_Name { get => first_Name; set {

                if (String.IsNullOrEmpty(value) || value.Length >= 15)
                {
                    MessageBox.Show(Application.Current.FindResource("FirstNameError").ToString());
                    return;
                }
                first_Name = value;
            } }
        private string last_Name;
        public string Last_Name { get => last_Name; set
            {
                if (String.IsNullOrEmpty(value) || value.Length >= 15)
                {
                    MessageBox.Show(Application.Current.FindResource("LastNameError").ToString());
                    return;
                }
                last_Name = value;
            } }
        private string login;
        public string Login { get => login; set {
                if (String.IsNullOrEmpty(value) || value.Length < 3 || value.Length >= 15)
                {
                    MessageBox.Show(Application.Current.FindResource("LoginError").ToString());
                    return;
                }
                login = value;
            } }
        private string password;
        public string Password { 
            get=>password; set {
                if (String.IsNullOrEmpty(value)
                    || value.Length < 8 || !value.Any(char.IsDigit) || !value.Any(char.IsUpper))
                {
                    MessageBox.Show(Application.Current.FindResource("OrdersError").ToString());
                    return;
                }
                password = value;
            } }

        private string email;
        public string Email {
            get => email; 
            set {
                if (!Validator.EmailValid(value) || value.Length >= 25)
                {
                    MessageBox.Show(Application.Current.FindResource("EmailError").ToString());
                    return;
                }
                email = value;
            } 
        }
        private string phone_Number;
        public string Phone_Number
        {
            get => phone_Number; set
            {
                if (String.IsNullOrEmpty(value) || value.Length < 8 || value.Length >= 15)
                {
                    MessageBox.Show(Application.Current.FindResource("PhoneNumberError").ToString());
                    return;
                }
                foreach (char c in value)
                {
                    if (!Char.IsDigit(c))
                    {
                        MessageBox.Show(Application.Current.FindResource("PhoneNumberError").ToString());
                        return;
                    }
                }

                phone_Number = value;
            }
        }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
