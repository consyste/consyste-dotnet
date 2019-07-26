using System.Collections.Generic;
using System.IO;

using Newtonsoft.Json;

namespace Consyste.Clients.Portal
{
    public class ListagemDocumentos
    {
        static readonly JsonSerializer jsonSerializer = JsonSerializer.CreateDefault();

        public ListagemDocumentos()
        {
        }

        public long Total { get; }
        public string ProximaPagina { get; }
        public List<dynamic> Documentos { get; }

        public static ListagemDocumentos FromJSON(Stream stream)
        {
            using (var sr = new StreamReader(stream))
            using (var jr = new JsonTextReader(sr))
            {
                return jsonSerializer.Deserialize<ListagemDocumentos>(jr);
            }
        }
    }
}
