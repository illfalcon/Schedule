using Schedule.Classes.Interfaces;
using Schedule.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Schedule.UI
{
    /// <summary>
    /// Логика взаимодействия для AccountManageWindow.xaml
    /// </summary>
    public partial class AccountManageWindow : Window
    {
        IRepository _repo;
        public AccountManageWindow(IRepository repo)
        {
            InitializeComponent();
            _repo = repo;
            textBoxName.Text = _repo.Name;
            textBoxSurname.Text = _repo.Surname;
            textBoxEmail.Text = _repo.Email;
        }

        private void textBoxSurname_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxSurname.Text.Trim()))
                fakeTextBoxSurname.Visibility = System.Windows.Visibility.Visible;
        }

        private void textBoxName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text.Trim()))
                fakeTextBoxName.Visibility = System.Windows.Visibility.Visible;
        }

        private void textBoxEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxEmail.Text.Trim()))
                fakeTextBoxEmail.Visibility = System.Windows.Visibility.Visible;
        }

        private void fakeTextBoxName_GotFocus(object sender, RoutedEventArgs e)
        {
            fakeTextBoxName.Visibility = System.Windows.Visibility.Collapsed;
            textBoxName.Focus();
        }

        private void fakeTextBoxSurname_GotFocus(object sender, RoutedEventArgs e)
        {
            fakeTextBoxSurname.Visibility = System.Windows.Visibility.Collapsed;
            textBoxSurname.Focus();
        }

        private void fakeTextBoxEmail_GotFocus(object sender, RoutedEventArgs e)
        {
            fakeTextBoxEmail.Visibility = System.Windows.Visibility.Collapsed;
            textBoxEmail.Focus();
        }

        private void Button_Submit_Click(object sender, RoutedEventArgs e)
        {
            emptySurnameMessage.Visibility = System.Windows.Visibility.Collapsed;
            emptyEmailMessage.Visibility = System.Windows.Visibility.Collapsed;
            emptyNameMessage.Visibility = System.Windows.Visibility.Collapsed;
            existingEmailMessage.Visibility = System.Windows.Visibility.Collapsed;
            bool emptyName = string.IsNullOrEmpty(textBoxName.Text.Trim());
            bool emptySurname = string.IsNullOrEmpty(textBoxSurname.Text.Trim());
            bool emptyEmail = string.IsNullOrEmpty(textBoxEmail.Text.Trim());
            if (emptyName || emptyEmail || emptySurname)
            {
                if (emptySurname)
                    emptySurnameMessage.Visibility = System.Windows.Visibility.Visible;
                if (emptyName)
                    emptyNameMessage.Visibility = System.Windows.Visibility.Visible;
                if (emptyEmail)
                    emptyEmailMessage.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                if (!_repo.UpdateUser(textBoxName.Text, textBoxSurname.Text, textBoxEmail.Text, OnUserUpdated))
                {
                    existingEmailMessage.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }

        private void OnUserUpdated()
        {
            MainWindow mainWindow = new MainWindow(_repo);
            mainWindow.Show();
            Close();
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(_repo);
            mainWindow.Show();
            Close();
        }

        private void Button_LogOut_Click(object sender, RoutedEventArgs e)
        {
            _repo.LogOut(OnLoggedOut);
        }

        private void OnLoggedOut()
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }

        private void Button_DeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            ConfirmationWindow confirm = new ConfirmationWindow();
            confirm.ShowDialog();
            if (confirm.DialogResult == true)
            {
                _repo.DeleteUser(OnUserDeleted);
            }
        }

        private void OnUserDeleted()
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }

        private void Button_PasswordChange_Click(object sender, RoutedEventArgs e)
        {
            var passwordChangeWindow = new PasswordChangeWindow(_repo);
            passwordChangeWindow.Show();
            Close();
        }
    }
}
