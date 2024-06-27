namespace Garbage.Collection.API.ViewModels
{
    public class EnderecoPaginacaoViewModel
    {
        public IEnumerable<EnderecoViewModel> Enderecos { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => Enderecos.Count() == PageSize;
        public string PreviousPageUrl => HasPreviousPage ? $"/Endereco?pagina={CurrentPage - 1}&amp;tamanho={PageSize}" : "";
        public string NextPageUrl => HasNextPage ? $"/Endereco?pagina={CurrentPage + 1}&amp;tamanho={PageSize}" : "";
    }

}
