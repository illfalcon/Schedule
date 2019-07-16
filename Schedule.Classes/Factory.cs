using Schedule.Classes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.Classes
{
    public class Factory
    {
        static Factory _instance;

        public static Factory Instance => _instance ?? (_instance = new Factory());

        private Factory() { }

        IRepository _repo;

        public IRepository GetRepository()
        {
            return _repo ?? (_repo = new DataBaseRepository());
        }
    }
}
