namespace Garbage.Collection.API.ViewModels
{
    public class AgendamentoPaginacaoViewModel
    {
        public IEnumerable<AgendamentoViewModel> Agendamentos { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => Agendamentos.Count() == PageSize;
        public string PreviousPageUrl => HasPreviousPage ? $"/Agendamento?pagina={CurrentPage - 1}&amp;tamanho={PageSize}" : "";
        public string NextPageUrl => HasNextPage ? $"/Agendamento?pagina={CurrentPage + 1}&amp;tamanho={PageSize}" : "";
    }
}
