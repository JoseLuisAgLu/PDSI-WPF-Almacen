using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Unison_Almacen_Core.Contratos.Servicios;
using Unison_Almacen_Core.Modelos;
using Unison_Almacen_Core.Servicios;

namespace Unison_Almacen_App.ViewModel
{
    public partial class NotaViewModel : ObservableObject
    { [ObservableProperty] private Nota _nota = new Nota();
    [ObservableProperty] private List<Nota> _notas;

    [ObservableProperty] private string _txtBotonFormulario;
    private const string TXT_AGREGAR = "Agregar";
    private const string TXT_MODIFICAR = "Modificar";
    
    private IS2<Nota> _servicio;

    public NotaViewModel(IS2<Nota> servicio)
    {
        // Definir el comando.
        AgregarNotaCommand = new RelayCommand(AgregarNota);
        EliminarNotaCommand = new RelayCommand<Nota>(EliminarNota);
        
        // Guardar la referencia del servicio.
        _servicio = servicio;
        
        // Obtener los productos de la base de datos.
        _notas = _servicio.Listar();
        
        // Texto del formulario.
        _txtBotonFormulario = TXT_AGREGAR;
    }
    
    public ICommand AgregarNotaCommand { get; }
    public ICommand EliminarNotaCommand { get; }

    private void AgregarNota()
    {
        var n = Nota;
        if (string.IsNullOrWhiteSpace(n.Titulo) || string.IsNullOrWhiteSpace(n.Contenido)) return;
        
        // Comprobar si el elemento existe.
        var producto = _servicio.ObtenerPorId(n.Id);
        
        // Si el producto no existe se agrega
        if (producto.Id == Guid.Empty)
        {
            // Agregar el producto.
            _servicio.Agregar(n);
        }
        // Si el producto existe se modifica.
        else
        {
            // Modificar el producto.
            _servicio.Modificar(n);
        }
            
        // Borramos los datos del formulario.
        Nota.Id = Guid.Empty;
        Nota.Titulo = string.Empty;
        Nota.Contenido = string.Empty;
        
        // Actualizar la tabla.
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
}