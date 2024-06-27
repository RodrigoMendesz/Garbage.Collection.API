using Garbage.Collection.Data.Models;

namespace Garbage.Collection.Data.Repository.Interfaces
{
    public interface ICaminhaoRepository
    {
        Task<IEnumerable<Caminhao>> Get(int pageNumber, int pageSize);
        Task<Caminhao> GetById(int id);
        Task<Caminhao> Create(Caminhao caminhao);
        Task<Caminhao> Update(Caminhao caminhao);
        Task<Caminhao> Delete(int id);
    }
}
