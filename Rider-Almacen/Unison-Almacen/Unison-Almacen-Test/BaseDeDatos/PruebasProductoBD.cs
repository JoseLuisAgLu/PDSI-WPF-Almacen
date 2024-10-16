using Unison_Almacen_Core.BaseDeDatos;
using Unison_Almacen_Core.Modelos;

namespace Unison_Almacen_Test.BaseDeDatos;

public class PruebasProductoBD
{
    
    private ProductBD _productoDB;
    
    [SetUp]
    public void Setup()
    {
        _productoDB = new ProductBD();          // 1.   Crea una instancia de la base de datos.
        _productoDB.Database.EnsureCreated();   // 2.   Se asegura que existe la base de datos.
    }

    [TearDown]
    public void TearDown()
    {
        _productoDB.Database.EnsureDeleted();   // 1.   Elimina la base de datos.
        _productoDB.Dispose();                  // 2.   Libera los recursos de la conexion.
    }

    [Test]
    public void PruebaAgregarProducto()
    {
        // 1.   Crear el Id del producto
        var id = Guid.NewGuid();
        
        // 2.   Crear un producto
        Producto producto = new()
        {
            Id = id,
            Nombre = "Producto 1",
            Descripcion = "Descripcion 1",
            Precio = 500f
        };
        // 3.   A;adir el producto a la base de datos.
        _productoDB.Productos.Add(producto);
        _productoDB.SaveChanges();
        
        // 4.   Consultar los productos para comprobar que se a;adio el producto
        var resultado = _productoDB.Productos.Find(id);
        
        Assert.That(resultado, Is.Not.Null, "No se agrego el producto.");
        Assert.That(resultado.Id, Is.EqualTo(id));
    }
}