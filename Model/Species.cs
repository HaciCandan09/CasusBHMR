using CasusExotischNederland.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasusExotischNederland.Model
{
    public class Species
    {
        public int Id { get; set; }
        public List<Observation> Observations { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }    
        public string FotoUrl { get; set; }
        public DataAccessLayer Dal { get; set; }

        public Species(int id, string name, string fotoUrl, string category)
        {
            Id = id;
            Name = name;
            FotoUrl = fotoUrl;
            Observations = new List<Observation>();
            Category = category;
        }
        public Species()
        {
            
        }

        public List<Species> GetAll()
        {
            Dal = new DataAccessLayer();
            return Dal.GetSpecies();
        }

        public Species GetById(int id)
        {
            Dal = new DataAccessLayer();
            return Dal.GetSpeciesById(id);
        }

        public void Create() { }
        public void Update() { }
        public void Delete() { }
    }
}
