using System.Windows;

namespace BlocNotas;

public partial class InicioView : Window
{
    public InicioView()
    {
        InitializeComponent();
    }

    private void EntrarButton_Click(object sender, RoutedEventArgs e)
    {
        // Abrir la ventana de listado de notas
        var listadoNotas = new ListadoNotas();
        listadoNotas.Show();
        this.Close(); // Cerrar la ventana de inicio
    }
}