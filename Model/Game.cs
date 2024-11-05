using CasusExotischNederland.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasusExotischNederland.Model
{
    public class Game
    {
        public int Id { get; set; }
        public List<Question> Questions { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DataAccessLayer Dal { get; set; }

        public Game()
        {
            
        }
        public Game(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
            Questions = new List<Question>();
        }

        public List<Game> GetGamesByRoute(int routeId)
        {
            Dal = new DataAccessLayer();
            return Dal.GetGamesByRouteId(routeId);
        }
        
    }
}
