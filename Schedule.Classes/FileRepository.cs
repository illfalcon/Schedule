using Schedule.Classes.Helpers;
using Schedule.Classes.Interfaces;
using Schedule.Classes.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.Classes
{
    internal class FileRepository : BaseRepository
    {
        public static IFileStreamer FileStreamer { get; set; }

        private const string DataFolder = "Data";
        private const string UsersFileName = "users.json";
        private const string StationsFileName = "stations.json";
        private const string RoutesFileName = "routes.json";



        public FileRepository()
        {
            try
            {
                FileStreamer = new JSONFileStreamer();
                _stations = FileStreamer.RestoreList<Station>(Path.Combine(DataFolder, StationsFileName));
                _routes = FileStreamer.RestoreList<Route>(Path.Combine(DataFolder, RoutesFileName));
                _users = FileStreamer.RestoreList<User>(Path.Combine(DataFolder, UsersFileName));



                foreach (var route in Routes)
                {
                    route.Stations = new List<RouteStation>();
                    foreach (var id in route.StationIds)
                    {
                        route.Stations.Add(new RouteStation()
                        {
                            Station = Stations.SingleOrDefault(s => s.Id == id)
                        });
                    }
                    int totalDistance = 0;
                    route.Stations[0].TimeFromOrigin = 0;
                    for (int k = 0; k < route.Intervals.Count; k++)
                    {
                        totalDistance += route.Intervals[k];
                        route.Stations[k + 1].TimeFromOrigin = totalDistance;
                    }
                    // Going back to fill TimeFromDest
                    for (int k = route.Intervals.Count - 1; k >= 0; k--)
                        route.Stations[k].TimeFromDest = totalDistance - route.Stations[k].TimeFromOrigin;
                };
                if (Users != null)
                {
                    foreach (var user in Users)
                    {
                        foreach (var pref in user.Preferences)
                        {
                            pref.Station = (Stations.SingleOrDefault(s => s.Id == pref.StationId));
                        }
                    }
                }
            }
            catch
            {
                // Is something goes wrong, start off with empty collections
                _users = new List<User>();
            }
        }

        public override void SaveRepository()
        {
            FileStreamer.SaveList(Path.Combine(DataFolder, RoutesFileName), _routes);
            FileStreamer.SaveList(Path.Combine(DataFolder, StationsFileName), _stations);
            FileStreamer.SaveList(Path.Combine(DataFolder, UsersFileName), _users);
        }
    }
}
