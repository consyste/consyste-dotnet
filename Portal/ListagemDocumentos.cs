using System.Collections.Generic;
using System.IO;

using Newtonsoft.Json;

namespace Consyste.Clients.Portal
{
    public class Documento
    {
        public string id { get; set; }
        public string chave { get; set; }
        public string emitido_em { get; set; }
        public int serie { get; set; }
        public int numero { get; set; }
        public string valor { get; set; }
        public string situacao_custodia { get; set; }
        public int situacao_sefaz { get; set; }
        public string emit_cnpj { get; set; }
        public string emit_nome { get; set; }
        public string dest_cnpj { get; set; }
        public string dest_nome { get; set; }
        public string toma_nome { get; set; }
        public string toma_cnpj { get; set; }
    }

    public class ListagemDocumentos
    {
        static readonly JsonSerializer jsonSerializer = JsonSerializer.CreateDefault();

        public ListagemDocumentos()
        {
        }

        public long total { get; set; }
        public string proxima_pagina { get; set; }
        public List<Documento> documentos { get; set; }

        public static ListagemDocumentos FromJSON(Stream stream)
        {
            using (var sr = new StreamReader(stream))
            {
                using (var jr = new JsonTextReader(sr))
                {
                    return jsonSerializer.Deserialize<ListagemDocumentos>(jr);
                }
            }
        }
    }
}
