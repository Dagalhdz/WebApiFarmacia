namespace WebApiFarmacia.Entidades
{
    public class Medicamento
    {
        public int MedicamentoId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int LaboratorioId  { get; set; }
        public Laboratorio Laboratorio { get; set; }
        public int ModoAdministracionId { get; set; }
        public ModoAdministracion ModoAdministracion { get; set; }
        public double Precio { get; set; }
        public DateTime FechaCaducidad { get; set; }

        public List<Venta> Ventas { get; set; }
    }
}
