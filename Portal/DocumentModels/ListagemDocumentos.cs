using System.Collections.Generic;

namespace Consyste.Clients.Portal
{
    public class ListagemDocumentos
    {
        public long total { get; set; }
        public string proxima_pagina { get; set; }
        public List<Documento> documentos { get; set; }
    }
}
