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
    /// Логика взаимодействия для EditDescriptionsWindow.xaml
    /// </summary>
    public partial class EditDescriptionsWindow : Window
    {
        IRepository _repo;
        public EditDescriptionsWindow(IRepository repo)
        {
            InitializeComponent();
            _repo = repo;
            preferencesList.ItemsSource = null;
            preferencesList.ItemsSource = _repo.Preferences;
        }

        private void preferencesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            descriptionTextBox.Text = (preferencesList.SelectedItem as Preference).Description;
        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            _repo.Preferences.FirstOrDefault(p => (preferencesList.SelectedItem as Preference) == p).Description = descriptionTextBox.Text;
            _repo.SaveRepository();
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            var editPreferences = new EditPreferences(_repo);
            editPreferences.Show();
            Close();
        }
    }
}
