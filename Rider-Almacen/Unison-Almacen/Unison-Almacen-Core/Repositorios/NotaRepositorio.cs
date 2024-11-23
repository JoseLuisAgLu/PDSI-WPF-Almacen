using Unison_Almacen_Core.Modelos;
using Unison_Almacen_Core.Contratos.Repositorios;
using Unison_Almacen_Core.BaseDeDatos;
namespace Unison_Almacen_Core.Repositorios
{
    public class NotaRepositorio : IR2<Nota>
    {
        public void Agregar(Nota notaNueva)
        {
            using var bd = new NotaBD();
        
            bd.Notas.Add(notaNueva);
        
            bd.SaveChanges();
        }
        public List<Nota> Listar()
        {
            using var bd = new NotaBD();

            return bd.Notas.ToList();
        }
        public Nota ObtenerPorId(Guid id)
        {
            using var bd = new NotaBD();
        
            var resultado = bd.Notas.Find(id);
         
            return resultado ?? new Nota();
        }
        public void Modificar(Nota notaModificada)
        {
            using var bd = new NotaBD();
        
            bd.Notas.Update(notaModificada);
        
            bd.SaveChanges();
        }
        
        public void Eliminar(Nota notaAEliminar)
        {
            using var bd = new NotaBD();
        
            bd.Notas.Remove(notaAEliminar);
        
            bd.SaveChanges();
        }
    }
}