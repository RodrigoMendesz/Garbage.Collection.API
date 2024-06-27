
using Garbage.Collection.Data.Context;
using Garbage.Collection.Data.Models;
using Garbage.Collection.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Garbage.Collection.Data.Repository
{
    public class CaminhaoRepository : ICaminhaoRepository
    {
        private readonly AppDbContext _context;
        public CaminhaoRepository(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Caminhao> GetAll(int page, int size)
        {
            return _context.Caminhao.Skip((page - 1) * page)
                                        .Take(size)
                                        .AsNoTracking()
                                        .ToList();
        }
        public IEnumerable<Caminhao> GetAllReference(int lastReference, int size)
        {
            var caminhoes = _context.Caminhao.Where(c => c.Id > lastReference)
                                .OrderBy(c => c.Id)
                                .Take(size)
                                .AsNoTracking()
                                .ToList();
            return caminhoes;
        }
        public async Task<IEnumerable<Caminhao>> Get(int pageNumber, int pageSize)
        {
            return await _context.Caminhao
                                 .Skip((pageNumber - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
        }
        public async Task<Caminhao> GetById(int id)
         {
            return await _context.Caminhao.Where(c => c.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Caminhao> Create(Caminhao caminhao)
        {
            _context.Caminhao.Add(caminhao);
            _context.SaveChanges();
            return caminhao;
        }

        public async Task<Caminhao> Delete(int id)
        {
            var caminhao = await GetById(id);
            _context.Caminhao.Remove(caminhao);
            _context.SaveChanges();
            return caminhao;
        }


        public async Task<Caminhao> Update(Caminhao caminhao)
        {
            _context.Entry(caminhao).State = EntityState.Modified;
            _context.SaveChanges();
            return caminhao;
        }
    }
}
