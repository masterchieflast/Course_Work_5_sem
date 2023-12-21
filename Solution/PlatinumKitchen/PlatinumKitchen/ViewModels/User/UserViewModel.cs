using Microsoft.Win32;
using PlatinumKitchen.Infasturcture;
using PlatinumKitchen.Models;
using PlatinumKitchen.Models.Database.Entityes;
using PlatinumKitchen.Utilities;
using PlatinumKitchen.Utilities.Commands;
using System.IO;
using System.Windows;
using System.Windows.Input;


namespace PlatinumKitchen.ViewModels.User
{
    public class UserViewModel : ViewModelBase
    {
        private DelegateCommand? edit;
        private DelegateCommand? change;
        private DelegateCommand? image;

        public int AccauntId
        {
            get
            {
                if (Controller.Admin)
                {
                    return -1;
                }
                if (Controller.UserE != null)
                {
                    return Controller.UserE.Id;
                }
                else if (Controller.User != null){
                    return Controller.User.Id;
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
                if (Controller.Admin)
                {
                    return "Admin";
                }
                if (Controller.UserE != null)
                {
                    return Controller.UserE.First_Name;
                }
                else if (Controller.User != null){
                    return Controller.User.First_Name;
                }
                else
                {
                    return "Admin";
                }
            }
            set
            {
                if(String.IsNullOrEmpty(value) || value.Length >= 15)
                {
                    return;
                }
                if (Controller.UserE != null)
                {
                    Controller.UserE.First_Name = value;
                    return;
                }
                if (Controller.User != null)
                {
                    Controller.User.First_Name = value;
                    return;
                }
            }
        }

        public string LastName
        {
            get
            {
                if (Controller.Admin)
                {
                    return "Admin";
                }
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
                if (String.IsNullOrEmpty(value) || value.Length >= 15)
                {
                    return;
                }
                if (Controller.User != null)
                {
                    Controller.User.Last_Name = value;
                    return;
                }
                if (Controller.UserE != null)
                {
                    Controller.UserE.Last_Name = value;
                    return;
                }
            }
        }

        public string Login
        {
            get
            {
                if (Controller.Admin)
                {
                    return "Admin";
                }
                if (Controller.UserE != null)
                {
                    return Controller.UserE.Login;
                }
                else if (Controller.User != null){
                    return Controller.User.Login;
                }
                else
                {
                    return "Admin";
                }
            }
            set
            {
                if(String.IsNullOrEmpty(value) || value.Length < 3 || value.Length >= 15)
                {
                    return;
                }

                if (Controller.UserE != null)
                {
                    Controller.UserE.Login = value;
                    return;
                }
                if (Controller.User != null)
                {
                    Controller.User.Login = value;
                    return;
                }
            }
        }

        public string Password
        {
            get
            {
                if (Controller.Admin)
                {
                    return "Admin";
                }
                if (Controller.UserE != null)
                {
                    return Controller.UserE.Password;
                }
                else if (Controller.User != null){
                    return Controller.User.Password;
                }
                else
                {
                    return "Admin";
                }
            }
            set
            {
                if (String.IsNullOrEmpty(value)
                || value.Length < 8 || !value.Any(char.IsDigit) || !value.Any(char.IsUpper))
                {
                    MessageBox.Show("Invalid Password");
                    value = value.Substring(0, 15);
                    return;
                }
                if (Controller.UserE != null)
                {
                    Controller.UserE.Password = UnsecureString.Encode(value);
                    return;
                }
                if (Controller.User != null)
                {
                    Controller.User.Password = UnsecureString.Encode(value);
                    return;
                }
            }
        }
        public string Email
        {
            get
            {
                if (Controller.Admin)
                {
                    return "Admin";
                }
                if (Controller.UserE != null)
                {
                    return Controller.UserE.Email;
                }
                else if (Controller.User != null){
                    return Controller.User.Email;
                }
                else
                {
                    return "Admin";
                }
            }
            set
            {
                if (!Validator.EmailValid(value) || value.Length >= 15)
                {
                    return;
                }
                if (Controller.UserE != null)
                {
                    Controller.UserE.Email = value;
                    return;
                }
                if (Controller.User != null)
                {
                    Controller.User.Email = value;
                    return;
                }
            }
        }

        public string PhoneNumber
        {
            get
            {
                if (Controller.Admin)
                {
                    return "Admin";
                }
                if (Controller.UserE != null)
                {
                    return Controller.UserE.Phone_Number;
                }
                else if (Controller.User != null){
                    return Controller.User.Phone_Number;
                }
                else
                {
                    return "Admin";
                }
            }
            set
            {
                if (String.IsNullOrEmpty(value) || value.Length < 8 || value.Length >= 15)
                {
                    value = value.Substring(0, 15);
                    return;
                }

                foreach (char c in value)
                {
                    if (!Char.IsDigit(c))
                    {
                        return;
                    }
                }
                if (Controller.UserE != null)
                {
                    Controller.UserE.Phone_Number = value;
                }
                if (Controller.User != null)
                {
                    Controller.User.Phone_Number = value;
                    return;
                }
            }
        }

        public string Position
        {
            get
            {
                if (Controller.Admin)
                {
                    return "Admin";
                }

                if (Controller.UserE != null)
                {
                    return Controller.UserE.Position;
                }
                if (Controller.User != null)
                {
                    Controller.UserView.PositionLine.Visibility = System.Windows.Visibility.Hidden;
                    Controller.UserView.Position.Visibility = System.Windows.Visibility.Hidden;
                    return ""; 
                }
                else
                {
                    return "-";
                }
            }
            set
            {
                if (value.Length >= 15)
                {
                    value = value.Substring(0, 15);
                }
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

                if (Controller.Admin)
                {
                    return "Admin";
                }
                if (Controller.UserE != null)
                {
                    return Controller.UserE.Salary.ToString();
                }
                else if (Controller.User != null)
                {
                    Controller.UserView.SalaryLine.Visibility = System.Windows.Visibility.Hidden;
                    Controller.UserView.Salary.Visibility = System.Windows.Visibility.Hidden;
                    return "";
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


        public ICommand Edit
        {
            get
            {
                if (edit == null)
                {
                    edit = new DelegateCommand(EditUser);
                }
                return edit;
            }
        }

        private void EditUser()
        {
            var view = Controller.UserView;
            view.FirstName.IsHitTestVisible = true;
            view.LastName.IsHitTestVisible = true;
            view.Login.IsHitTestVisible = true;
            view.Password.IsHitTestVisible = true;
            view.Email.IsHitTestVisible = true;
            view.PhoneNumber.IsHitTestVisible = true;
            view.GetImage.Visibility = System.Windows.Visibility.Visible;
            view.SaveBut.Visibility = System.Windows.Visibility.Visible;
        }

        public ICommand Image
        {
            get
            {
                if (image == null)
                {
                    image = new DelegateCommand(ImageUser);
                }
                return image;
            }
        }

        private void ImageUser()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|All files (*.*)|*.*";
            var destinationPath = Controller.Uri + $"/Assert/Images/User/PreImage.png";

            if (openFileDialog.ShowDialog() == true)
            {
                string sourcePath = openFileDialog.FileName;

                try
                {
                    // Копируем файл из исходной директории в указанную директорию
                    File.Copy(sourcePath, destinationPath, true);

                    MessageBox.Show("Image Save");
                }
                catch (Exception ex)
                {
                    // Обрабатываем возможные ошибки, например, если файл уже используется другим процессом
                    MessageBox.Show($"Ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        public ICommand Change
        {
            get
            {
                if (change == null)
                {
                    change = new DelegateCommand(ChangeUser);
                }
                return change;
            }
        }

        private void ChangeUser()
        {
            var view = Controller.UserView;
            view.FirstName.IsHitTestVisible = false;
            view.LastName.IsHitTestVisible = false;
            view.Login.IsHitTestVisible = false;
            view.Password.IsHitTestVisible = false;
            view.Email.IsHitTestVisible = false;
            view.PhoneNumber.IsHitTestVisible = false;
            view.GetImage.Visibility = System.Windows.Visibility.Hidden;
            view.SaveBut.Visibility = System.Windows.Visibility.Hidden;


            try
            {
                var sourcePath = Controller.Uri + $"/Assert/Images/User/PreImage.png";
                var destinationPath = Controller.Uri;

                if (Controller.UserE != null)
                {
                    Controller.DataBase.EmployeeRepository.Update(Controller.UserE);
                    destinationPath += $"/Assert/Images/User/Pree{Controller.UserE.Id}.png";
                }
                else if (Controller.User != null)
                {
                    Controller.DataBase.CustomerRepository.Update(Controller.User);
                    destinationPath += $"/Assert/Images/User/Pre{Controller.User.Id}.png";
                }


                Controller.DataBase.Save();

                File.Copy(sourcePath, destinationPath, true);
            }
            catch (Exception ex)
            {
            }

        }
    }
}
