using Microsoft.EntityFrameworkCore;
using Unison_Almacen_Core.Modelos;

namespace Unison_Almacen_Core.BaseDeDatos
{
    public class NotaBD : DbContext
    {
        private const string NombreBaseDeDatos = "notas.db";

        public DbSet<Nota> Notas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var directorio = AppContext.BaseDirectory + "/_data";
            if (!Directory.Exists(directorio)) Directory.CreateDirectory(directorio);
            optionsBuilder.UseSqlite($"Filename={directorio}/{NombreBaseDeDatos}");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Nota>(nota =>
            {
                nota.ToTable(nameof(Notas));
                nota.HasKey(n => n.Id);
                nota.Property(n => n.Titulo).IsRequired();
                nota.Property(n => n.Contenido).IsRequired();
                nota.Property(n => n.Color).IsRequired();
            });
        }
    }
}


