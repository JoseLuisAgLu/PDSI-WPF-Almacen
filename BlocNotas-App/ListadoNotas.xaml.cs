using System.Collections.Generic;
using System.Windows;
using System.Collections.ObjectModel;
using BlocNotas;
using System.Data.SQLite;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Text.Json;
using BlocNotas_Core.Models;
using Microsoft.Win32;
using ClosedXML.Excel; 


namespace BlocNotas
{
    public partial class ListadoNotas : Window
    {
        // Lista de notas
        public ObservableCollection<Nota> Notas { get; set; }
        public ListadoNotas()
        {
            InitializeComponent();

            // Inicializar la lista de notas desde la base de datos
            using (var context = new NotaContext())
            {
                Notas = new ObservableCollection<Nota>(context.Notas.ToList());
            }
            // Asignar la colección a la lista en la interfaz
            NotasListBox.ItemsSource = Notas;
        }
        // Método para actualizar la nota en el listado
        private void AgregarNota_Click(object sender, RoutedEventArgs e)
        {
            // Crear una instancia de la ventana AgregarNota
            AgregarNota ventanaAgregarNota = new AgregarNota();

            // Mostrar la ventana de formulario como un cuadro de diálogo modal
            ventanaAgregarNota.ShowDialog();

            // Si se agregó una nueva nota, actualizar la lista y la base de datos
            if (ventanaAgregarNota.NuevaNota != null)
            {
                // Agregar la nueva nota a la lista
                Notas.Add(ventanaAgregarNota.NuevaNota);

                // Guardar la nueva nota en la base de datos
                using (var context = new NotaContext())
                {
                    context.Notas.Add(ventanaAgregarNota.NuevaNota);
                    context.SaveChanges();  // Guardar cambios en la base de datos
                }
            }
        }
        // Evento de clic para eliminar una nota
        private void EliminarNota_Click(object sender, RoutedEventArgs e)
        {
            // Obtener la nota seleccionada en el ListBox
            var nota = ((FrameworkElement)sender).DataContext as Nota;
            if (nota == null) return;

            // Mostrar ventana de confirmación
            var ventanaConfirmacion = new ConfirmacionEliminar();
            ventanaConfirmacion.ShowDialog();

            // Si el usuario confirma la eliminación
            if (ventanaConfirmacion.EsConfirmado)
            {
                // Eliminar la nota de la base de datos
                using (var context = new NotaContext())
                {
                    var notaAEliminar = context.Notas.FirstOrDefault(n => n.Id == nota.Id);
                    if (notaAEliminar != null)
                    {
                        context.Notas.Remove(notaAEliminar);
                        context.SaveChanges(); // Guardar cambios en la base de datos
                    }
                }
                // Eliminar la nota de la lista en la interfaz
                Notas.Remove(nota);
            }
        }
        private void ModificarNota_Click(object sender, RoutedEventArgs e)
        {
            // Obtener la nota seleccionada en el ListBox
            var nota = ((FrameworkElement)sender).DataContext as Nota;
            if (nota == null) return;
            // Crear una nueva ventana de modificación y pasarle la nota a modificar
            var ventanaModificar = new ModificarNota(nota); // Enviar la nota al constructor de la ventana de modificación
            if (ventanaModificar.ShowDialog() == true)
            {
                // Actualizar la nota en la base de datos
                using (var context = new NotaContext())
                {
                    var notaAActualizar = context.Notas.FirstOrDefault(n => n.Id == nota.Id);
                    if (notaAActualizar != null)
                    {
                        // Actualizamos los campos de la nota
                        notaAActualizar.Titulo = nota.Titulo;
                        notaAActualizar.Contenido = nota.Contenido;
                        notaAActualizar.Color = nota.Color;
                        context.SaveChanges(); // Guardamos los cambios en la base de datos
                    }
                }
                // Actualizar la nota en la lista de la interfaz
                var indiceNota = Notas.IndexOf(nota);
                if (indiceNota >= 0)
                {
                    Notas[indiceNota] = nota;
                }
            }
        }
        private void ActualizarPagina_Click(object sender, RoutedEventArgs e)
        {
            // Actualizar la lista de notas desde la base de datos
            using (var context = new NotaContext())
            {
                Notas.Clear();  // Limpiar la lista actual
                foreach (var nota in context.Notas.ToList())  // Recargar las notas desde la base de datos
                {
                    Notas.Add(nota);  // Agregar las notas a la lista
                }
            }
        }
        private void RegresarAlInicio_Click(object sender, RoutedEventArgs e)
        {
            // Crear e iniciar la ventana de inicio
            InicioView ventanaInicio = new InicioView();
            ventanaInicio.Show();
            this.Close();
        }
       private void ExportarNotas_Click(object sender, RoutedEventArgs e)
{
    // Abrir el explorador de archivos para que el usuario elija la ubicación y nombre del archivo
    SaveFileDialog saveFileDialog = new SaveFileDialog();
    saveFileDialog.Filter = "Notas de Unison (*.notasunison)|*.notasunison";  // Filtrar por la extensión .notasunison
    saveFileDialog.DefaultExt = ".notasunison";  // Definir la extensión por defecto

    if (saveFileDialog.ShowDialog() == true)
    {
        // Obtener la ubicación y el nombre del archivo seleccionado
        string filePath = saveFileDialog.FileName;

        // Crear un objeto que contenga las notas a exportar
        var notasData = Notas.Select(nota => new
        {
            Titulo = nota.Titulo,
            Contenido = nota.Contenido,
            Color = nota.Color
        }).ToList();

        // 1. Generar el archivo .notasunison (JSON)
        string jsonNotas = JsonSerializer.Serialize(notasData);
        string notasunisonFilePath = Path.ChangeExtension(filePath, ".notasunison");
        File.WriteAllText(notasunisonFilePath, jsonNotas);

        // 2. Generar el archivo .xlsx (Excel)
        var workbook = new XLWorkbook();
        var worksheet = workbook.AddWorksheet("Notas");

        // Encabezados de columnas en el Excel
        worksheet.Cell(1, 1).Value = "Título";
        worksheet.Cell(1, 2).Value = "Contenido";
        worksheet.Cell(1, 3).Value = "Color";

        // Rellenar las celdas con los datos de las notas
        for (int i = 0; i < notasData.Count; i++)
        {
            worksheet.Cell(i + 2, 1).Value = notasData[i].Titulo;
            worksheet.Cell(i + 2, 2).Value = notasData[i].Contenido;
            worksheet.Cell(i + 2, 3).Value = notasData[i].Color;
        }

        // 2.1 Guardar el archivo Excel (.xlsx)
        string excelFilePath = Path.ChangeExtension(filePath, ".xlsx");
        workbook.SaveAs(excelFilePath);

        // Notificar al usuario que las notas fueron exportadas correctamente
        MessageBox.Show($"Notas exportadas correctamente:\n\nArchivo .notasunison: {notasunisonFilePath}\nArchivo .xlsx: {excelFilePath}", "Exportación Completa", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}

private void ExportarNotasASimpleArchivo(string filePath)
{
    // Crear un archivo de texto plano con la información de las notas
    using (StreamWriter writer = new StreamWriter(filePath))
    {
        foreach (var nota in Notas)
        {
            // Escribir el título y contenido de cada nota en el archivo
            writer.WriteLine($"Título: {nota.Titulo}");
            writer.WriteLine($"Contenido: {nota.Contenido}");
            writer.WriteLine($"Color: {nota.Color}");
            writer.WriteLine("--------------------------------");
        }
    }

    MessageBox.Show("Notas exportadas con éxito en formato .notasunison", "Exportación exitosa", MessageBoxButton.OK, MessageBoxImage.Information);
}
private void ExportarNotasAExcel(string filePath)
{
    // Crear un archivo de Excel utilizando ClosedXML
    using (var workbook = new XLWorkbook())
    {
        var worksheet = workbook.AddWorksheet("Notas");

        // Definir encabezados de columna
        worksheet.Cell(1, 1).Value = "Título";
        worksheet.Cell(1, 2).Value = "Contenido";
        worksheet.Cell(1, 3).Value = "Color";

        // Escribir las notas en el archivo Excel
        int row = 2;
        foreach (var nota in Notas)
        {
            worksheet.Cell(row, 1).Value = nota.Titulo;
            worksheet.Cell(row, 2).Value = nota.Contenido;
            worksheet.Cell(row, 3).Value = nota.Color;
            row++;
        }
        // Guardar el archivo en la ubicación seleccionada
        workbook.SaveAs(filePath);
    }
    MessageBox.Show("Notas exportadas con éxito en formato Excel (.xlsx)", "Exportación exitosa", MessageBoxButton.OK, MessageBoxImage.Information);
}
// Método que se ejecuta al hacer clic en el botón "Importar Notas"
    private void ImportarNotas_Click(object sender, RoutedEventArgs e)
{
        // Abrir el explorador de archivos para que el usuario elija el archivo
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "Archivos de Notas (*.notasunison;*.xlsx)|*.notasunison;*.xlsx";  // Filtrar por archivos .notasunison y .xlsx
        openFileDialog.DefaultExt = ".notasunison";  // Establecer la extensión predeterminada

        if (openFileDialog.ShowDialog() == true)
        {
            string filePath = openFileDialog.FileName;

            // Verificar la extensión del archivo
            if (Path.GetExtension(filePath) == ".notasunison")
            {
                // Leer archivo .notasunison (JSON)
                try
                {
                    string jsonNotas = File.ReadAllText(filePath);  // Leer el contenido del archivo

                    // Deserializar el JSON a una lista de objetos Nota
                    var notasData = JsonSerializer.Deserialize<List<Nota>>(jsonNotas);

                    // Limpiar la lista actual de notas
                    Notas.Clear();
                    foreach (var nota in notasData)
                    {
                        Notas.Add(nota);  // Agregar las notas importadas
                    }

                    // Mostrar un mensaje indicando que las notas se importaron correctamente
                    MessageBox.Show("Notas importadas correctamente desde el archivo .notasunison", "Importación Completa", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    // Manejo de errores si algo falla al leer el archivo .notasunison
                    MessageBox.Show($"Error al importar el archivo .notasunison: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else if (Path.GetExtension(filePath) == ".xlsx")
            {
                // Leer archivo .xlsx
                try
                {
                    var workbook = new XLWorkbook(filePath);  // Abrir el archivo Excel
                    var worksheet = workbook.Worksheet(1);   // Seleccionar la primera hoja del archivo

                    // Limpiar la lista actual de notas antes de importar nuevas
                    Notas.Clear();

                    // Leer las filas del archivo Excel
                    var rows = worksheet.RowsUsed().Skip(1);  // Saltar la primera fila (encabezados)

                    foreach (var row in rows)
                    {
                        string titulo = row.Cell(1).Value.ToString();
                        string contenido = row.Cell(2).Value.ToString();
                        string color = row.Cell(3).Value.ToString();

                        // Crear una nueva Nota y agregarla a la lista
                        Notas.Add(new Nota { Titulo = titulo, Contenido = contenido, Color = color });
                    }

                    // Mostrar mensaje de éxito
                    MessageBox.Show("Notas importadas correctamente desde el archivo .xlsx", "Importación Completa", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    // Manejo de errores si algo falla al leer el archivo .xlsx
                    MessageBox.Show($"Error al importar el archivo .xlsx: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
}
    }
}
