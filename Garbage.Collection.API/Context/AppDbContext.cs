using Garbage.Collection.API.Model;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Diagnostics;

namespace Garbage.Collection.API.Contexto
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


