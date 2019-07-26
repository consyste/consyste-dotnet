using System;
using System.Net;
using System.Threading.Tasks;

namespace Consyste.Clients.Portal
{
    public class Client
    {
        public Configuration Config { get; }

        public Client() : this(new Configuration())
        {
        }

        public Client(Configuration config)
        {
            Config = config;
        }

        public async Task<ListagemDocumentos> ListaDocumentos(ModeloDocumento modelo, string filtro, string consulta = null, string[] campos = null)
        {
            string codModelo = CodigoModelo(modelo);

            var uri = $"{Config.UrlBase}/api/v1/{codModelo}/lista/{filtro}?q=" + Uri.EscapeUriString(consulta);

            var req = (HttpWebRequest) HttpWebRequest.Create(uri);
            var res = (HttpWebResponse) await req.GetResponseAsync();

            if (res.StatusCode != HttpStatusCode.OK)
                throw new ApplicationException($"Erro {res.StatusCode} ao solicitar listagem de documentos");

            return ListagemDocumentos.FromJSON(res.GetResponseStream());
        }

        private string CodigoModelo(ModeloDocumento modelo)
        {
            switch (modelo)
            {
                case ModeloDocumento.Nfe:
                    return "nfe";
                case ModeloDocumento.Cte:
                    return "cte";
                default:
                    throw new ArgumentException($"Filtro desconhecido: {modelo}", nameof(modelo));
            }
        }
    }
}
