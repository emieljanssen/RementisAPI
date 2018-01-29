using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RementisApi.Models
{
    public class Profile
    {
        [Key]
        public int CustomerId { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string Geslacht { get; set; }

        

    }
}
