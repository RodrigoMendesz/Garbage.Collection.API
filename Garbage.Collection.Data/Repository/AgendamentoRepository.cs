using Garbage.Collection.Data.Context;
using Garbage.Collection.Data.Models;
using Garbage.Collection.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garbage.Collection.Data.Repository
{
    public class AgendamentoRepository : IAgendamentoRepository
    {
        private readonly AppDbContext _context;
        public AgendamentoRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public IEnumerable<Agendamento> GetAll(int page, int size)
        {
            return _context.Agendamentos.Skip((page - 1) * page)
                                        .Take(size)
                                        .AsNoTracking()
                                        .ToList();
        }
        public IEnumerable<Agendamento> GetAllReference(int lastReference, int size)
        {
            var agendamentos = _context.Agendamentos.Where(c => c.Id > lastReference)
                                .OrderBy(c => c.Id)
                                .Take(size)
                                .AsNoTracking()
                                .ToList();
            return agendamentos;
        }
        public async Task<IEnumerable<Agendamento>> Get(int pageNumber, int pageSize)
        {
            return await _context.Agendamentos
                                 .Skip((pageNumber - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
        }

        public async Task<Agendamento> GetById(int id)
        {
            return await _context.Agendamentos.Where(a => a.Id == id).FirstAsync();
        }
        public async Task<Agendamento> Create(Agendamento agendamento)
        {
            _context.Add(agendamento);
            _context.SaveChanges();
            return agendamento;
        }

        public async Task<Agendamento> Update(Agendamento agendamento)
        {
            _context.Entry(agendamento).State = EntityState.Modified;
            _context.SaveChanges();
            return agendamento;
        }

        public async Task<Agendamento> Delete(int id)
        {
            var agendamento = await GetById(id);
            _context.Agendamentos.Remove(agendamento);
            _context.SaveChanges();
            return agendamento;
        } 
    }
}
