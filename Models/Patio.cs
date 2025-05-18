using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace patioAPI.Models
{
    [Table("patios", Schema = "RM99208")]
    public class Patio
    {
        [Key]
        public int CourtId { get; set; } // Identificador único do pátio
        public required string CourtLocal { get; set; } // Localização ou nome do pátio
        public int BranchId { get; set; } // Id da filial
        public required string Branch { get; set; } // Nome da filial
        public double AreaTotal { get; set; } // Área total utilizável em m²
        public int? MaxMotos { get; set; } // Capacidade máxima estimada de motos
        public int GridRows { get; set; } // Quantidade de linhas do grid (plano cartesiano)
        public int GridCols { get; set; } // Quantidade de colunas do grid (plano cartesiano)
    }
}
