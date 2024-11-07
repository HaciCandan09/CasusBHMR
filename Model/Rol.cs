using CasusExotischNederland.DAL;
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
        public DataAccessLayer Dal { get; set; }


        public Rol(int id, string name)
        {
            Id = id;
            Name = name;
            Users = new List<User>();
        }

        public void GetById(int id)
        {
            Dal = new DataAccessLayer();
            Dal.GetRoleById(id);
        }

        public void GetAll() { }
        public void Create() { }
        public void Update() { }
        public void Delete() { }
    }
}
