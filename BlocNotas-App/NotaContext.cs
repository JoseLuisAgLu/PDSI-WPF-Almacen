using BlocNotas_Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BlocNotas
{
    public class NotaContext : DbContext
    {
        // Nombre de la base de datos
        private const string NombreBaseDeDatos = "notas.db";

        // Tabla de notas
        public DbSet<Nota> Notas { get; set; }

        // Configurar la dirección de la base de datos
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Directorio donde se guarda la base de datos
            var directorio = AppContext.BaseDirectory;

            // Configuramos la base de datos en SQLite
            optionsBuilder.UseSqlite($"Filename={directorio}/{NombreBaseDeDatos}");
            
            base.OnConfiguring(optionsBuilder);
        }

        // Configurar la estructura de la base de datos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Nota>(nota =>
            {
                nota.ToTable(nameof(Notas));  // Tabla Notas
                nota.HasKey(n => n.Id);      // Clave primaria
                nota.Property(n => n.Titulo).IsRequired();
                nota.Property(n => n.Contenido).IsRequired();
                nota.Property(n => n.Color).IsRequired();
            });
        }
    }
}