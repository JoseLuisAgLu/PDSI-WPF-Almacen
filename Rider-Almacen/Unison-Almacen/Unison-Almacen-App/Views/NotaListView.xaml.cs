using System.Windows;
using UnisonAlmacen.App.ViewModels;

namespace UnisonAlmacen.App.Views
{
    public partial class NotaListView : Window
    {
        public NotaListView()
        {
            InitializeComponent();
            DataContext = new NotaListViewModel();
        }
    }
}