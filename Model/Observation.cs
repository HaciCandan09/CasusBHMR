using CasusExotischNederland.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasusExotischNederland.Model
{
    public class Observation
    {
        public int Id { get; set; }
        public Area Area { get; set; }
        public Species Species { get; set; }
        public User User { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public float CoordinateX { get; set; }
        public float CoordinateY { get; set; }
        public string FotoUrl { get; set; }
        public DataAccessLayer Dal { get; set; }

        public Observation(int id, Area area, Species species, User user, DateTime date, string name, float coordinateX, float coordinateY, string fotoUrl, string location)
        {
            Id = id;
            Area = area;
            Species = species;
            User = user;
            Date = date;
            Name = name;
            CoordinateX = coordinateX;
            CoordinateY = coordinateY;
            FotoUrl = fotoUrl;
            Location = location;
        }

        public void Add()
        {
            Dal = new DataAccessLayer();
            Dal.CreateObservation(this);    
        }


    }
}
