using System.Windows.Controls;
using System.Windows;
using Unison_Almacen_App.ViewModel;

namespace Unison_Almacen_App.Views;

public partial class NotaInicioView : Page
{
    public NotaInicioView()
    {
        InitializeComponent();
    }
    private void AgregarNotaButton_Click(object sender, RoutedEventArgs e)
    {
        var listViewModel = new NotaListViewModel(); // Usar datos vacíos o predeterminados
        var listView = new NotaListView(listViewModel);
        NavigationService.Navigate(listView);
    }
}