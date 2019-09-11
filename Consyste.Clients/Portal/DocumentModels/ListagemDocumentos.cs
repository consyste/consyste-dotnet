using System.Collections.Generic;

namespace Consyste.Clients.Portal
{
    public class ListagemDocumentos
    {
        public long Total { get; set; }
        public string ProximaPagina { get; set; }
        public List<Documento> Documentos { get; set; }
    }
}
