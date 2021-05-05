using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ItemEyes.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Quantity { get; set; }

        public int OrderCount { get; set; }

        public DateTime ReceivedOn { get; set; }

        public string StorageZone { get; set; }

        [Required]
        public int LocationId { get; set; }

        [JsonIgnore]
        public Location Location { get; set; }
    }
}
