using Unison_Almacen_Core.Modelos;
using Unison_Almacen_Core.Contratos.Servicios;
using Unison_Almacen_Core.Contratos.Repositorios;

namespace Unison_Almacen_Core.Servicios
{
    public class NotaServicio (IR2<Nota> _repositorio): IS2<Nota>
    {

        public void Agregar(Nota notaNueva)
        {
            _repositorio.Agregar(notaNueva);
        }

        public List<Nota> Listar()
        {
            return _repositorio.Listar();
        }
       
        public Nota ObtenerPorId(Guid id)
        {
           return _repositorio.ObtenerPorId(id);
        }

        public void Modificar(Nota notaModificada)
        {
            _repositorio.Modificar(notaModificada);
        }

        public void Eliminar(Nota notaAEliminar)
        {
            _repositorio.Eliminar(notaAEliminar);
        }
    }
}