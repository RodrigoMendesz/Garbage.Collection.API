namespace Garbage.Collection.API.ViewModels
{
    public class EnderecoPaginacaoReferenciaViewModel
    {
        public IEnumerable<EnderecoViewModel> Enderecos { get; set; }
        public int PageSize { get; set; }
        public int Ref { get; set; }
        public int NextRef { get; set; }
        public string PreviousPageUrl => $"/Endereco?referencia={Ref}&amp;tamanho={PageSize}";
        public string NextPageUrl => (Ref < NextRef) ? $"/Endereco?referencia={NextRef}&amp;tamanho={PageSize}" : "";
    }
}
