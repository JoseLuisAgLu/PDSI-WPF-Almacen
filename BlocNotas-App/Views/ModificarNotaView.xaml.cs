using System.Windows;
using System.Windows.Controls;

using BlocNotas_Core.Models;
using BlocNotas.ViewModels;

namespace BlocNotas.Views;

public partial class ModificarNotaView : Window
{
    public ModificarNotaView(Nota nota)
    {
        InitializeComponent();
        DataContext = new ModificarNotaViewModel(nota);
    }
}