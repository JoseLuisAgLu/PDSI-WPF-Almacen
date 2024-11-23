using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Unison_Almacen_Core.Modelos;
using Unison_Almacen_Core.Contratos.Servicios;
using Unison_Almacen_App.Servicios;
using Unison_Almacen_App.Views;

namespace Unison_Almacen_App.ViewModel;

public partial class NotaListViewModel : ObservableObject
{
    private readonly IS2<Nota> _notaServicio;

    [ObservableProperty] private ObservableCollection<Nota> _notas;

    [ObservableProperty] private Nota? _notaSeleccionada;

    public NotaListViewModel(IS2<Nota> notaServicio)
    {
        _notaServicio = notaServicio;
        Notas = new ObservableCollection<Nota>(_notaServicio.Listar());
    }

    [RelayCommand]
    private void AgregarNota()
    {
        var nuevaNota = new Nota { Id = Guid.NewGuid(), Titulo = "Nueva Nota", Contenido = "" };
        _notaServicio.Agregar(nuevaNota);
        Notas.Add(nuevaNota);
    }

    [RelayCommand]
    private void EliminarNota(Nota? nota)
    {
        if (nota == null) return;

        _notaServicio.Eliminar(nota);
        Notas.Remove(nota);
    }

    [RelayCommand]
    private void SeleccionarNota(Nota nota)
    {
        if (nota == null) return;

        var contentViewModel = new NotaContentViewModel(nota);

        // Navegar a la vista asociada a NotaContentViewModel
        NavigationService.Navigate<NotaContentViewModel>();
    }
}