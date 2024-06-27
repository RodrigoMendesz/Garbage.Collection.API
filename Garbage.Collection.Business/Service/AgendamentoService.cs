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

    public class AgendamentoService : IAgendamentoService
    {
        private readonly IAgendamentoRepository _service;
        public AgendamentoService(IAgendamentoRepository service)
        {
            _service = service;
        }
        public Task<IEnumerable<Agendamento>> ObterAgendamentos(int pageNumber, int pageSize) => _service.Get(pageNumber, pageSize);
        public Task<Agendamento> ObterAgendamentoById(int id) => _service.GetById(id);
        public Task<Agendamento> AtualizarAgendamento(Agendamento agendamento) => _service.Update(agendamento);
        public Task<Agendamento> CriarAgendamento(Agendamento agendamento) => _service.Create(agendamento);
        public Task<Agendamento> ExcluirAgendamento(int id) => _service.Delete(id);
    }
}
