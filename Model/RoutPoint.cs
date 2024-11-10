using CasusExotischNederland.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasusExotischNederland.Model
{
    public class RoutePoint
    {
        public int Id { get; set; }
        public List<Route> Routes { get; set; }
        public Poi poi { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float CoordinateX { get; set; }
        public float CoordinateY { get; set; }
        public DataAccessLayer Dal { get; set; }

        public RoutePoint(int id, string name, string description, float coordinateX, float coordinateY)
        {
            Id = id;
            Name = name;
            Description = description;
            CoordinateX = coordinateX;
            CoordinateY = coordinateY;
            Routes = new List<Route>();
        }
        public RoutePoint()
        {
                
        }

        public void GetAll()
        {
            Dal = new DataAccessLayer();
            Dal.GetAllRoutePoints();
        }

        public Route GetById(int routeId)
        {
            Dal = new DataAccessLayer();
            return Dal.GetRouteById(routeId);
        }

        public Poi GetPoiByRoutePointId(int routePointId)
        {
            Dal = new DataAccessLayer();
            poi = Dal.GetPoiByRoutePointId(routePointId, this);
            return Dal.GetPoiByRoutePointId(routePointId, this);
        }

        public void Create() { }
        public void Update() { }
        public void Delete() { }
    }
}

