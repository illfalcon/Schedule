using Schedule.Classes.Helpers;
using Schedule.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.Classes
{
    public class DataBaseRepository : BaseRepository, IDisposable
    {
        private Context _context = new Context();
        public DataBaseRepository()
        {
            //_routes = _context.Routes.Include("Stations").ToList();
            //foreach(var station in _context.Stations)
            //{
            //    _stations.Add(station);
            //}
            //_routes = _context.Routes.Include("Stations").ToList();
            //foreach (var route in _context.Routes)
            //{
            //    _routes.Add(route);
            //}
            //_users = _context.Users.Include("Preferences").ToList();
            //foreach (var user in _context.Users)
            //{
            //    _users.Add(user);
            //}
        }
        public override void SaveRepository()
        {
            _context.SaveChanges();
        }

        public override void Authorization(string email, string password, Action wrongEmail, Action wrongPassword, Action authorized)
        {
            Action WrongEmail = wrongEmail;
            Action WrongPassword = wrongPassword;
            Action Authorized = authorized;

            var curUser = _context.Users.Include("Preferences").SingleOrDefault(u => u.Email == email);
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
                _stations = _context.Stations.ToList();
                _authorizedUser = curUser;
                _routes = _context.Routes.Include("Stations").ToList();
                Authorized?.Invoke();
            }
        }

        public override bool RegisterUser(string name, string surname, string email, string password, Action onUserRegistered)
        {
            Action UserRegistered = onUserRegistered;
            if (CanAddUser(email))
            {
                _stations = _context.Stations.ToList();
                _routes = _context.Routes.Include("Stations").ToList();
                User newUser = new User()
                {
                    Name = name,
                    Surname = surname,
                    Email = email,
                    Password = password,
                    Preferences = new List<Preference>()
                };
                _context.Users.Add(newUser);
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

        public override bool CanAddUser(string email)
        {
            return !(_context.Users.Any(u => u.Email == email));
        }

        public override void DeleteUser(Action onUserDeleted)
        {
            Action userDeleted = onUserDeleted;
            _context.Users.Remove(_authorizedUser);
            SaveRepository();
            userDeleted?.Invoke();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
