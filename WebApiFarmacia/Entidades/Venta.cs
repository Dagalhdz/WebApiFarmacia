namespace WebApiFarmacia.Entidades
{
    public class Venta
    {
        public int VentaId { get; set; }
        public int TrabajadorId { get; set; }
        public Trabajador Trabajador { get; set; }
        public int MedicamentoId { get; set; }
        public Medicamento Medicamento { get; set; }
        public DateTime Fecha { get; set; }
        public int Cantidad { get; set; }
        public double Total { get; set; }
    }
}
