using Microsoft.EntityFrameworkCore;

namespace api_adota_pet.Models
{
    public class AppDbContext : DbContext 
    {
        public AppDbContext(DbContextOptions options) : base(options) 
        { 
        }
        
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Anuncio> Anuncios { get; set; }
        public DbSet<Interacao> Interacoes { get; set; }
    }
}
