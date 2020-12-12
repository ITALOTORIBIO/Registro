using Microsoft.EntityFrameworkCore;
using Registro.Models;

namespace Registro.Data
{
    public class WebAppContext : DbContext
    {
        public DbSet<Productos> Productos { get; set; }
        
        public WebAppContext(DbContextOptions dco) : base(dco){

        }
    }
}