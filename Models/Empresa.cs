namespace PruebaAFP.Models
{
    public class Empresa
    {
        public int EmpresaID { get; set; }
        public string Nombre { get; set; }
        public string RazonSocial { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string DetallesBitacora { get; set; }
    }
}
