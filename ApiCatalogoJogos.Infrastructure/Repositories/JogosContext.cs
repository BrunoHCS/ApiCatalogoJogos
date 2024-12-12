using ApiCatalogoJogos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogoJogos.Infrastructure.Repositories
{
    public class JogosContext : DbContext
    {
        public JogosContext(DbContextOptions<JogosContext> options) : base(options) {   }

        public DbSet<Jogo> Jogos { get; set; }
    }
}
