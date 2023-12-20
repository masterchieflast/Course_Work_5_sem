using PlatinumKitchen.Models;
using PlatinumKitchen.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlatinumKitchen.ViewModels.User
{
    public class UserViewModel : ViewModelBase
    {
        public int AccauntId
        {
            get
            {
                if (Controller.User != null)
                {
                    return Controller.User.Id;
                }
                else if (Controller.UserE != null){
                    return Controller.UserE.Id;
                }
                else
                {
                    return 0;
                }
            }
        }

        public string FirstName
        {
            get
            {
                if (Controller.User != null)
                {
                    return Controller.User.First_Name;
                }
                else if (Controller.UserE != null){
                    return Controller.UserE.First_Name;
                }
                else
                {
                    return "Admin";
                }
            }
            set
            {
                if (Controller.User != null)
                {
                    Controller.User.First_Name = value;
                }
                if (Controller.UserE != null)
                {
                    Controller.UserE.First_Name = value;
                }
            }
        }

        public string LastName
        {
            get
            {
                if (Controller.User != null)
                {
                    return Controller.User.Last_Name;
                }
                else if (Controller.UserE != null){
                    return Controller.UserE.Last_Name;
                }
                else
                {
                    return "Admin";
                }
            }
            set
            {
                if (Controller.User != null)
                {
                    Controller.User.Last_Name = value;
                }
                if (Controller.UserE != null)
                {
                    Controller.UserE.Last_Name = value;
                }
            }
        }

        public string Login
        {
            get
            {
                if (Controller.User != null)
                {
                    return Controller.User.Login;
                }
                else if (Controller.UserE != null){
                    return Controller.UserE.Login;
                }
                else
                {
                    return "Admin";
                }
            }
            set
            {
                if (Controller.User != null)
                {
                    Controller.User.Login = value;
                }
                if (Controller.UserE != null)
                {
                    Controller.UserE.Login = value;
                }
            }
        }

        public string Password
        {
            get
            {
                if (Controller.User != null)
                {
                    return Controller.User.Password;
                }
                else if (Controller.UserE != null){
                    return Controller.UserE.Password;
                }
                else
                {
                    return "Admin";
                }
            }
            set
            {
                if (Controller.User != null)
                {
                    Controller.User.Password = value;
                }
                if (Controller.UserE != null)
                {
                    Controller.UserE.Password = value;
                }
            }
        }
        public string Email
        {
            get
            {
                if (Controller.User != null)
                {
                    return Controller.User.Email;
                }
                else if (Controller.UserE != null){
                    return Controller.UserE.Email;
                }
                else
                {
                    return "Admin";
                }
            }
            set
            {
                if (Controller.User != null)
                {
                    Controller.User.Email = value;
                }
                if (Controller.UserE != null)
                {
                    Controller.UserE.Email = value;
                }
            }
        }

        public string PhoneNumber
        {
            get
            {
                if (Controller.User != null)
                {
                    return Controller.User.Phone_Number;
                }
                else if (Controller.UserE != null){
                    return Controller.UserE.Phone_Number;
                }
                else
                {
                    return "Admin";
                }
            }
            set
            {
                if (Controller.User != null)
                {
                    Controller.User.Phone_Number = value;
                }
                if (Controller.UserE != null)
                {
                    Controller.UserE.Phone_Number = value;
                }
            }
        }

        public string Position
        {
            get
            {
                if (Controller.User != null)
                {
                    Controller.UserView.PositionLine.Visibility = System.Windows.Visibility.Hidden;
                    Controller.UserView.Position.Visibility = System.Windows.Visibility.Hidden;
                    return ""; 
                }
                else if (Controller.UserE != null){
                    return Controller.UserE.Position;
                }
                else
                {
                    return "Admin";
                }
            }
            set
            {
                if (Controller.UserE != null)
                {
                    Controller.UserE.Position = value;
                }
            }
        }

        public string Salary
        {
            get
            {
                if (Controller.User != null)
                {
                    Controller.UserView.SalaryLine.Visibility = System.Windows.Visibility.Hidden;
                    Controller.UserView.Salary.Visibility = System.Windows.Visibility.Hidden;
                    return ""; 
                }
                else if (Controller.UserE != null){
                    return Controller.UserE.Salary.ToString();
                }
                else
                {
                    return "Admin";
                }
            }
            set
            {
                if (Controller.UserE != null)
                {
                    try
                    {
                        Controller.UserE.Salary = decimal.Parse(value);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Invalid  Salary");
                    }
                }
            }
        }
    }
}
