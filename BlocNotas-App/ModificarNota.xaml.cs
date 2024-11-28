using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BlocNotas_Core.Models; // Importar los controles de WPF

namespace BlocNotas
{
    public partial class ModificarNota : Window
    {
        public Nota Nota { get; set; }

        public ModificarNota(Nota nota)
        {
            InitializeComponent();
            Nota = nota;
            DataContext = Nota;

            // Asegurarnos de que el ComboBox tenga el color correcto seleccionado
            if (Nota != null)
            {
                // Seleccionar el color en el ComboBox según el valor de la nota
                ColorComboBox.SelectedItem = ColorComboBox.Items
                    .Cast<ComboBoxItem>()
                    .FirstOrDefault(item => item.Content.ToString() == Nota.Color);
            }
        }

        // Guardar cambios (modificar nota)
        private void GuardarCambios_Click(object sender, RoutedEventArgs e)
        {
            // Validar que la nota y el color no sean nulos
            if (Nota != null)
            {
                // Verificar que el color haya sido seleccionado
                var colorSeleccionado = ColorComboBox.SelectedItem as ComboBoxItem;
                if (colorSeleccionado != null)
                {
                    // Asignar el color seleccionado a la propiedad Nota.Color
                    Nota.Color = colorSeleccionado.Content.ToString();
                }

                using (var context = new NotaContext())
                {
                    var notaAEditar = context.Notas.FirstOrDefault(n => n.Id == Nota.Id);
                    if (notaAEditar != null)
                    {
                        // Actualizar los datos de la nota en la base de datos
                        notaAEditar.Titulo = Nota.Titulo;
                        notaAEditar.Contenido = Nota.Contenido;
                        notaAEditar.Color = Nota.Color; // Actualizamos el color

                        context.SaveChanges(); // Guardar cambios en la base de datos
                    }
                }
            }

            // Cerrar la ventana después de guardar los cambios
            this.Close();
        }

        // Cancelar sin guardar cambios
        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            // Cerrar la ventana sin guardar
            this.Close();
        }
    }
}
