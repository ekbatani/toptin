using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Toptin.Api.Models
{
    public class User : IdentityUser
    {
        public int SmsSentNumber { get; set; }
    }
}
