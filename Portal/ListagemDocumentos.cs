using System.Collections.Generic;
using System.IO;

using Newtonsoft.Json;

namespace Consyste.Clients.Portal
{
    public class Documento
    {
        public string id { get; }
        public string chave { get; }
        public string emitido_em { get; }
        public int serie { get; }
        public int numero { get; }
        public string valor { get; }
        public string situacao_custodia { get; }
        public int situacao_sefaz { get; }
        public string emit_cnpj { get; }
        public string emit_nome { get; }
        public string dest_cnpj { get; }
        public string dest_nome { get; }
        public string toma_nome { get; }
        public string toma_cnpj { get; }
    }

    public class ListagemDocumentos
    {
        static readonly JsonSerializer jsonSerializer = JsonSerializer.CreateDefault();

        public ListagemDocumentos()
        {
        }

        public long total { get; }
        public string proxima_pagina { get; }
        public List<Documento> documentos { get; }

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
