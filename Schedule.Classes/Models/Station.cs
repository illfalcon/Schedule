using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.Classes.Models
{
    public class Station
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsPreference(IEnumerable<Preference> preferences)
        {
            return preferences.Any(p => p.Station.Id == Id);
        }
    }
}
