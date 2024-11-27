using System.Windows.Controls;
using System.Windows;
using Unison_Almacen_App.ViewModel;
using Unison_Almacen_Core.Repositorios;
using Unison_Almacen_Core.Servicios;

namespace Unison_Almacen_App.Views;

public partial class NotaView : Page
{
    public NotaView(NotaViewModel viewModel)
    {
        InitializeComponent();
   
        DataContext = viewModel;
    }
    private void CancelarNotaButton_Click(object sender, RoutedEventArgs e)
    {
        var viewModel = new NotaListViewModel();
        var nuevaNotaView = new NotaListView(viewModel);
        NavigationService.Navigate(nuevaNotaView);
    }


}