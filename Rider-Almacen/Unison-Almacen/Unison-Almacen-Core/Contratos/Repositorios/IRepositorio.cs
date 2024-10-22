namespace Unison_Almacen_Core.Contratos.Repositorios;

public interface IRepositorio<T>
{
    /*
     * 1. CREATE
     * 2. READ
     * 3. UPDATE
     * 4. DELETE
     */
    
    void Agregar(T productoNuevo);
    List<T> Listar();
    T ObtenerPorId(Guid id);
    void Modificar(T productoModificado);
    void Eliminar(T productoAEliminar);
}