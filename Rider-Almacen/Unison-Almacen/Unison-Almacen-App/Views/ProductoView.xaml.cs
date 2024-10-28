using System.Windows.Controls;
using Unison_Almacen_App.ViewModel;

namespace Unison_Almacen_App.Views;

public partial class ProductoView : Page
{
    public ProductoView(ProductoViewModel viewModel)
    {
        InitializeComponent();
        
        DataContext = viewModel;
    }
}