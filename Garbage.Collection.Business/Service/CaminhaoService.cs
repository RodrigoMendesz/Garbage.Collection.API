﻿
using Garbage.Collection.Business.Service.Interfaces;
using Garbage.Collection.Data.Models;
using Garbage.Collection.Data.Repository.Interfaces;

namespace Garbage.Collection.Business.Service
{
    public class CaminhaoService : ICaminhaoService
    {
        private readonly ICaminhaoRepository _repository;
        public CaminhaoService(ICaminhaoRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Caminhao>> ObterCaminhao() => _repository.Get();
        public Task<Caminhao> ObterCaminhaoyId(int id) => _repository.GetById(id);
        public Task<Caminhao> CriarCaminhao(Caminhao caminhao) => _repository.Create(caminhao);
        public Task<Caminhao> AtualizarCaminhao(Caminhao caminhao) => _repository.Update(caminhao);
        public Task<Caminhao> ExcluirCaminhao(int id) => _repository.Delete(id);

    }
}