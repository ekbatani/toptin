using System;
using System.Collections.Generic;
using System.Text;

namespace Toptin.Api.Models
{
    public class RequestDetail
    {
        public int RequestDetailId { get; set; }
        public string Title { get; set; }
        public int Num { get; set; }
        public int Status { get; set; }
        public int Price { get; set; }

        public int RequstId { get; set; }
        public Request Request { get; set; }
        public int KalaId { get; set; }
        public Kala Kala { get; set; }
    }
}
