namespace Consyste.Clients.Portal
{
    public class ConsultaDownload
    {
        public string Id { get; set; }
        public string Formato { get; set; }
        public string TipoDocumento { get; set; }
        public int? Atual { get; set; }
        public int? Total { get; set; }
        public string Erros { get; set; }
        public string ConcluidoEm { get; set; }
        public string Arquivo { get; set; }
    }
}