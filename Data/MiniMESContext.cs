using Microsoft.EntityFrameworkCore;
using MiniMES.API.Models;

namespace MiniMES.API.Data
{
    public class MiniMESContext : DbContext
    {
        public MiniMESContext(DbContextOptions<MiniMESContext> options) : base(options) { }

        public DbSet<MaquinaModel> Maquinas => Set<MaquinaModel>();
        public DbSet<OrdemProducaoModel> Ordens => Set<OrdemProducaoModel>();
        public DbSet<EventoProducaoModel> Eventos => Set<EventoProducaoModel>();
    }
}