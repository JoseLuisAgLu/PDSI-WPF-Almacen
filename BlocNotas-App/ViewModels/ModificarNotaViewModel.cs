using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BlocNotas_Core.Models;
using System.Linq;
using System.Windows;

namespace BlocNotas.ViewModels
{
    public partial class ModificarNotaViewModel : ObservableObject
    {
        [ObservableProperty]
        private Nota nota;

        public ModificarNotaViewModel(Nota nota)
        {
            Nota = nota;
        }

        [RelayCommand]
        public void GuardarCambios()
        {
            if (Nota != null)
            {
                using (var context = new NotaContext())
                {
                    var notaAEditar = context.Notas.FirstOrDefault(n => n.Id == Nota.Id);
                    if (notaAEditar != null)
                    {
                        // Actualizar los datos de la nota
                        notaAEditar.Titulo = Nota.Titulo;
                        notaAEditar.Contenido = Nota.Contenido;
                        notaAEditar.Color = Nota.Color;

                        context.SaveChanges();
                    }
                }
            }
        }

        [RelayCommand]
        public void Cancelar(Window currentWindow)
        {
            currentWindow.Close();
        }
    }
}