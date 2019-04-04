using System;
using System.Collections.Generic;
using System.Text;

namespace Toptin.Api.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Desc { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public DateTime DateTime { get; set; }
        public int KeyfiyatSakht { get; set; }
        public int Tarahi { get; set; }
        public int ArzeshKharid { get; set; }

        public int KalaId { get; set; }
        public Kala Kala { get; set; }
    }
}
