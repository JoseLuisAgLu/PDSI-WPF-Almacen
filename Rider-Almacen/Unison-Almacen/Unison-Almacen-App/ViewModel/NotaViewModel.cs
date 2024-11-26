using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Unison_Almacen_Core.Contratos.Servicios;
using Unison_Almacen_Core.Modelos;

namespace Unison_Almacen_App.ViewModel
{
    public partial class NotaViewModel : ObservableObject
    {
        [ObservableProperty] private Nota _nota = new Nota();
        [ObservableProperty] private List<Nota> _notas;

        [ObservableProperty] private string _txtBotonFormulario;
        private const string TXT_AGREGAR = "Agregar";
        private const string TXT_MODIFICAR = "Modificar";

        private IS2<Nota>? _servicio;

        // Constructor sin parámetros
        public NotaViewModel()
        {
            // Inicializar con datos vacíos
            _notas = new List<Nota>();
            _nota.Color = "Rojo";
            _txtBotonFormulario = TXT_AGREGAR;

            // Inicializar comandos
            AgregarNotaCommand = new RelayCommand(AgregarNota);
         
        }

        // Constructor con servicio
        public NotaViewModel(IS2<Nota> servicio) : this()
        {
            _servicio = servicio;
            // Cargar datos del servicio
            _notas = _servicio.Listar();
        }

        public ICommand AgregarNotaCommand { get; }

        private void AgregarNota()
        {
            try
            {
                var n = Nota;
                if (string.IsNullOrWhiteSpace(n.Titulo) || string.IsNullOrWhiteSpace(n.Contenido))
                {
                    MessageBox.Show("El título y el contenido son obligatorios.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Comprobar si la nota existe.
                var notaExistente = _servicio?.ObtenerPorId(n.Id) ?? new Nota();

                // Si la nota no existe, se agrega.
                if (notaExistente.Id == Guid.Empty)
                {
                    _servicio?.Agregar(n);
                }
                // Si la nota existe, se modifica.
                else
                {
                    _servicio?.Modificar(n);
                }

                // Borrar los datos del formulario.
                Nota.Id = Guid.Empty;
                Nota.Titulo = string.Empty;
                Nota.Contenido = string.Empty;
                Nota.Color = string.Empty;

                // Actualizar la tabla.
                Nota = new Nota { Color = "Rojo" };
                Notas = _servicio?.Listar() ?? new List<Nota>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar la nota: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        

        partial void OnNotaChanged(Nota? oldValue, Nota newValue)
        {
            TxtBotonFormulario = newValue.Id != Guid.Empty ? TXT_MODIFICAR : TXT_AGREGAR;
            Nota = newValue;
        }
    }
}
