using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Unison_Almacen_Core.Contratos.Servicios;
using Unison_Almacen_Core.Modelos;
using Unison_Almacen_Core.Servicios;

namespace Unison_Almacen_App.ViewModel;

public partial class ProductoViewModel : ObservableObject
{
    [ObservableProperty] private Producto _producto = new Producto();
    [ObservableProperty] private List<Producto> _productos;

    [ObservableProperty] private string _txtBotonFormulario;
    private const string TXT_AGREGAR = "Agregar";
    private const string TXT_MODIFICAR = "Modificar";
    
    private IServicio<Producto> _servicio;

    public ProductoViewModel(IServicio<Producto> servicio)
    {
        // Definir el comando.
        AgregarProductoCommand = new RelayCommand(AgregarProducto);
        EliminarProductoCommand = new RelayCommand<Producto>(EliminarProducto);
        
        // Guardar la referencia del servicio.
        _servicio = servicio;
        
        // Obtener los productos de la base de datos.
        _productos = _servicio.Listar();
        
        // Texto del formulario.
        _txtBotonFormulario = TXT_AGREGAR;
    }
    
    public ICommand AgregarProductoCommand { get; }
    public ICommand EliminarProductoCommand { get; }

    private void AgregarProducto()
    {
        var p = Producto;
        if (string.IsNullOrWhiteSpace(p.Nombre) || string.IsNullOrWhiteSpace(p.Descripcion)) return;
        
        // Comprobar si el elemento existe.
        var producto = _servicio.ObtenerPorId(p.Id);
        
        // Si el producto no existe se agrega
        if (producto.Id == Guid.Empty)
        {
            // Agregar el producto.
            _servicio.Agregar(p);
        }
        // Si el producto existe se modifica.
        else
        {
            // Modificar el producto.
            _servicio.Modificar(p);
        }
            
        // Borramos los datos del formulario.
        Producto.Id = Guid.Empty;
        Producto.Nombre = string.Empty;
        Producto.Descripcion = string.Empty;
        
        // Actualizar la tabla.
        Productos = _servicio.Listar();
    }
    
    private void EliminarProducto(Producto? producto)
    {
        if (producto == null) return;
        
        _servicio.Eliminar(producto);
        Productos = _servicio.Listar();
    }

    partial void OnProductoChanged(Producto? oldValue, Producto newValue)
    {
        TxtBotonFormulario = newValue.Id != Guid.Empty ? TXT_MODIFICAR : TXT_AGREGAR;
        Producto = newValue;
    }
}