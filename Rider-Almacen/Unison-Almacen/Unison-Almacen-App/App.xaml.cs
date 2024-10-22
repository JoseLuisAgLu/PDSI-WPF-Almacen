using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Unison_Almacen_Core.BaseDeDatos;
using Unison_Almacen_Core.Contratos.Repositorios;
using Unison_Almacen_Core.Contratos.Servicios;
using Unison_Almacen_Core.Modelos;
using Unison_Almacen_Core.Repositorios;
using Unison_Almacen_Core.Servicios;

namespace Unison_Almacen_App;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public IHost Host { get; }

    public static T GetService<T>() where T : class
    {
        if ((App.Current as App)?.Host.Services.GetService(typeof(T)) is not T service)
        {
            throw new ArgumentException($"El servicio {typeof(T).Name} encontrado. Debe ser configurado en ConfigureServices dentro del App.xaml.cs.");
        }
        
        return service;
    }
    
    public App()
    {
        InitializeComponent();
        
        // Asegurar que la base de datos existe.
        using var bd = new ProductoBD();
        bd.Database.EnsureCreated();
        
        // Inicialización del host.
        Host = Microsoft.Extensions.Hosting.Host
            .CreateDefaultBuilder()
            .UseContentRoot(AppContext.BaseDirectory)
            .ConfigureServices((context, services) =>
            {
                // Servicios.
                services.AddTransient<ProductoBD>();
                services.AddTransient<IServicio<Producto>, ProductoServicio>();
                services.AddTransient<IRepositorio<Producto>, ProductoRepositorio>();

            })
            .Build();
    }
}