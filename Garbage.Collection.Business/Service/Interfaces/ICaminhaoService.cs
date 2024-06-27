using Garbage.Collection.Data.Models;

namespace Garbage.Collection.Business.Service.Interfaces
{
    public interface ICaminhaoService
    {
        Task<IEnumerable<Caminhao>> ObterCaminhao(int pageNumber, int pageSize);
        Task<Caminhao> ObterCaminhaoyId(int id);
        Task<Caminhao> CriarCaminhao(Caminhao caminhao);
        Task<Caminhao> AtualizarCaminhao(Caminhao caminhao);
        Task<Caminhao> ExcluirCaminhao(int id);
        
    }
}
