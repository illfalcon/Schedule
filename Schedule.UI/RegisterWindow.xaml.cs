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
    /// Логика взаимодействия для RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private IRepository _repo;
        public RegisterWindow(IRepository repo)
        {
            InitializeComponent();
            _repo = repo;
            fakePasswordBoxPassword.GotFocus += fakeTextBox_GotFocus;
            fakeTextBoxName.GotFocus += fakeTextBox_GotFocus;
            fakeTextBoxSurname.GotFocus += fakeTextBox_GotFocus;
            fakeTextBoxEmail.GotFocus += fakeTextBox_GotFocus;
        }

        //public UserRegistrationDelegate UserRegistered;

        private void Button_Submit_Click(object sender, RoutedEventArgs e)
        {
            emptySurnameMessage.Visibility = System.Windows.Visibility.Collapsed;
            emptyEmailMessage.Visibility = System.Windows.Visibility.Collapsed;
            emptyNameMessage.Visibility = System.Windows.Visibility.Collapsed;
            emptyPasswordMessage.Visibility = System.Windows.Visibility.Collapsed;
            invalidPasswordMessage.Visibility = System.Windows.Visibility.Collapsed;
            bool emptyName = string.IsNullOrEmpty(textBoxName.Text.Trim());
            bool emptySurname = string.IsNullOrEmpty(textBoxSurname.Text.Trim());
            bool emptyEmail = string.IsNullOrEmpty(textBoxEmail.Text.Trim());
            bool emptyPassword = string.IsNullOrEmpty(passwordBoxPassword.Password.Trim());
            if (emptyEmail || emptyEmail || emptyPassword || emptySurname)
            {
                if (emptySurname)
                    emptySurnameMessage.Visibility = System.Windows.Visibility.Visible;
                if (emptyPassword)
                    emptyPasswordMessage.Visibility = System.Windows.Visibility.Visible;
                if (emptyName)
                    emptyNameMessage.Visibility = System.Windows.Visibility.Visible;
                if (emptyEmail)
                    emptyEmailMessage.Visibility = System.Windows.Visibility.Visible;
            }
            else if (passwordBoxPassword.Password.Length < 7)
            {
                invalidPasswordMessage.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                if (!_repo.RegisterUser(textBoxName.Text, textBoxSurname.Text, textBoxEmail.Text, PasswordEncrypter.Encrypt(passwordBoxPassword.Password), OnUserRegistered))
                {
                    existingEmailMessage.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }

        private void OnUserRegistered()
        {
            var mainWindow = new MainWindow(_repo);
            mainWindow.Show();
            Close();
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }

        private void fakeTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            textBox.Visibility = System.Windows.Visibility.Collapsed;
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

        private void passwordBoxPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(passwordBoxPassword.Password))
                fakePasswordBoxPassword.Visibility = System.Windows.Visibility.Visible;
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

        private void fakePasswordBoxPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            fakePasswordBoxPassword.Visibility = System.Windows.Visibility.Collapsed;
            passwordBoxPassword.Focus();
        }
    }
}
