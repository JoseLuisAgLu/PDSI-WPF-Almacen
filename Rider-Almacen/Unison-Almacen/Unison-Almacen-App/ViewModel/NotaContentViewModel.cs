using CommunityToolkit.Mvvm.ComponentModel;
using Unison_Almacen_Core.Modelos;

namespace Unison_Almacen_App.ViewModel;

public partial class NotaContentViewModel : ObservableObject
{
    [ObservableProperty] private Nota _nota;

    public NotaContentViewModel(Nota nota)
    {
        Nota = nota;
    }
}