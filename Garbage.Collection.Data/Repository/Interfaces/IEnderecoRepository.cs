using Garbage.Collection.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garbage.Collection.Data.Repository.Interfaces
{
    public interface IEnderecoRepository
    {
        Task<IEnumerable<Endereco>> Get();
        Task<Endereco> GetById(int id);
        Task<Endereco> Create(Endereco endereco);
        Task<Endereco> Update(Endereco endereco);
        Task <Endereco>Delete(int id);
    }
}
