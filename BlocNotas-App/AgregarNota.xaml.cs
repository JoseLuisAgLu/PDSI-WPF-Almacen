using System.Windows;
using System.Windows.Controls;
using BlocNotas_Core.Models; // Necesario para ComboBoxItem
namespace BlocNotas
{
    public partial class AgregarNota : Window
    {
        public Nota NuevaNota { get; private set; }

        public AgregarNota()
        {
            InitializeComponent();
        }

        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            // Asegúrate de que un color esté seleccionado
            if (ColorComboBox.SelectedItem != null)
            {
                // Obtener el contenido del ComboBox (el color seleccionado)
                var selectedItem = ColorComboBox.SelectedItem as ComboBoxItem;
                string selectedColor = selectedItem?.Content.ToString();

                // Crear una nueva nota con los valores del formulario
                NuevaNota = new Nota
                {
                    Titulo = TituloTextBox.Text,
                    Contenido = ContenidoTextBox.Text,
                    Color = (ColorComboBox.SelectedItem as ComboBoxItem)?.Content.ToString()
                };

                // Mostrar mensaje de éxito
                MessageBox.Show("Nota agregada con éxito");

                // Cerrar la ventana de formulario
                this.Close();
            }
            else
            {
                // Si no se ha seleccionado un color, puedes manejar el caso (opcional)
                MessageBox.Show("Por favor, seleccione un color.");


            }
           
        }
        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            // Cerrar la ventana sin guardar
            this.Close();
        }
    }
}