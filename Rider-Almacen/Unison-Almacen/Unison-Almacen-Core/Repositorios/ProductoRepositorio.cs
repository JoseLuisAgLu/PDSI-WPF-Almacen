using Unison_Almacen_Core.BaseDeDatos;
using Unison_Almacen_Core.Contratos.Repositorios;
using Unison_Almacen_Core.Modelos;

namespace Unison_Almacen_Core.Repositorios;

public class ProductoRepositorio : IRepositorio<Producto>
{
    public void Agregar(Producto productoNuevo)
    {
        // 1. Crear la conexión a la basse de datos.
        using var bd = new ProductoBD();
        
        // 2. Agregar el producto a la base de datos.
        bd.Productos.Add(productoNuevo);
        
        // 3. Guardar los cambios en la base de datos.
        bd.SaveChanges();
    }

    public List<Producto> Listar()
    {
        // 1. Crear una conexión a la base de datos.
        using var bd = new ProductoBD();

        // 2. Devolver una lista con todos los productos.
        return bd.Productos.ToList();
    }

    public Producto ObtenerPorId(Guid id)
    {
        // 1. Obtener la conexión con la base de datos.
        using var bd = new ProductoBD();
        
        // 2. Buscar el producto.
         var resultado = bd.Productos.Find(id);
         
        // 3. Devolver el producto.
        return resultado ?? new Producto();
    }

    public void Modificar(Producto productoModificado)
    {
        // 1. Crear conexión con la base de datos.
        using var bd = new ProductoBD();
        
        // 2. Modificar el producto.
        bd.Productos.Update(productoModificado);
        
        // 3. Guardar los cambios en la base de datos.
        bd.SaveChanges();
    }

    public void Eliminar(Producto productoAEliminar)
    {
        // 1. Crear conexión con la base de datos.
        using var bd = new ProductoBD();
        
        // 2. Eliminar el producto.
        bd.Productos.Remove(productoAEliminar);
        
        // 3. Actualizar la base de datos.
        bd.SaveChanges();
    }
}