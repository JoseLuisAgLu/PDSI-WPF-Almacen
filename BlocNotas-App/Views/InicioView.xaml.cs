using System.Windows.Controls;
using System.Windows;
namespace BlocNotas.Views
{

    public partial class InicioView : Window
    {
        public InicioView()
        {
            InitializeComponent();
        }

        private void EntrarButton_Click(object sender, RoutedEventArgs e)
        {
            // Abrir la ventana de listado de notas
            var listadoNotas = new ListadoNotasView();
            listadoNotas.Show();
            this.Close(); // Cerrar la ventana de inicio
        }
    }
}