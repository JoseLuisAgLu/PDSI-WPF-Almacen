using System.Windows.Controls;
using System.Windows;
using Unison_Almacen_App.ViewModel;
using Unison_Almacen_Core.Repositorios;
using Unison_Almacen_Core.Servicios;

namespace Unison_Almacen_App.Views;

public partial class NotaView : Page
{
    public NotaView()
    {
        InitializeComponent();
        // Inicializar el servicio y el ViewModel
        var repositorio = new NotaRepositorio();
        var servicio = new NotaServicio(repositorio);
        var viewModel = new NotaViewModel(servicio);
        DataContext = viewModel;
    }
    private void CancelarNotaButton_Click(object sender, RoutedEventArgs e)
    {
        var viewModel = new NotaListViewModel();
        var nuevaNotaView = new NotaListView(viewModel);
        NavigationService.Navigate(nuevaNotaView);
    }


}