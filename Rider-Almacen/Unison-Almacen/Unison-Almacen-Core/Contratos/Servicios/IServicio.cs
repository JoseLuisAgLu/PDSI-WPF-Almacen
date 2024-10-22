namespace Unison_Almacen_Core.Contratos.Servicios;

public interface IServicio<T>
{
    void Agregar(T productoNuevo);
    List<T> Listar();
    T ObtenerPorId(Guid id);
    void Modificar(T productoModificado);
    void Eliminar(T productoAEliminar);
}