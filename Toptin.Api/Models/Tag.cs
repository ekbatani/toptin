using System;
using System.Collections.Generic;
using System.Text;

namespace Toptin.Api.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public string Title { get; set; }

        public int KalaId { get; set; }
        public Kala Kala { get; set; }
    }
}
