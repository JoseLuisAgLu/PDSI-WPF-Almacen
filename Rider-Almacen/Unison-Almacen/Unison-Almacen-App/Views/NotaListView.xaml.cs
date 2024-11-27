using System.Windows.Controls;
using System.Windows;
using Unison_Almacen_App.ViewModel;

namespace Unison_Almacen_App.Views;

public partial class NotaListView : Page
{
    public NotaListView(NotaListViewModel viewModel)
    {
        InitializeComponent();
        // Inicializar el DataContext.
        DataContext = viewModel;
    }
    private void AgregarNotaButton_Click(object sender, RoutedEventArgs e)
    {
        var listViewModel = new NotaViewModel(); // Usar datos vacíos o predeterminados
        var listView = new NotaView(listViewModel);
        NavigationService.Navigate(listView);
    }
}