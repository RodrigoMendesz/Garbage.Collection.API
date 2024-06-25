namespace Garbage.Collection.Data.Models
{
    public class Agendamento
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string? Descricao { get; set; }
        public Endereco? Endereco { get; set; }
    }
}
