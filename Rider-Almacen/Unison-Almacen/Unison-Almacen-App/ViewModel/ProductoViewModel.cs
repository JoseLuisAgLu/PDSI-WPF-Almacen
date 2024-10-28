using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Unison_Almacen_Core.Modelos;

namespace Unison_Almacen_App.ViewModel;

public partial class ProductoViewModel : ObservableObject
{
    [ObservableProperty] private Producto producto = new Producto();

    public ProductoViewModel()
    {
        AgregarProductoCommand = new RelayCommand(AgregarProducto);
    }
    
    public ICommand AgregarProductoCommand { get; }

    private void AgregarProducto()
    {
        Producto p = new Producto();
    }
}