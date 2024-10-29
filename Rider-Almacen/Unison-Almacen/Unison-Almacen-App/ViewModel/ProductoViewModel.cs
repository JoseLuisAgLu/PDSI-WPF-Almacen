using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Unison_Almacen_Core.Modelos;
using Unison_Almacen_Core.BaseDeDatos;
using Unison_Almacen_Core.Repositorios;
namespace Unison_Almacen_App.ViewModel;

public partial class ProductoViewModel : ObservableObject
{
    [ObservableProperty]
    private Producto producto = new Producto();

    private readonly ProductoRepositorio _productoRepositorio;

    public ProductoViewModel()
    {
        _productoRepositorio = new ProductoRepositorio();
        AgregarProductoCommand = new RelayCommand(AgregarProducto);
    }

    public ICommand AgregarProductoCommand { get; }

    private void AgregarProducto()
    {
        // Anadir productines a la base de datos a
        _productoRepositorio.Agregar(Producto);

        // Limpiar los campos y tal 
        Producto = new Producto();
    }
}