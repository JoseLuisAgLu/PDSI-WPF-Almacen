using System.Windows.Controls;
using System.Windows;
namespace Unison_Almacen_App.Views;

public partial class NotaInicioView : Page
{
    public NotaInicioView()
    {
        InitializeComponent();
    }
    private void AgregarNotaButton_Click(object sender, RoutedEventArgs e)
    {
        // Crear la instancia de la página NotaView
        var nuevaNotaView = new NotaListView();

        // Navegar hacia la nueva vista
        NavigationService.Navigate(nuevaNotaView);
    }
}