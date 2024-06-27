namespace Garbage.Collection.API.ViewModels
{
    public class CaminhaoPaginacaoReferenciaViewModel
    {
        public IEnumerable<CaminhaoViewModel> Caminhoes { get; set; }
        public int PageSize { get; set; }
        public int Ref { get; set; }
        public int NextRef { get; set; }
        public string PreviousPageUrl => $"/Caminhao?referencia={Ref}&amp;tamanho={PageSize}";
        public string NextPageUrl => (Ref < NextRef) ? $"/Caminhao?referencia={NextRef}&amp;tamanho={PageSize}" : "";
    }
}
