namespace Unison_Almacen_Core.Modelos
{
    public class Nota
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; } 
        public string Contenido { get; set; } 
        public string Color { get; set; } 
        
        public DateTime FechaCreacion { get; set; }
    }
}