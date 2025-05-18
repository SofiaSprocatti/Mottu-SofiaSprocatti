using System.ComponentModel.DataAnnotations.Schema;

namespace patioAPI.Models
{
    [Table("filiais")]
    public class Filial
    {
        public int BranchId { get; set; }
        public required string? Branch { get; set; } // nome da filial
        public required string? Address { get; set; }
        public required string? Cidade { get; set; }
        public required string? Estado { get; set; }
    }
}

