using Unison_Almacen_Core.Modelos;
using Unison_Almacen_Core.Repositorios;

namespace Unison_Almacen_Test.BaseDeDatos;

public class PruebaAgregarNotaConCamposValidos
{
    [Test]
    public void PruebaAgregarNotaConCamposValido()
    {
        var repositorio = new NotaRepositorio();

        var nuevaNota = new Nota
        {
            Id = Guid.NewGuid(),
            Titulo = "Nota de prueba",
            Contenido = "Contenido de prueba",
            Color = "Rojo"
        };

        Assert.DoesNotThrow(() => repositorio.Agregar(nuevaNota));
    }
}