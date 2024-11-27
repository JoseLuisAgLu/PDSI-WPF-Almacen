using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Unison_Almacen_Core.Contratos.Servicios;
using Unison_Almacen_Core.Modelos;

namespace Unison_Almacen_App.ViewModel;

public partial class NotaGeneralViewModel : ObservableObject
{
    [ObservableProperty] private Nota _nota = new Nota();
    [ObservableProperty] private List<Nota> _notas;

    [ObservableProperty] private List<string> _colores = new List<string> { "Rojo", "Azul", "Verde", "Amarillo", "Blanco", "Negro" };

    [ObservableProperty] private string _txtBotonFormulario;
    private const string TXT_AGREGAR = "Agregar";
    private const string TXT_MODIFICAR = "Modificar";

    private readonly IServicio<Nota> _servicio;

    public NotaGeneralViewModel(IServicio<Nota> servicio)
    {
        // Definir los comandos
        AgregarNotaCommand = new RelayCommand(AgregarNota);
        EliminarNotaCommand = new RelayCommand<Nota>(EliminarNota);

        // Guardar la referencia del servicio
        _servicio = servicio;

        // Obtener las notas de la base de datos
        _notas = _servicio.Listar();

        // Texto del formulario
        _txtBotonFormulario = TXT_AGREGAR;
    }

    public ICommand AgregarNotaCommand { get; }
    public ICommand EliminarNotaCommand { get; }

    private void AgregarNota()
    {
        var n = Nota;
        if (string.IsNullOrWhiteSpace(n.Titulo) || string.IsNullOrWhiteSpace(n.Contenido) || string.IsNullOrWhiteSpace(n.Color)) return;

        // Comprobar si la nota existe
        var notaExistente = _servicio.ObtenerPorId(n.Id);

        // Si la nota no existe, se agrega
        if (notaExistente.Id == Guid.Empty)
        {
            _servicio.Agregar(n);
        }
        // Si la nota existe, se modifica
        else
        {
            _servicio.Modificar(n);
        }

        // Limpiar el formulario
        Nota = new Nota();

        // Actualizar la lista
        Notas = _servicio.Listar();
    }

    private void EliminarNota(Nota? nota)
    {
        if (nota == null) return;

        _servicio.Eliminar(nota);
        Notas = _servicio.Listar();
    }

    partial void OnNotaChanged(Nota? oldValue, Nota newValue)
    {
        TxtBotonFormulario = newValue.Id != Guid.Empty ? TXT_MODIFICAR : TXT_AGREGAR;
        Nota = newValue;
    }
}