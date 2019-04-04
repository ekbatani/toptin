using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace Toptin.Api.Models
{
    public class Favorit
    {
        [Key]
        public int FavoritId { get; set; }

        public string Id { get; set; }
        public User User { get; set; }
        public int KalaId { get; set; }
        public Kala Kala { get; set; }
    }
}
