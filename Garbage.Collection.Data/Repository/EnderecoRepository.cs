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
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly AppDbContext _context;
        public EnderecoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Endereco> Create(Endereco endereco)
        {
            _context.Enderecos.Add(endereco);
            _context.SaveChanges();
            return endereco;    
        }

        public async Task<Endereco> Delete(int id)
        {
            var endereco = await GetById(id);
            _context.Enderecos.Remove(endereco);
            _context.SaveChanges();
            return endereco;
        }

        public async Task<IEnumerable<Endereco>> Get()
        {
            return await _context.Enderecos.ToListAsync();
        }

        public async Task<Endereco> GetById(int id)
        {
            return await _context.Enderecos.Where(e => e.Id == id).FirstAsync();
        }

        public async Task<Endereco> Update(Endereco endereco)
        {
            _context.Entry(endereco).State = EntityState.Modified;
            _context.SaveChanges();
            return endereco;
        }
    }
}
