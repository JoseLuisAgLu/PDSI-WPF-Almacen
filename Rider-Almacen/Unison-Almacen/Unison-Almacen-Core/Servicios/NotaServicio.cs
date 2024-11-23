using Unison_Almacen_Core.Modelos;
using Unison_Almacen_Core.Contratos.Servicios;
using Unison_Almacen_Core.Contratos.Repositorios;

namespace Unison_Almacen_Core.Servicios
{
    public class NotaServicio : IS2<Nota>
    {
        private readonly IR2<Nota> _repositorio;

        public NotaServicio(IR2<Nota> repositorio)
        {
            _repositorio = repositorio;
        }

        public void Agregar(Nota notaNueva) => _repositorio.Agregar(notaNueva);

        public List<Nota> Listar() => _repositorio.Listar();

        public Nota ObtenerPorId(Guid id) => _repositorio.ObtenerPorId(id);

        public void Modificar(Nota notaModificada) => _repositorio.Modificar(notaModificada);

        public void Eliminar(Nota notaAEliminar) => _repositorio.Eliminar(notaAEliminar);
    }
}