using Garbage.Collection.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garbage.Collection.Business.Service.Interfaces
{
    public interface IEnderecoService
    {
        Task<IEnumerable<Endereco>> ObterEnderecos();
        Task<Endereco> ObterEnderecoById(int id);
        Task<Endereco> CriarEndereco(Endereco endereco);
        Task<Endereco> AtualizarEndereco(Endereco endereco);
        Task<Endereco> ExcluirEndereco(int id);
    }
}
