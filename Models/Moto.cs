using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace patioAPI.Models
{
    [Table("fiapmottu", Schema = "RM99208")]
    public class Moto
    {
        [Key]
        public int VehicleId { get; set; }
        public required string Plate { get; set; }
        public required string Model { get; set; }
        public required string Localization { get; set; }
        public required string Branch { get; set; }
        public required string Court { get; set; }
    }
}

