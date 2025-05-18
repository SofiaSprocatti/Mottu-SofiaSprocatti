using System.ComponentModel.DataAnnotations.Schema;

namespace patioAPI.Models
{
    [Table("fiapmottu", Schema = "fleet")]
    public class Moto
    {
        public int VehicleId { get; set; }
        public required string Plate { get; set; }
        public required string Model { get; set; }
        public required string Localization { get; set; }
        public required string Branch { get; set; }
        public required string Court { get; set; }
    }
}

