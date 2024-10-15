using Unison_Almacen_Core.Modelos;

namespace Unison_Almacen_Test.Modelos;

public class TestProducto
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void PruebaProductoId()
    {
        // 1. Crear un producto.
        var producto = new Producto();
        
        // 2. Asignarle un Id.
        var productoId = Guid.NewGuid();
        producto.Id = productoId;
        
        // 3. Comprobar que el Id asignado es el mismo
        //    que devuelve.
        Assert.That(producto.Id, Is.EqualTo(productoId));
    }

    [Test]
    public void PruebaProductoNombre()
    {
        /*
         * 1. Crear un producto.
         * 2. Asignarle un nombre.
         * 3. Comprobar que el nombre asignado es el
         *    mismo que devuelve.
         */
        
        // 1. Crear un producto.
        var producto = new Producto();
        
        // 2. Asignarle un nombre.
        const string nombreProducto = "Coca-Cola 2L";
        producto.Nombre = nombreProducto;
        
        // 3. Comprobar que el nombre asignado es el
        //    mismo que devuelve.
        Assert.That(producto.Nombre, Is.EqualTo(nombreProducto));
    }

    [Test]
    public void PruebaProductoDescripcion()
    {
        // 1. Crear un producto.
        var producto = new Producto();
        
        // 2. Asignar la descripción.
        const string descripcion = "Coca-Cola clásica de 2L";
        producto.Descripcion = descripcion;
        
        // 3. Comprobar que la descripción asignada es la
        //    misma que devuelve.
        Assert.That(producto.Descripcion, Is.EqualTo(descripcion));
    }

    [Test]
    public void PruebaProductoPrecio()
    {
        // 1. Crear un producto.
        var producto = new Producto();
        
        // 2. Asignar el precio.
        const float precioProducto = 29.5f;
        producto.Precio = precioProducto;
        
        // 3. Comprobar que el precio asigna es el
        //    mismo que devuelve.
        Assert.That(producto.Precio, Is.EqualTo(precioProducto));
    }
}