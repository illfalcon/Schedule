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
    /// Логика взаимодействия для EditPreferences.xaml
    /// </summary>
    public partial class EditPreferences : Window
    {
        IRepository _repo;

        public EditPreferences(IRepository repo)
        {
            InitializeComponent();
            _repo = repo;
            UpdateStations();
        }

        public void UpdateStations()
        {
            listBoxStations.ItemsSource = null;
            listBoxStations.ItemsSource = _repo.Stations;
            for (int i = 0; i < listBoxStations.Items.Count; i++)
            {
                var station = listBoxStations.Items[i] as Station;
                if (station.IsPreference(_repo.Preferences))
                    listBoxStations.SelectedItems.Add(listBoxStations.Items[i]);
            }
        }

        //private void Button_Add_Click(object sender, RoutedEventArgs e)
        //{
        //    var addWindow = new AddWindow(_curUser, _repo);
        //    if (addWindow.ShowDialog() == true)
        //    {
        //        UpdatePreferences();
        //    }
        //}

        private void Button_OK_Click(object sender, RoutedEventArgs e)
        {
            _repo.UpdatePreferences((listBoxStations.SelectedItems) as IEnumerable<object>);
            var mainWindow = new MainWindow(_repo);
            mainWindow.Show();
            Close();
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow(_repo);
            mainWindow.Show();
            Close();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_OK_Click(sender, e);
                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                Button_Cancel_Click(sender, e);
                e.Handled = true;
            }
        }

        private void Button_Descriptions_Click(object sender, RoutedEventArgs e)
        {
            _repo.UpdatePreferences((listBoxStations.SelectedItems) as IEnumerable<object>);
            var descriptionWindow = new EditDescriptionsWindow(_repo);
            descriptionWindow.Show();
            Close();
        }
    }
}
