using Schedule.Classes.Helpers;
using Schedule.Classes.Interfaces;
using Schedule.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.Classes
{
    public abstract class BaseRepository : IRepository
    {
        protected List<User> _users;
        protected List<Station> _stations;
        protected List<Route> _routes;

        protected User _authorizedUser;

        public IEnumerable<User> Users => _users;
        public IEnumerable<Station> Stations => _stations;
        public IEnumerable<Route> Routes => _routes;
        public IEnumerable<Preference> Preferences => _authorizedUser.Preferences;
        public string Name => _authorizedUser.Name;
        public string Surname => _authorizedUser.Surname;
        public string Email => _authorizedUser.Email;

        public abstract void SaveRepository();

        public virtual void Authorization(string email, string password, Action wrongEmail, Action wrongPassword, Action authorized)
        {
            Action WrongEmail = wrongEmail;
            Action WrongPassword = wrongPassword;
            Action Authorized = authorized;

            var curUser = Users.SingleOrDefault(u => u.Email == email);
            if (curUser == null)
            {
                WrongEmail?.Invoke();
            }
            else if (curUser.Password != PasswordEncrypter.Encrypt(password))
            {
                WrongPassword?.Invoke();
            }
            else
            {
                _authorizedUser = curUser;
                Authorized?.Invoke();
            }
        }

        public virtual bool RegisterUser(string name, string surname, string email, string password, Action onUserRegistered)
        {
            Action UserRegistered = onUserRegistered;
            if (CanAddUser(email))
            {
                User newUser = new User()
                {
                    Name = name,
                    Surname = surname,
                    Email = email,
                    Password = password,
                    Preferences = new List<Preference>()
                };
                _users.Add(newUser);
                _authorizedUser = newUser;
                SaveRepository();
                UserRegistered?.Invoke();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void LogOut(Action onLoggedOut)
        {
            Action loggedOut = onLoggedOut;
            _authorizedUser = null;
            loggedOut?.Invoke();
        }

        public bool UpdateUser(string name, string surname, string email, Action onUserUpdated)
        {
            Action userUpdated = onUserUpdated;
            if (CanAddUser(email) || _authorizedUser.Email == email)
            {
                _authorizedUser.Name = name;
                _authorizedUser.Surname = surname;
                _authorizedUser.Email = email;
                SaveRepository();
                userUpdated?.Invoke();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void UpdatePassword(string oldPassword, string newPassword, Action onWrongPassword, Action onPasswordUpdated)
        {
            Action wrongPassword = onWrongPassword;
            Action passwordUpdated = onPasswordUpdated;
            if (PasswordEncrypter.Encrypt(oldPassword) != _authorizedUser.Password)
            {
                wrongPassword?.Invoke();
            }
            else
            {
                _authorizedUser.Password = PasswordEncrypter.Encrypt(newPassword);
                SaveRepository();
                passwordUpdated?.Invoke();
            }
        }

        public virtual void DeleteUser(Action onUserDeleted)
        {
            Action userDeleted = onUserDeleted;
            _users.Remove(_authorizedUser);
            SaveRepository();
            userDeleted?.Invoke();
        }

        public virtual bool CanAddUser(string email)
        {
            return !(Users.Any(u => u.Email == email));
        }
       
        public void UpdatePreferences(IEnumerable<object> newPreferences)
        {
            List<Preference> preferences = new List<Preference>();
            foreach (var pref in _authorizedUser.Preferences)
            {
                if (newPreferences.Any(p => (p as Station) == pref.Station))
                {
                    preferences.Add(pref);
                }
            }
            _authorizedUser.Preferences = preferences;
            foreach (var pref in newPreferences)
            {
                if (!_authorizedUser.Preferences.Any(p => p.Station == (pref as Station)))
                {
                    _authorizedUser.Preferences.Add(new Preference() { Station = (pref as Station), StationId = (pref as Station).Id });
                }
            }
            SaveRepository();
        }

        public IEnumerable<ScheduleItem> FindTimes(Station station, IEnumerable<Route> routes)
        {
            var curStation = station;
            List<ScheduleItem> result = new List<ScheduleItem>();

            // Call to DateTime.Now only once to prevent different readings on different loop iterations
            // Here manual time can also be set to test the algorithm
            DateTime currentDt = DateTime.Now;

            foreach (var route in routes)
            {

                var routeStation = route.Stations
                    .FirstOrDefault(st => st.Station == station);

                if (routeStation != null)
                {

                    if (routeStation != route.Stations.Last())
                    {
                        int left = route.TimeToNextDepartureFromOrigin(routeStation, currentDt);
                        result.Add(new ScheduleItem
                        {
                            RouteName = route.Name,
                            Destination = route.Stations.Last().Station,
                            MinutesLeft = left
                        });
                    }
                    if (routeStation != route.Stations.First())
                    {
                        int left = route.TimeToNextDepartureFromDest(routeStation, currentDt);
                        result.Add(new ScheduleItem
                        {
                            RouteName = route.Name,
                            Destination = route.Stations.First().Station,
                            MinutesLeft = left
                        });
                    }
                }
            }
            return result;
        }
    }
}
