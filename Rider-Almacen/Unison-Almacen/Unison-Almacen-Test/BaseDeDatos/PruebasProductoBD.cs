using Unison_Almacen_Core.BaseDeDatos;
using Unison_Almacen_Core.Modelos;

namespace Unison_Almacen_Test.BaseDeDatos;

public class PruebasProductoBD
{
    [Test]
    public void PruebaAgregarProducto()
    {
        // 1. Crear la conexión con la base de datos.
        using var db = new ProductoBD(); 
        
        // 2. Eliminar el contenido de la base de datos.
        db.Database.EnsureDeleted();
        
        // 3. Asegurar que la base de datos exista.
        db.Database.EnsureCreated();
       
       // 4. Crear id del producto.
       var id = Guid.NewGuid();
        
       // 5. Crear un producto.
       Producto producto = new()
       {
           Id = id,
           Nombre = "Producto 1",
           Descripcion = "Descripcion 1",
           Precio = 500f
       };

       // 6. Añadir el producto a la base de datos.
       db.Productos.Add(producto);
       db.SaveChanges();
        
       // 7. Consultar los productos para comprobar que se añadió el producto.
       var resultado = db.Productos.Find(id);
        
       Assert.That(resultado, Is.Not.Null, "No se agregó el producto.");
       Assert.That(resultado.Id, Is.EqualTo(id), "Producto con Id incorrecto.");
    }
    
    [Test]
    public void PruebaModificarProducto()
    {
        // 1. Crear la conexión con la base de datos.
        using var db = new ProductoBD(); 
        
        // 2. Eliminar el contenido de la base de datos.
        db.Database.EnsureDeleted();
        
        // 3. Asegurar que la base de datos exista.
        db.Database.EnsureCreated();
       
        // 4. Crear id del producto.
        var id = Guid.NewGuid();
        
        // 5. Crear un producto.
        Producto producto = new()
        {
            Id = id,
            Nombre = "Producto 1",
            Descripcion = "Descripcion 1",
            Precio = 500f
        };

        // 6. Añadir el producto a la base de datos.
        db.Productos.Add(producto);
        db.SaveChanges();
        
        // 7. Modificar el producto.
        var nuevoNombre = "Producto 2";
        producto.Nombre = nuevoNombre;
        
        // 8. Guardar cambios en la bd.
        db.Productos.Update(producto);
        db.SaveChanges();
        
        // 7. Consultar los productos para comprobar que se añadió el producto.
        var resultado = db.Productos.Find(id);
        
        Assert.That(resultado, Is.Not.Null, "No se agregó el producto.");
        Assert.That(resultado.Id, Is.EqualTo(id), "Producto con Id incorrecto.");
        Assert.That(resultado.Nombre, Is.EqualTo(nuevoNombre), "Producto con Nombre incorrecto.");
    }
    
    [Test]
    public void PruebaEliminarProducto()
    {
        // 1. Crear la conexión con la base de datos.
        using var db = new ProductoBD(); 
        
        // 2. Eliminar el contenido de la base de datos.
        db.Database.EnsureDeleted();
        
        // 3. Asegurar que la base de datos exista.
        db.Database.EnsureCreated();
       
        // 4. Crear id del producto.
        var id = Guid.NewGuid();
        
        // 5. Crear un producto.
        Producto producto = new()
        {
            Id = id,
            Nombre = "Producto 1",
            Descripcion = "Descripcion 1",
            Precio = 500f
        };

        // 6. Añadir el producto a la base de datos.
        db.Productos.Add(producto);
        db.SaveChanges();
        
        // 7. Consultar los productos para comprobar que se añadió el producto.
        var resultado = db.Productos.Find(id);
        
        // 8. Comprobar que el producto se añadió correctamente.
        Assert.That(resultado, Is.Not.Null, "No se agregó el producto.");
        Assert.That(resultado.Id, Is.EqualTo(id), "Producto con Id incorrecto.");
        
        // 9. Eliminar el producto de la base de datos.
        db.Productos.Remove(producto);
        db.SaveChanges();
        
        // 10. Comprobar que el producto se eliminó de la base de datos.
        Assert.That(db.Productos.Find(id), Is.Null, "Producto aún existe.");
    }
}