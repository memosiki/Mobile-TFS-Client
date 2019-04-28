
using App5.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;

//using App5.Models.DTO.Identity;

namespace App5.Models
{
    public class Worker
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public Identity IdentityJson { get; set; }

        public override string ToString()
        {
           return Name;
        }
    }
}
