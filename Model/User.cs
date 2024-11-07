using CasusExotischNederland.DAL;
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
        public DataAccessLayer Dal { get; set; }

        public User()
        {
                
        }

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


        public List<User> GetAll()
        {
            Dal = new DataAccessLayer();
            return Dal.GetAllUser();
            
        }

        public void Create()
        {
            Dal = new DataAccessLayer();
            Dal.CreateUser(this);
            
        }

        public void Update()
        {
            Dal = new DataAccessLayer();
            Dal.UpdateUser(this);
        }

        public void Delete(int userId)
        {
            Dal = new DataAccessLayer();
            Dal.DeleteUser(userId);
        }

        public User GetUserbyId(int userId)
        {
            Dal = new DataAccessLayer();
            return Dal.GetUserById(userId);
        }

        public List<int> GetRolesById(int id) {
            Dal = new DataAccessLayer();
            Dal.GetRolesByUserId(id);
            List<int> UserRoles = new List<int>();
            foreach(Rol item in Dal.GetRolesByUserId(id))
            {
                UserRoles.Add(item.Id);
            }
            return UserRoles;
        }
    }
}
