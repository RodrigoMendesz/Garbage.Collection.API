using Garbage.Collection.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garbage.Collection.Data.Repository.Interfaces
{
    public interface IAgendamentoRepository
    {
        Task<IEnumerable<Agendamento>> Get(int pageNumber, int pageSize);
        Task<Agendamento> GetById(int id);
        Task<Agendamento> Create(Agendamento agendamento);
        Task<Agendamento> Update(Agendamento agendamento);
        Task <Agendamento> Delete(int id);
    }
}
