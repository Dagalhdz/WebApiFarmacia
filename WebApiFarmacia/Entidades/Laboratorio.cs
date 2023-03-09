namespace WebApiFarmacia.Entidades
{
    public class Laboratorio
    {
        public int LaboratorioId { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set;}

        public List<Medicamento> Medicamentos { get; set; }

    }
}
