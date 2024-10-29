using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Unison_Almacen_Core.Modelos;
using Unison_Almacen_Core.BaseDeDatos;
using Unison_Almacen_Core.Repositorios;
using System.Windows;
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
        // Validar que los campos 
        if (string.IsNullOrWhiteSpace(Producto.Nombre) ||
            string.IsNullOrWhiteSpace(Producto.Descripcion) ||
            Producto.Precio <= 0)
        {
            MessageBox.Show("Hay campos vacios, por favor, completa todos los campos.", 
                "Campos Vacíos", 
                MessageBoxButton.OK, 
                MessageBoxImage.Warning);
            return;
        }

        // Agregar el producto a la base de datos
        _productoRepositorio.Agregar(Producto);

        // Limpiar los campos
        Producto = new Producto();
    }
}