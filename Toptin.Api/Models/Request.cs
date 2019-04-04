using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Toptin.Api.Models
{
    public class Request
    {
        [Key]
        public int RequestId { get; set; }
        public string Title { get; set; }
        public DateTime DateTime { get; set; }
        public int Status { get; set; }
        public int Price { get; set; }

        public string Id { get; set; }
        public User User { get; set; }
    }
}
