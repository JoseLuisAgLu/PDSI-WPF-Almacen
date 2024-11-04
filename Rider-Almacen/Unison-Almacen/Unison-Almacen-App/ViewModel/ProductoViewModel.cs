using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Unison_Almacen_Core.Contratos.Servicios;
using Unison_Almacen_Core.Modelos;
using Unison_Almacen_Core.Servicios;

namespace Unison_Almacen_App.ViewModel;

public partial class ProductoViewModel : ObservableObject
{
    [ObservableProperty] private Producto _producto = new Producto();
    [ObservableProperty] private List<Producto> _productos;
    
    private IServicio<Producto> _servicio;

    public ProductoViewModel(IServicio<Producto> servicio)
    {
        // Definir el comando.
        AgregarProductoCommand = new RelayCommand(AgregarProducto);
        
        // Guardar la referencia del servicio.
        _servicio = servicio;
        
        // Obtener los productos de la base de datos.
        _productos = _servicio.Listar();
    }
    
    public ICommand AgregarProductoCommand { get; }

    private void AgregarProducto()
    {
        var p = Producto;
        if (string.IsNullOrWhiteSpace(p.Nombre) || string.IsNullOrWhiteSpace(p.Descripcion)) return;
        
        // Agregar el producto.
        _servicio.Agregar(p);
        
        // Actualizamos la lista de productos.
        Productos = _servicio.Listar();
            
        // Borramos los datos del formulario.
        Producto.Nombre = string.Empty;
        Producto.Descripcion = string.Empty;
        
        // Actualizar la tabla.
    }
}