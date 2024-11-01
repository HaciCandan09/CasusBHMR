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

        public Species(int id, string name, string fotoUrl, string category)
        {
            Id = id;
            Name = name;
            FotoUrl = fotoUrl;
            Observations = new List<Observation>();
            Category = category;
        }
    }
}
