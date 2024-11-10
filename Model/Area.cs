using CasusExotischNederland.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasusExotischNederland.Model
{
    public class Area
    {
        public int Id { get; set; }
        public List<Route> Routes { get; set; }
        public List<Observation> Observations { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }    
        public DataAccessLayer Dal { get; set; }


        public Area()
        {
            
        }

        public Area(int id, string name, string description, string location)
        {
            Id = id;
            Name = name;
            Description = description;
            Location = location;
            Routes = new List<Route>();
            Observations = new List<Observation>();
        }

        public List<Area> GetAll()
        {
            Dal = new DataAccessLayer();
            return Dal.GetAreas();
        }

        public Area GetById(int id)
        {
            Dal = new DataAccessLayer();
            return Dal.GetAreaById(id);
        }

        public void Create() { }
        public void Update() { }
        public void Delete() { }
    }
}
