using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasusExotischNederland.Model
{
    public class User
    {
        public int Id { get; set; }
        public List<Observation> Observations { get; set; }
        public List<Route> Routes { get; set; }
        public List<Rol> Roles { get; set; }
        public List<Question> Questions { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }

        public User(int id, string name, int age, string email, int phoneNumber)
        {
            Id = id;
            Name = name;
            Age = age;
            Email = email;
            PhoneNumber = phoneNumber;
            Observations = new List<Observation>();
            Routes = new List<Route>();
            Roles = new List<Rol>();
            Questions = new List<Question>();
        }
    }
}
