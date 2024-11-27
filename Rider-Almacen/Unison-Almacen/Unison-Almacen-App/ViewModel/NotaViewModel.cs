using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Windows;
using Unison_Almacen_Core.Modelos;

namespace UnisonAlmacen.App.ViewModels
{
    public class NotaViewModel : ObservableObject
    {
        private string _titulo;
        private string _contenido;

        public string Titulo
        {
            get => _titulo;
            set => SetProperty(ref _titulo, value);
        }

        public string Contenido
        {
            get => _contenido;
            set => SetProperty(ref _contenido, value);
        }

        public IRelayCommand GuardarNotaCommand { get; }

        public NotaViewModel()
        {
            GuardarNotaCommand = new RelayCommand(GuardarNota);
        }

        private void GuardarNota()
        {
            // Validación básica de los campos
            if (string.IsNullOrWhiteSpace(Titulo) || string.IsNullOrWhiteSpace(Contenido))
            {
                System.Windows.MessageBox.Show("El título y contenido no pueden estar vacíos.", 
                    "Error", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error);
                return;
            }

            // Crear y guardar la nueva nota
            var nuevaNota = new Nota
            {
                Id = Guid.NewGuid(),
                Titulo = Titulo,
                Contenido = Contenido,
                FechaCreacion = DateTime.Now
            };

            // Aquí deberías integrar el repositorio para guardar la nota
            // Por ejemplo:
            // _notaRepositorio.AgregarNota(nuevaNota);

            // Mostrar mensaje de éxito
            System.Windows.MessageBox.Show("Nota guardada con éxito.", 
                "Éxito", 
                MessageBoxButton.OK, 
                MessageBoxImage.Information);

            // Limpia los campos (opcional)
            Titulo = string.Empty;
            Contenido = string.Empty;
        }
    }
}


