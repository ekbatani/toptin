using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Toptin.Api.Models
{
    public class Store
    {
        [Key]
        public int StoreId { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public int Star { get; set; }
        public int StarNum { get; set; }
        public int Hit { get; set; }

        public string Id { get; set; }
        public User User { get; set; }
    }
}
