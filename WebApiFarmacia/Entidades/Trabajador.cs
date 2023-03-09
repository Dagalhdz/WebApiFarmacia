namespace WebApiFarmacia.Entidades
{
    public class Trabajador
    {
        public int TrabajadorId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string RFC { get; set; }
        public string telefono { get; set; }

        public List<Venta> Ventas { get; set; }
    }
}
