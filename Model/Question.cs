using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasusExotischNederland.Model
{
    public class Question
    {
        public int Id { get; set; }
        public List<Answer> Answers { get; set; }
        public List<User> Users { get; set; }
        public string QuestionText { get; set; }
        public string Type { get; set; }
        public int AmountOfPoints { get; set; }

        public Question()
        {
            
        }

        public Question(int id, string questionText, string type, int amountOfPoints)
        {
            Id = id;
            QuestionText = questionText;
            Type = type;
            AmountOfPoints = amountOfPoints;
            Answers = new List<Answer>();
            Users = new List<User>();
        }

        public void GetAll() { }
        public void Create() { }
        public void Update() { }
        public void Delete() { }
    }
}
