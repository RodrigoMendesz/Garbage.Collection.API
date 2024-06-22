namespace Garbage.Collection.API.Model
{
    public class Agendamento
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string? Descricao { get; set; }
        public Endereco? Endereco { get; set; }
    }
}
