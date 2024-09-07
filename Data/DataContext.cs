using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
        :base(options)
        {
            
        }

        //Para las migraciones,modelo y nombre de la tabla
        public DbSet<Producto> Productos{ get; set; }
        
    }
}