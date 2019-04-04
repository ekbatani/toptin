using System;
using System.Collections.Generic;
using System.Text;

namespace Toptin.Api.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string Title { get; set; }
        public int Type { get; set; }
        public DateTime DateTime { get; set; }

        public int KalaId { get; set; }
        public Kala Kala { get; set; }
        public int ParentQuestionId { get; set; }
        public Question ParentQuestion { get; set; }
        public ICollection<Question> ParentQuestions { get; set; }
    }
}
