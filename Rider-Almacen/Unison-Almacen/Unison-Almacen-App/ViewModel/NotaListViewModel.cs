using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Unison_Almacen_Core.Modelos;
using Unison_Almacen_Core.Repositorios;

namespace UnisonAlmacen.App.ViewModels
{
    public class NotaListViewModel : ObservableObject
    {
        private readonly NotaRepositorio _notaRepositorio;

        public ObservableCollection<Nota> Notas { get; } = new();

        public NotaListViewModel()
        {
            _notaRepositorio = new NotaRepositorio();
            CargarNotasCommand = new RelayCommand(CargarNotas);
        }

        public IRelayCommand CargarNotasCommand { get; }

        private void CargarNotas()
        {
            Notas.Clear();
            foreach (var nota in _notaRepositorio.ListarNotas())
            {
                Notas.Add(nota);
            }
        }
    }
}