using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BlocNotas_Core.Models;
using System.Windows;

namespace BlocNotas.ViewModels
{
    public partial class AgregarNotaViewModel : ObservableObject
    {
        [ObservableProperty]
        private string titulo;

        [ObservableProperty]
        private string contenido;

        [ObservableProperty]
        private string color;

        public Nota NuevaNota { get; private set; }

        [RelayCommand]
        public void Aceptar(Window currentWindow)
        {
            if (!string.IsNullOrWhiteSpace(Color))
            {
                // Crear la nueva nota
                NuevaNota = new Nota
                {
                    Titulo = Titulo,
                    Contenido = Contenido,
                    Color = Color
                };

                // Cerrar la ventana después de agregar la nota
                MessageBox.Show("Nota agregada con éxito");
                currentWindow.Close();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un color.");
            }
        }

        [RelayCommand]
        public void Cancelar(Window currentWindow)
        {
            // Cerrar la ventana sin guardar
            currentWindow.Close();
        }
    }
}