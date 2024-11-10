using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasusExotischNederland.Model
{
    public class Answer
    {
        public int Id { get; set; }
        public Question Question { get; set; }
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }

        public Answer(int id, Question question, string answerText, bool isCorrect)
        {
            Id = id;
            Question = question;
            AnswerText = answerText;
            IsCorrect = isCorrect;
        }

        public void GetAll() { }
        public void Create() { }
        public void Update() { }
        public void Delete() { }
    }
}
