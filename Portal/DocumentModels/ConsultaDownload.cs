namespace Consyste.Clients.Portal
{
    public class ConsultaDownload
    {
        public string id { get; set; }
        public string formato { get; set; }
        public string tipo_documento { get; set; }
        public int atual { get; set; }
        public int total { get; set; }
        public string erros { get; set; }
        public string concluido_em { get; set; }
        public string arquivo { get; set; }
    }
}