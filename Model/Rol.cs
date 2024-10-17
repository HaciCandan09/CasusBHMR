using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasusExotischNederland.Model
{
    public class Rol
    {
        public int Id { get; set; }
        public List<User> Users { get; set; }
        public string Name { get; set; }

        public Rol(int id, string name)
        {
            Id = id;
            Name = name;
            Users = new List<User>();
        }
    }
}
