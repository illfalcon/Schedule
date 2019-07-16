using Schedule.Classes;
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
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private IRepository _repo = Factory.Instance.GetRepository();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void OnUserAuthorized()
        {
            var mainWindow = new MainWindow(_repo);
            mainWindow.Show();
            Close();
        }

        private void OnWrongEmail()
        {
            wrongEmailMessage.Visibility = System.Windows.Visibility.Visible;
        }

        private void OnWrongPassword()
        {
            wrongPasswordMessage.Visibility = System.Windows.Visibility.Visible;
        }

        private void UpdateStorage()
        {
            _repo.SaveRepository();
        }

        private void Button_SignIn_Click(object sender, RoutedEventArgs e)
        {
            wrongEmailMessage.Visibility = System.Windows.Visibility.Collapsed;
            wrongPasswordMessage.Visibility = System.Windows.Visibility.Collapsed;
            emptyEmailMessage.Visibility = System.Windows.Visibility.Collapsed;
            emptyPasswordMessage.Visibility = System.Windows.Visibility.Collapsed;
            if (string.IsNullOrEmpty(textBoxEmail.Text))
            {
                emptyEmailMessage.Visibility = System.Windows.Visibility.Visible;
            }
            else if (string.IsNullOrEmpty(passwordBoxPassword.Password))
            {
                emptyPasswordMessage.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                _repo.Authorization(textBoxEmail.Text, passwordBoxPassword.Password, OnWrongEmail, OnWrongPassword, OnUserAuthorized);
            }
        }

        private void Button_Register_Click(object sender, RoutedEventArgs e)
        {
            var registerWindow = new RegisterWindow(_repo);
            registerWindow.Show();
            Close();
        }

        private void textBoxPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            textBoxPassword.Visibility = System.Windows.Visibility.Collapsed;
            passwordBoxPassword.Focus();
        }

        private void passwordBoxPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(passwordBoxPassword.Password))
                textBoxPassword.Visibility = System.Windows.Visibility.Visible;
        }

        private void fakeTextBoxEmail_GotFocus(object sender, RoutedEventArgs e)
        {
            fakeTextBoxEmail.Visibility = System.Windows.Visibility.Collapsed;
            textBoxEmail.Focus();
        }

        private void textBoxEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxEmail.Text))
                fakeTextBoxEmail.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
