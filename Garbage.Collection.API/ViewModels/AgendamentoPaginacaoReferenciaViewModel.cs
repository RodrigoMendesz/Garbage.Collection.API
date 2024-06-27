namespace Garbage.Collection.API.ViewModels
{
    public class AgendamentoPaginacaoReferenciaViewModel
    {
        public IEnumerable<AgendamentoViewModel> Agendamentos { get; set; }
        public int PageSize { get; set; }
        public int Ref { get; set; }
        public int NextRef { get; set; }
        public string PreviousPageUrl => $"/Agendamento?referencia={Ref}&amp;tamanho={PageSize}";
        public string NextPageUrl => (Ref < NextRef) ? $"/Agendamento?referencia={NextRef}&amp;tamanho={PageSize}" : "";
    }
}
