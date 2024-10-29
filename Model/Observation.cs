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
        public float CoordinateX { get; set; }
        public float CoordinateY { get; set; }
        public string FotoUrl { get; set; }
        public DataAccessLayer Dal { get; set; }

        public Observation(int id, Area area, Species species, User user, DateTime date, string name, float coordinateX, float coordinateY, string fotoUrl)
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
        }

        public void AddObservation()
        {
            Dal = new DataAccessLayer();
            int speciesId = Dal.CreateSpecies(this.Species);
            this.Species.Id = speciesId;
            Dal.CreateObservation(this);    
        }


    }
}
