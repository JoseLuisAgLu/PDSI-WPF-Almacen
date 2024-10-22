using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Unison_Almacen_Core.Contratos.Repositorios;
using Unison_Almacen_Core.Contratos.Servicios;
using Unison_Almacen_Core.Modelos;
using Unison_Almacen_Core.Servicios;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace Unison_Almacen_App;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : FluentWindow
{
    
    public MainWindow()
    {
        //InitializeComponent();
        ApplicationThemeManager.Apply(this);
    }
}