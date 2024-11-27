using System.Windows;
using Unison_Almacen_App.ViewModel;
using UnisonAlmacen.App.ViewModels;

namespace UnisonAlmacen.App.Views
{
    public partial class NotaView : Window
    {
        public NotaView()
        {
            InitializeComponent();
            // Asignar el ViewModel correspondiente
            DataContext = new NotaViewModel();
        }
    }
}