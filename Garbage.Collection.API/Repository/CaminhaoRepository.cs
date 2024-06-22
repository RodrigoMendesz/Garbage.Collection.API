using Garbage.Collection.API.Contexto;
using Garbage.Collection.API.Model;
using Garbage.Collection.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Garbage.Collection.API.Repository
{
    public class CaminhaoRepository : ICaminhaoRepository
    {
        private readonly AppDbContext _context;
        public CaminhaoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Caminhao>> Get()
        {
            return await _context.Caminhao.ToListAsync();
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
