namespace Schedule.Classes.Migrations
{
    using Newtonsoft.Json;
    using Schedule.Classes.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    internal sealed class Configuration : DbMigrationsConfiguration<Schedule.Classes.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Schedule.Classes.Context context)
        {
            List<User> users = new List<User>();
            List<Station> stations = new List<Station>();
            List<Route> routes = new List<Route>();

            //reading from files -- start

            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = "Schedule.Classes.Data.stations.json";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    using (var jsonReader = new JsonTextReader(sr))
                    {
                        var serializer = new JsonSerializer();
                        stations = serializer.Deserialize<List<Station>>(jsonReader);
                    }
                }
            }

            resourceName = "Schedule.Classes.Data.users.json";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    using (var jsonReader = new JsonTextReader(sr))
                    {
                        var serializer = new JsonSerializer();
                        users = serializer.Deserialize<List<User>>(jsonReader);
                    }
                }
            }

            resourceName = "Schedule.Classes.Data.routes.json";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    using (var jsonReader = new JsonTextReader(sr))
                    {
                        var serializer = new JsonSerializer();
                        routes = serializer.Deserialize<List<Route>>(jsonReader);
                    }
                }
            }

            foreach (var route in routes)
            {
                route.Stations = new List<RouteStation>();
                foreach (var id in route.StationIds)
                {
                    route.Stations.Add(new RouteStation()
                    {
                        Station = stations.SingleOrDefault(s => s.Id == id)
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
            if (users != null)
            {
                foreach (var user in users)
                {
                    foreach (var pref in user.Preferences)
                    {
                        pref.Station = (stations.SingleOrDefault(s => s.Id == pref.StationId));
                    }
                }
            }

            //reading from files -- end

            
            foreach (var station in stations)
            {
                context.Stations.AddOrUpdate(s => s.Name,
                    new Station()
                    {
                        Name = station.Name
                    });
                context.SaveChanges();
            }
            context.SaveChanges();

            foreach (var user in users)
            {
                context.Users.AddOrUpdate(u => u.Email,
                    new User()
                    {
                        Email = user.Email,
                        Name = user.Name,
                        Surname = user.Surname,
                        Password = user.Password,
                    });
                context.SaveChanges();

                User contextUser = context.Users.First(u => u.Email == user.Email);
                contextUser.Preferences = new List<Preference>();
                foreach (var pref in user.Preferences)
                {
                    if (pref != null)
                    {
                        contextUser.Preferences.Add(new Preference()
                        {
                            Description = pref.Description,
                            Station = context.Stations.FirstOrDefault(s => s.Name == pref.Station.Name)
                        });
                    }
                    context.SaveChanges();
                }
                context.SaveChanges();
            }
            context.SaveChanges();

            foreach (var route in routes)
            {
                context.Routes.AddOrUpdate(r => r.Name,
                    new Route()
                    {
                        Name = route.Name,
                        StartTime = route.StartTime,
                        EndTime = route.EndTime,
                        Interval = route.Interval
                    });
                context.SaveChanges();
                Route contextRoute = context.Routes.First(r => r.Name == route.Name);
                contextRoute.Stations = new List<RouteStation>();
                context.SaveChanges();

                foreach (var station in route.Stations)
                {
                    contextRoute.Stations.Add(new RouteStation()
                    {
                        Station = context.Stations.FirstOrDefault(s => s.Name == station.Station.Name),
                        TimeFromDest = station.TimeFromDest,
                        TimeFromOrigin = station.TimeFromOrigin
                    });
                    context.SaveChanges();
                }
            }
            context.SaveChanges();
        }
    }
}
