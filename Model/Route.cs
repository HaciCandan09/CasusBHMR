using CasusExotischNederland.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasusExotischNederland.Model
{
    public class Route
    {
        public int Id { get; set; }
        public List<RoutePoint> RoutePoints { get; set; }
        public List<Game> Games { get; set; }
        public List<User> Users { get; set; }
        public Area Area { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DataAccessLayer Dal { get; set; }

        public Route()
        {
            
        }
        public Route(int id, Area area, string name, string description)
        {
            Id = id;
            Area = area;
            Name = name;
            Description = description;
            RoutePoints = new List<RoutePoint>();
            Games = new List<Game>();
            Users = new List<User>();
        }


        public Route GetById(int routeId)
        {
            Dal = new DataAccessLayer();
            return Dal.GetRouteById(routeId);
        }
        public List<Route> GetRoutesByArea(int areaId)
        {
            Dal = new DataAccessLayer();
            return Dal.GetRoutesByAreaID(areaId);
        }
    }
}
