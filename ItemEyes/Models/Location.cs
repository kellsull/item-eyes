using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ItemEyes.Models
{
    public class Location
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }

        //Navigational
        public IEnumerable<Item> Items { get; set; }
    }
}
