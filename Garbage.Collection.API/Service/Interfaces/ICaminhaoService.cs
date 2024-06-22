using Garbage.Collection.API.Model;
using Garbage.Collection.API.ViewModels;

namespace Garbage.Collection.API.Service.Interfaces
{
    public interface ICaminhaoService
    {
        Task<IEnumerable<Caminhao>> ObterCaminhao();
        Task<Caminhao> ObterCaminhaoyId(int id);
        Task<Caminhao> CriarCaminhao(Caminhao caminhao);
        Task<Caminhao> AtualizarCaminhao(Caminhao caminhao);
        Task<Caminhao> ExcluirCaminhao(int id);
        
    }
}
