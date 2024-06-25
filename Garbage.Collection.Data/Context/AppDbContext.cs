using Garbage.Collection.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Garbage.Collection.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Agendamento> Agendamentos { get; set; }
        public DbSet<Caminhao> Caminhao { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
    }
}


