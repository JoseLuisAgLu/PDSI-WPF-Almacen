namespace Unison_Almacen_Core.Modelos;

public class Producto
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public float Precio { get; set; }
}