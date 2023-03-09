namespace WebApiFarmacia.Entidades
{
    public class ModoAdministracion
    {
        public int ModoAdministracionId { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }

        public List<Medicamento> Medicamentos { get; set; }
    }
}
