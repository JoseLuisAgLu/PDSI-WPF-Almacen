using System.Windows.Controls;
using Unison_Almacen_App.ViewModel;

namespace Unison_Almacen_App.Views;

public partial class NotaGeneralView : Page
{
    public NotaGeneralView(NotaGeneralViewModel viewModel)
    {
        InitializeComponent();

        // Inicializar el DataContext
        DataContext = viewModel;
    }
}