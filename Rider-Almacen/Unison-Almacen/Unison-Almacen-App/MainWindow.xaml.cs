using Unison_Almacen_App.ViewModel;
using Unison_Almacen_App.Views;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace Unison_Almacen_App;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : FluentWindow
{
    public MainWindow(MainWindowViewModel viewModel)
    {
        InitializeComponent();

        // Inicializamos el contexto de datos.
        DataContext = viewModel;

        // Inicializa el tema de la aplicación.
        ApplicationThemeManager.Apply(this);

        // Establece la página de inicio.
        Loaded += (_, _) => RootNavigation.Navigate(typeof(ProductoView));
    }
}