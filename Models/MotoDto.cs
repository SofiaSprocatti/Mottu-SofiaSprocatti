namespace patioAPI.Models
{
    public class MotoDto
    {
        public int Id { get; set; }
        public required string Placa { get; set; }
        public required string Modelo { get; set; }
        public int Ano { get; set; }
    }
}

