using System.Windows;

namespace BlocNotas
{
    public partial class ConfirmacionEliminar : Window
    {
        public bool EsConfirmado { get; private set; }

        public ConfirmacionEliminar()
        {
            InitializeComponent();
            EsConfirmado = false;
        }

        // Cuando el usuario confirma la eliminación
        private void ConfirmarButton_Click(object sender, RoutedEventArgs e)
        {
            EsConfirmado = true;
            Close();
        }

        // Cuando el usuario cancela la eliminación
        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            EsConfirmado = false;
            Close();
        }
    }
}