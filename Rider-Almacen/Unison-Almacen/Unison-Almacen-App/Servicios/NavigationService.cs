using System;
using System.Collections.Generic;
using System.Windows;
using Wpf.Ui.Controls;

namespace Unison_Almacen_App.Servicios
{
    public static class NavigationService
    {
        private static NavigationView? _navigationView;
        private static readonly Dictionary<Type, Type> ViewModelToViewMapping = new();

        // Configurar el NavigationView principal
        public static void Configure(NavigationView navigationView)
        {
            _navigationView = navigationView ?? throw new ArgumentNullException(nameof(navigationView));
        }

        // Registrar mapeo entre ViewModel y View
        public static void Register<TViewModel, TView>()
            where TView : FrameworkElement
        {
            ViewModelToViewMapping[typeof(TViewModel)] = typeof(TView);
        }

        // Navegar a un ViewModel
        public static void Navigate<TViewModel>() where TViewModel : class
        {
            if (_navigationView == null)
                throw new InvalidOperationException("NavigationService no está configurado. Llama a Configure() antes de navegar.");

            if (!ViewModelToViewMapping.TryGetValue(typeof(TViewModel), out var viewType))
                throw new InvalidOperationException($"No se ha registrado una vista para {typeof(TViewModel).Name}.");

            _navigationView.Navigate(viewType);
        }
    }
}