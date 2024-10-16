using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Unison_Almacen_Core.Modelos;

namespace Unison_Almacen_Core.BaseDeDatos;

public class ProductBD : DbContext
{
    private const string NombreBaseDeDatos = "datos.db";
    
    /// <summary>
    /// Configura la direccion de la base de datos.
    /// </summary>
    public DbSet<Producto> Productos
    {
        get;
        set;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var directorio = AppContext.BaseDirectory;
        // 1.   Asegurar que existe el directorio de la base de datos.
        if (!Directory.Exists(directorio)) Directory.CreateDirectory(directorio);
        // 2.   Configuramos el nombre de la base de datos.
        optionsBuilder.UseSqlite($"Filename={directorio}/{NombreBaseDeDatos}",
            op => op.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));
        // 3.   Terminar con la configuracion
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Definir la tabla producto
        modelBuilder.Entity<Producto>(producto =>
        {
            producto.ToTable(nameof(Productos));
            producto.HasKey(p => p.Id);
            producto.Property(p => p.Nombre).IsRequired();
            producto.Property(p => p.Descripcion).IsRequired();
            producto.Property(p => p.Precio).IsRequired();
        });
    }
    
}