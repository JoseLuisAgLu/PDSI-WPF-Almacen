using Unison_Almacen_Core.Modelos;
using Unison_Almacen_Core.Contratos.Repositorios;
using Unison_Almacen_Core.BaseDeDatos;
using System.Collections.Generic;
using System.Linq;
namespace Unison_Almacen_Core.Repositorios
{
    public class NotaRepositorio
    {
        private readonly List<Nota> notas = new();

        public List<Nota> ListarNotas()
        {
            return notas;
        }

        public void AgregarNota(Nota nueva)
        {
            nueva.Id = Guid.NewGuid(); // Generar un identificador único
            notas.Add(nueva);
        }


        public void ActualizarNota(Nota modificada)
        {
            var nota = notas.FirstOrDefault(n => n.Id == modificada.Id);
            if (nota != null)
            {
                nota.Titulo = modificada.Titulo;
                nota.Contenido = modificada.Contenido;
            }
        }

        public void EliminarNota(Guid id)
        {
            var nota = notas.FirstOrDefault(n => n.Id == id);
            if (nota != null)
            {
                notas.Remove(nota);
            }
        }
    }
}