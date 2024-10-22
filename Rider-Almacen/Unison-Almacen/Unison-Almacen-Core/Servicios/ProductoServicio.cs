using Unison_Almacen_Core.Contratos.Repositorios;
using Unison_Almacen_Core.Contratos.Servicios;
using Unison_Almacen_Core.Modelos;

namespace Unison_Almacen_Core.Servicios;

public class ProductoServicio(IRepositorio<Producto> repositorio) : IServicio<Producto>
{
    public void Agregar(Producto productoNuevo)
    {
        repositorio.Agregar(productoNuevo);
    }

    public List<Producto> Listar()
    {
        return repositorio.Listar();
    }

    public Producto ObtenerPorId(Guid id)
    {
        return repositorio.ObtenerPorId(id);
    }

    public void Modificar(Producto productoModificado)
    {
        repositorio.Modificar(productoModificado);
    }

    public void Eliminar(Producto productoAEliminar)
    {
        repositorio.Eliminar(productoAEliminar);
    }
}