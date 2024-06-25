using Garbage.Collection.Business.Service.Interfaces;
using Garbage.Collection.Data.Models;
using Garbage.Collection.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garbage.Collection.Business.Service
{
    public class EnderecoService : IEnderecoService
    {
        private readonly IEnderecoRepository _repository;
        public EnderecoService(IEnderecoRepository repository)
        {
            _repository = repository;
        }
        Task<Endereco> IEnderecoService.AtualizarEndereco(Endereco endereco) => _repository.Update(endereco);
        

        Task<Endereco> IEnderecoService.CriarEndereco(Endereco endereco) => _repository.Create(endereco);
        

        Task<Endereco> IEnderecoService.ExcluirEndereco(int id) => _repository.Delete(id);
        

        Task<Endereco> IEnderecoService.ObterEnderecoById(int id) => _repository.GetById(id);

        Task<IEnumerable<Endereco>> IEnderecoService.ObterEnderecos() => _repository.Get();
    }
}
