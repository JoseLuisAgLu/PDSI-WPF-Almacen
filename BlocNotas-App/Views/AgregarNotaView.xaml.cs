using System.Windows;
using System.Windows.Controls;
using BlocNotas.ViewModels;

namespace BlocNotas.Views;

public partial class AgregarNotaView : Window
{
    public AgregarNotaView()
    {
        InitializeComponent();
        
        DataContext = new AgregarNotaViewModel();
    }
}