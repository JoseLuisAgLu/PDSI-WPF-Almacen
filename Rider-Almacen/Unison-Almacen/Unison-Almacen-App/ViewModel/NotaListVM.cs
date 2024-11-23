using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Unison_Almacen_Core.Modelos;
using Unison_Almacen_Core.Repositorios;

namespace Unison_Almacen_App.ViewModel;

public partial class NotaListVM : ObservableObject
{
    private readonly NotaRepositorio _notaRepositorio;

    [ObservableProperty]
    private ObservableCollection<Nota> notas;

    public NotaListVM()
    {
        _notaRepositorio = new NotaRepositorio();
        CargarNotas();
    }

    private void CargarNotas()
    {
        // Saca productos desde la base de datos usando el repositorio
        var listaNotas = _notaRepositorio.Listar();
        Notas = new ObservableCollection<Nota>(listaNotas);
    }
}