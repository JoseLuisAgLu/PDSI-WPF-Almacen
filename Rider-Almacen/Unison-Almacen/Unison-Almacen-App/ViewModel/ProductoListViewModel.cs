using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Unison_Almacen_Core.Modelos;
using Unison_Almacen_Core.Repositorios;

namespace Unison_Almacen_App.ViewModel;

public partial class ProductoListViewModel : ObservableObject
{
    private readonly ProductoRepositorio _productoRepositorio;

    [ObservableProperty]
    private ObservableCollection<Producto> productos;

    public ProductoListViewModel()
    {
        _productoRepositorio = new ProductoRepositorio();
        CargarProductos();
    }

    private void CargarProductos()
    {
        // Saca productos desde la base de datos usando el repositorio
        var listaProductos = _productoRepositorio.Listar();
        Productos = new ObservableCollection<Producto>(listaProductos);
    }
}