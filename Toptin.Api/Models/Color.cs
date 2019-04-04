using System;
using System.Collections.Generic;
using System.Text;

namespace Toptin.Api.Models
{
    public class Color
    {
        public int ColorId { get; set; }
        public string Title { get; set; }
        public int ColorNum { get; set; }

        public int KalaId { get; set; }
        public Kala Kala { get; set; }

    }
}
