namespace patioAPI.Models
{
    public class AlertaProximidadeDto
    {
        public int VehicleId { get; set; }
        public int CourtId { get; set; }
        public int BranchId { get; set; }
        public string Position { get; set; } = string.Empty; // Inicializando a propriedade
        public int X { get; set; }
        public int Y { get; set; }
        public int Distancia { get; set; }
        public List<VeiculoProximoDto> VeiculosProximos { get; set; } = new List<VeiculoProximoDto>(); // Inicializando a propriedade
    }

    public class VeiculoProximoDto
    {
        public int VehicleId { get; set; }
        public required string Plate { get; set; } = string.Empty; // Inicializando a propriedade
        public required string Model { get; set; } = string.Empty; // Inicializando a propriedade
        public int X { get; set; }
        public int Y { get; set; }
        public string Position { get; set; } = string.Empty; // Inicializando a propriedade
    }
}