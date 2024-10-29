using System.Collections.ObjectModel;
using System.Configuration.Provider;
using CommunityToolkit.Mvvm.ComponentModel;
using Unison_Almacen_App.Views;
using Wpf.Ui.Controls;

namespace Unison_Almacen_App.ViewModel;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<NavigationViewItem> _navigationItems = [];

    public MainWindowViewModel()
    {
        NavigationItems =
        [
            new NavigationViewItem()
            {
                Content = "Inicio",
                TargetPageType = typeof(InicioView)
            },
            new NavigationViewItem()
            {
                Content = "Producto",
                TargetPageType = typeof(ProductoView)
            },
            new NavigationViewItem()
            {
            Content = "Ver Productos",
            TargetPageType = typeof(ProductoListView)
            }
        ];
    }
}