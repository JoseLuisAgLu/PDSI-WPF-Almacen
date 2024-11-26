using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Unison_Almacen_Core.Modelos;
using Unison_Almacen_Core.Contratos.Servicios;
using Unison_Almacen_App.Servicios;
using Unison_Almacen_App.Views;
using System.Windows.Input;
namespace Unison_Almacen_App.ViewModel
{
    public partial class NotaListViewModel : ObservableObject
    {
        [ObservableProperty] private Nota _nota = new Nota();
        [ObservableProperty] private List<Nota> _notas;
        [ObservableProperty] private string _txtBotonFormulario;
    
        private const string TXT_AGREGAR = "Agregar";
        private const string TXT_MODIFICAR = "Modificar";
        private IS2<Nota>? _servicio;

        // Constructor sin parámetros
        public NotaListViewModel()
        {
            _notas = new List<Nota>
            {
                new Nota { Id = Guid.NewGuid(), Titulo = "Perdi mi gato", Contenido = "Na mentira, es que no sabia que poner", Color="Blue" },
                new Nota { Id = Guid.NewGuid(), Titulo = "Spoiler del Elden ring", Contenido = "Radagorn es Marika", Color="Red" }
            };
            _txtBotonFormulario = TXT_AGREGAR;
        }

        // Constructor con servicio
        public NotaListViewModel(IS2<Nota> servicio)
        {
            _servicio = servicio;
            _notas = _servicio.Listar();
            _txtBotonFormulario = TXT_AGREGAR;
        }

        public ICommand EliminarNotaCommand { get; }

        private void EliminarNota(Nota? nota)
        {
            if (nota == null) return;
            _servicio?.Eliminar(nota);
            Notas = _servicio?.Listar();
        }

        partial void OnNotaChanged(Nota? oldValue, Nota newValue)
        {
            TxtBotonFormulario = newValue.Id != Guid.Empty ? TXT_MODIFICAR : TXT_AGREGAR;
            Nota = newValue;
        }
    }

}