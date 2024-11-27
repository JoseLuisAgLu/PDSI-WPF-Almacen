using System.Windows.Controls;
using System.Windows;
using Unison_Almacen_App.ViewModel;
using UnisonAlmacen.App.Views;

namespace Unison_Almacen_App.Views;

public partial class NotaInicioView : Window
{
    public NotaInicioView()
    {
        InitializeComponent();
    }
    private void OnIrALaListaDeNotasClick(object sender, RoutedEventArgs e)
    {
        // Navegar a la lista de notas
        var notaListView = new NotaListView();
        notaListView.Show();
        this.Close(); // Cerrar la ventana actual
    }
}