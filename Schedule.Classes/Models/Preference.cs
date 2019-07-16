using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.Classes.Models
{
    public class Preference
    {
        public int Id { get; set; }
        public int? StationId { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public Station Station { get; set; }
    }
}
