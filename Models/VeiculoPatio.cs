using System.ComponentModel.DataAnnotations.Schema;

namespace patioAPI.Models
{
    [Table("veiculopatio", Schema = "fleet")]
    public class VeiculoPatio
    {
        public int VehicleId { get; set; } // Id do veículo
        public int CourtId { get; set; } // Id do pátio
        public int BranchId { get; set; } // Id da filial
        public required string Position { get; set; } // Posição do veículo dentro do pátio
        public int X { get; set; } // Posição X no grid do pátio
        public int Y { get; set; } // Posição Y no grid do pátio
    }
}
