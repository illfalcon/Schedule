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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Schedule.UI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IRepository _repo;
        public MainWindow(IRepository repo)
        {
            InitializeComponent();
            _repo = repo;
            UpdatePreferences();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            DisplayRoutes();
        }

        public void UpdatePreferences()
        {
            comboBoxStations.ItemsSource = null;
            comboBoxStations.ItemsSource = _repo.Preferences;
            comboBoxStations.Text = "Select Station";
            dataGridRoutes.ItemsSource = null;
            descriptionTextBlock.Text = "";
        }
        public void DisplayRoutes()
        {
            var selectedStation = comboBoxStations.SelectedItem as Preference;
            if (selectedStation != null)
            {
                descriptionTextBlock.Text = selectedStation.Description;
                dataGridRoutes.ItemsSource = null;
                dataGridRoutes.ItemsSource = _repo.FindTimes(selectedStation.Station, _repo.Routes).OrderBy(r => r.MinutesLeft);
            }
        }

        private void Button_EditFavorites_Click(object sender, RoutedEventArgs e)
        {
            var editPreferences = new EditPreferences(_repo);
            editPreferences.Show();
            Close();
        }

        private void comboBoxStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DisplayRoutes();
        }

        private void Button_Account_Click(object sender, RoutedEventArgs e)
        {
            var accountWindow = new AccountManageWindow(_repo);
            accountWindow.Show();
            Close();
        }

    }
}
