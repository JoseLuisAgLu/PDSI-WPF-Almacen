namespace Unison_Almacen_Core.Modelos
{
    public class Nota
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Contenido { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
    }
}