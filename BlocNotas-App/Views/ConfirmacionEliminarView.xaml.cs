using System.Windows;
using System.Windows.Controls;

namespace BlocNotas.Views;

public partial class ConfirmacionEliminarView : Window
{
    public bool EsConfirmado { get; private set; }

    public ConfirmacionEliminarView()
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