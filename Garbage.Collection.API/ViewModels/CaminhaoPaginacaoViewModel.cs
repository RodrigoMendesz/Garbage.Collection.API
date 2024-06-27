namespace Garbage.Collection.API.ViewModels
{
    public class CaminhaoPaginacaoViewModel
    {
        public IEnumerable<CaminhaoViewModel> Caminhoes { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => Caminhoes.Count() == PageSize;
        public string PreviousPageUrl => HasPreviousPage ? $"/Caminhao?pagina={CurrentPage - 1}&amp;tamanho={PageSize}" : "";
        public string NextPageUrl => HasNextPage ? $"/Caminhao?pagina={CurrentPage + 1}&amp;tamanho={PageSize}" : "";
    }
}
