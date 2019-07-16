using Schedule.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.Classes.Interfaces
{
    public interface IRepository
    {
        IEnumerable<User> Users { get; }
        IEnumerable<Station> Stations { get; }
        IEnumerable<Route> Routes { get; }
        IEnumerable<Preference> Preferences { get; }

        string Name { get; }
        string Surname { get; }
        string Email { get; }

        bool UpdateUser(string name, string surname, string email, Action onUserUpdated);
        void DeleteUser(Action onUserDeleted);

        IEnumerable<ScheduleItem> FindTimes(Station station, IEnumerable<Route> routes);
        void Authorization(string email, string password, Action wrongEmail, Action wrongPassword, Action authorized);
        void LogOut(Action onLoggedOut);
        bool RegisterUser(string name, string surname, string email, string password, Action onUserRegistered);
        void UpdatePreferences(IEnumerable<object> newPreferences);
        void UpdatePassword(string oldPassword, string newPassword, Action onWrongPassword, Action onPasswordUdated);
        void SaveRepository();

    }
}
