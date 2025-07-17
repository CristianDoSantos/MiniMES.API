namespace MiniMES.API.DTOs
{
    public class MaquinaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string IP { get; set; } = string.Empty;
        public bool Ativa { get; set; }
    }
}
