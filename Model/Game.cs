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

        public Game(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
            Questions = new List<Question>();
        }
    }
}
