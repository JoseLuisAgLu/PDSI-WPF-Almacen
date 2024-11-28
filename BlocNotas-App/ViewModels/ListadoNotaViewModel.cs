using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using BlocNotas_Core.Models;
using Microsoft.Win32;
using ClosedXML.Excel;

namespace BlocNotas.ViewModels
{
    public partial class ListadoNotaViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Nota> notas;

        public ListadoNotaViewModel()
        {
            using var context = new NotaContext();
            Notas = new ObservableCollection<Nota>(context.Notas.ToList());
        }

        [RelayCommand]
        public void AgregarNota()
        {
            // Abrir la ventana de agregar nota
            var ventanaAgregar = new AgregarNota();
            if (ventanaAgregar.ShowDialog() == true)
            {
                // Obtener la nueva nota desde la ventana y agregarla a la lista
                if (ventanaAgregar.DataContext is AgregarNotaViewModel vm && vm.NuevaNota != null)
                {
                    using var context = new NotaContext();
                    context.Notas.Add(vm.NuevaNota);
                    context.SaveChanges();
                    Notas.Add(vm.NuevaNota); // Actualizar la lista local
                }
            }
        }

        [RelayCommand]
        public void ModificarNota(Nota nota)
        {
            if (nota == null) return;

            var ventanaModificar = new BlocNotas.Views.ModificarNotaView(nota);
            if (ventanaModificar.ShowDialog() == true)
            {
                using var context = new NotaContext();
                var notaAActualizar = context.Notas.FirstOrDefault(n => n.Id == nota.Id);
                if (notaAActualizar != null)
                {
                    notaAActualizar.Titulo = nota.Titulo;
                    notaAActualizar.Contenido = nota.Contenido;
                    notaAActualizar.Color = nota.Color;
                    context.SaveChanges();
                }
                var indice = Notas.IndexOf(nota);
                if (indice >= 0)
                {
                    Notas[indice] = nota;
                }
            }
        }

        [RelayCommand]
        public void EliminarNota(Nota nota)
        {
            if (nota == null) return;

            var ventanaConfirmacion = new BlocNotas.Views.ConfirmacionEliminarView();
            if (ventanaConfirmacion.ShowDialog() == true && ventanaConfirmacion.EsConfirmado)
            {
                using var context = new NotaContext();
                var notaAEliminar = context.Notas.FirstOrDefault(n => n.Id == nota.Id);
                if (notaAEliminar != null)
                {
                    context.Notas.Remove(notaAEliminar);
                    context.SaveChanges();
                    Notas.Remove(nota);
                }
            }
        }

        [RelayCommand]
        public void ActualizarPagina()
        {
            using var context = new NotaContext();
            Notas = new ObservableCollection<Nota>(context.Notas.ToList());
        }

        [RelayCommand]
        public void ExportarNotas()
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Notas de Unison (*.notasunison)|*.notasunison|Excel Files (*.xlsx)|*.xlsx",
                DefaultExt = ".notasunison"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                var filePath = saveFileDialog.FileName;
                if (filePath.EndsWith(".notasunison"))
                {
                    var jsonNotas = JsonSerializer.Serialize(Notas);
                    File.WriteAllText(filePath, jsonNotas);
                }
                else if (filePath.EndsWith(".xlsx"))
                {
                    using var workbook = new XLWorkbook();
                    var worksheet = workbook.AddWorksheet("Notas");

                    worksheet.Cell(1, 1).Value = "Título";
                    worksheet.Cell(1, 2).Value = "Contenido";
                    worksheet.Cell(1, 3).Value = "Color";

                    for (int i = 0; i < Notas.Count; i++)
                    {
                        var nota = Notas[i];
                        worksheet.Cell(i + 2, 1).Value = nota.Titulo;
                        worksheet.Cell(i + 2, 2).Value = nota.Contenido;
                        worksheet.Cell(i + 2, 3).Value = nota.Color;
                    }

                    workbook.SaveAs(filePath);
                }
            }
        }

        [RelayCommand]
        public void ImportarNotas()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Notas de Unison (*.notasunison)|*.notasunison|Excel Files (*.xlsx)|*.xlsx",
                DefaultExt = ".notasunison"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                var filePath = openFileDialog.FileName;

                if (filePath.EndsWith(".notasunison"))
                {
                    var jsonNotas = File.ReadAllText(filePath);
                    var notasImportadas = JsonSerializer.Deserialize<ObservableCollection<Nota>>(jsonNotas);

                    Notas.Clear();
                    foreach (var nota in notasImportadas)
                    {
                        Notas.Add(nota);
                    }
                }
                else if (filePath.EndsWith(".xlsx"))
                {
                    using var workbook = new XLWorkbook(filePath);
                    var worksheet = workbook.Worksheet(1);

                    var rows = worksheet.RowsUsed().Skip(1);

                    Notas.Clear();
                    foreach (var row in rows)
                    {
                        var nota = new Nota
                        {
                            Titulo = row.Cell(1).GetValue<string>(),
                            Contenido = row.Cell(2).GetValue<string>(),
                            Color = row.Cell(3).GetValue<string>()
                        };
                        Notas.Add(nota);
                    }
                }
            }
        }
        [RelayCommand]
        public void RegresarAlInicio(Window currentWindow)
        {
            var inicioView = new BlocNotas.Views.InicioView();
            inicioView.Show();
            currentWindow.Close();
        }

    }
}
