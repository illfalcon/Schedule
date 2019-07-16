using Schedule.Classes.Helpers;
using Schedule.Classes.Interfaces;
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
    /// Логика взаимодействия для PasswordChangeWindow.xaml
    /// </summary>
    public partial class PasswordChangeWindow : Window
    {
        private IRepository _repo;
        public PasswordChangeWindow(IRepository repo)
        {
            InitializeComponent();
            _repo = repo;
        }

        private void Button_Submit_Click(object sender, RoutedEventArgs e)
        {
            wrongNewPasswordMessage.Visibility = System.Windows.Visibility.Collapsed;
            emptyNewPasswordMessage.Visibility = System.Windows.Visibility.Collapsed;
            wrongOldPasswordMessage.Visibility = System.Windows.Visibility.Collapsed;
            emptyOldPasswordMessage.Visibility = System.Windows.Visibility.Collapsed;
            if (string.IsNullOrEmpty(passwordBoxOldPassword.Password) || string.IsNullOrEmpty(passwordBoxNewPassword.Password))
            {
                if (string.IsNullOrEmpty(passwordBoxOldPassword.Password))
                    emptyOldPasswordMessage.Visibility = System.Windows.Visibility.Visible;
                if (string.IsNullOrEmpty(passwordBoxNewPassword.Password))
                    emptyNewPasswordMessage.Visibility = System.Windows.Visibility.Visible;
            }
            else if (passwordBoxNewPassword.Password.Length < 7)
            {
                wrongNewPasswordMessage.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                _repo.UpdatePassword(passwordBoxOldPassword.Password, passwordBoxNewPassword.Password, OnWrongPassword, OnPasswordUpdated);
            }
        }

        private void OnWrongPassword()
        {
            wrongOldPasswordMessage.Visibility = System.Windows.Visibility.Visible;
        }

        private void OnPasswordUpdated()
        {
            var accountWindow = new AccountManageWindow(_repo);
            accountWindow.Show();
            Close();
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void textBoxOldPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            textBoxOldPassword.Visibility = System.Windows.Visibility.Collapsed;
            passwordBoxOldPassword.Focus();
        }

        private void textBoxNewPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            textBoxNewPassword.Visibility = System.Windows.Visibility.Collapsed;
            passwordBoxNewPassword.Focus();
        }

        private void passwordBoxOldPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(passwordBoxOldPassword.Password))
                textBoxOldPassword.Visibility = System.Windows.Visibility.Visible;
        }

        private void passwordBoxNewPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(passwordBoxNewPassword.Password))
                textBoxNewPassword.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
