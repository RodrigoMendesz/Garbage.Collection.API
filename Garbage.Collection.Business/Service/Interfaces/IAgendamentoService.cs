using Garbage.Collection.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garbage.Collection.Business.Service.Interfaces
{
    public interface IAgendamentoService
    {
        Task<IEnumerable<Agendamento>> ObterAgendamentos(int pageNumber, int pageSize);
        Task<Agendamento> ObterAgendamentoById(int id);
        Task<Agendamento> CriarAgendamento(Agendamento agendamento);
        Task<Agendamento> AtualizarAgendamento(Agendamento agendamento);
        Task<Agendamento> ExcluirAgendamento(int id);

    }
}
