using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Unison_Almacen_App.Servicios;
using Unison_Almacen_App.ViewModel;
using Unison_Almacen_App.Views;
using Unison_Almacen_Core.BaseDeDatos;
using Unison_Almacen_Core.Contratos.Repositorios;
using Unison_Almacen_Core.Contratos.Servicios;
using Unison_Almacen_Core.Modelos;
using Unison_Almacen_Core.Repositorios;
using Unison_Almacen_Core.Servicios;
using Wpf.Ui;

namespace Unison_Almacen_App;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public sealed partial class App : Application
{
    public App()
    {
        InitializeComponent();
        
        // Inicializar los servicios.
        Services = ConfigServices();
        
        // Asegurar que la base de datos existe.
        using var bd = new ProductoBD();
        bd.Database.EnsureCreated();
    }
    
    /// <summary>
    /// Obtiene la referencia de la aplicación actual.
    /// </summary>
    public new static App Current => (App)Application.Current;

    /// <summary>
    /// Obtiene el proveedor para resolver los servicios de la aplicación.
    /// </summary>
    public IServiceProvider Services { get; }
    
    /// <summary>
    /// Configura los servicios de la aplicación.
    /// </summary>
    /// <returns></returns>
    private static IServiceProvider ConfigServices()
    {
        var services = new ServiceCollection();
        
        // Servicios.
        services.AddTransient<ProductoBD>();
        services.AddTransient<IServicio<Producto>, ProductoServicio>();
        services.AddTransient<IRepositorio<Producto>, ProductoRepositorio>();
        services.AddSingleton<IPageService, PageService>();

        // MainWindow.
        services.AddSingleton<MainWindow>();
                
        // Views
        services.AddTransient<InicioView>();
        services.AddTransient<ProductoView>();
        
        // ViewModels.
        services.AddTransient<MainWindowViewModel>();
        services.AddTransient<InicioViewModel>();
        
        return services.BuildServiceProvider();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        var mainWindow = Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}