namespace Unison_Almacen_Core.Contratos.Repositorios;

public interface IR2<T>
{
    void Agregar(T notaNueva);
    List<T> Listar();
    T ObtenerPorId(Guid id);
    void Modificar(T notaModificada);
    void Eliminar(T notaAEliminar);
}