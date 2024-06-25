using Garbage.Collection.Data.Models;

namespace Garbage.Collection.API.ViewModels
{
    public class AgendamentoViewModel
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string? Descricao { get; set; }
        public Endereco? Endereco { get; set; }
    }
}
