using System.Windows;
using BlocNotas.ViewModels;

namespace BlocNotas.Views
{
    public partial class ListadoNotasView : Window
    {
        public ListadoNotasView()
        {
            InitializeComponent();
            DataContext = new ListadoNotaViewModel(); // Conectar el ViewModel
        }
    }
}