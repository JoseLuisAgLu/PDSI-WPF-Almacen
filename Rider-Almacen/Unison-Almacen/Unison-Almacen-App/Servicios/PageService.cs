using System.Windows;
using Wpf.Ui;

namespace Unison_Almacen_App.Servicios;

public class PageService : IPageService
{
    /// <summary>
    /// Servicio que provee las instancias de las páginas.
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Inicializa la instancia de la clase PageService.
    /// </summary>
    /// <param name="serviceProvider"></param>
    public PageService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    /// <inheritdoc />
    public T? GetPage<T>() where T : class
    {
        if (!typeof(FrameworkElement).IsAssignableFrom(typeof(T)))
        {
            throw new InvalidOperationException("La página debe ser un control de WPF.");
        }

        return (T?)_serviceProvider.GetService(typeof(T));
    }
    /// <inheritdoc />
    public FrameworkElement? GetPage(Type pageType)
    {
        if (!typeof(FrameworkElement).IsAssignableFrom(pageType))
        {
            throw new InvalidOperationException("La página debe ser un control de WPF.");
        }

        return _serviceProvider.GetService(pageType) as FrameworkElement;
    }
}