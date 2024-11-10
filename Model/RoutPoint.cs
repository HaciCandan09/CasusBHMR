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
            Dal.GetRoutePoints();
        }

        public Route GetById(int routeId)
        {
            Dal = new DataAccessLayer();
            return Dal.GetRouteById(routeId);
        }

        public Poi GetPoiByRoutePointId(int routePointId)
        {
            Dal = new DataAccessLayer();
            List<Poi> pois = Dal.GetPOIsByRoutePointId(routePointId);

            // Return the first Poi (assuming one Poi per RoutePointId)
            return pois.FirstOrDefault(); // If there's no Poi, this will return null
        }

        public void LoadPoi()
        {
            Dal = new DataAccessLayer();
            this.poi = Dal.GetPOIsByRoutePointId(this.Id).FirstOrDefault();  // Fetch POI for the current RoutePoint

            // Log POI data to confirm it’s being retrieved
            if (this.poi != null)
            {
                Console.WriteLine($"POI Loaded: {this.poi.Name}, {this.poi.Description}, {this.poi.Type}");
            }
            else
            {
                Console.WriteLine($"No POI found for RoutePoint ID: {this.Id}");
            }
        }

        public void Create() { }
        public void Update() { }
        public void Delete() { }
    }
}

