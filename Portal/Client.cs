using System;
using System.Net;
using System.Threading.Tasks;

namespace Consyste.Clients.Portal
{
    public class Client
    {
        public Configuration Config { get; }

        public Client() : this(new Configuration()) { }

        public Client(string apiKey) : this(new Configuration { ApiKey = apiKey }) { }

        public Client(Configuration config)
        {
            Config = config;
        }

        public async Task<ListagemDocumentos> ListaDocumentos(ModeloDocumento modelo, string filtro, string consulta = null, string[] campos = null)
        {
            return await ListaDocumentos(CodigoModelo(modelo), filtro, consulta, campos);
        }

        public async Task<ListagemDocumentos> ListaDocumentos(string modelo, string filtro, string consulta = null, string[] campos = null)
        {
            var res = await PerformGet($"/api/v1/{modelo}/lista/{filtro}?q=" + Uri.EscapeUriString(consulta));

            if (res.StatusCode != HttpStatusCode.OK)
                throw new ApplicationException($"Erro {res.StatusCode} ao solicitar listagem de documentos");

            return ListagemDocumentos.FromJSON(res.GetResponseStream());
        }

        public async Task<ListagemDocumentos> ContinuaListagem(ModeloDocumento modelo, string token)
        {
            return await ContinuaListagem(CodigoModelo(modelo), token);
        }

        public async Task<ListagemDocumentos> ContinuaListagem(string modelo, string token)
        {
            var res = await PerformGet($"/api/v1/{modelo}/lista/continua/{token}");

            if (res.StatusCode != HttpStatusCode.OK)
                throw new ApplicationException($"Erro {res.StatusCode} ao continuar listagem de documentos");

            return ListagemDocumentos.FromJSON(res.GetResponseStream());
        }

        private async Task<HttpWebResponse> PerformGet(string uri)
        {
            var req = (HttpWebRequest) HttpWebRequest.Create(Config.UrlBase + uri);
            req.Headers.Add(HttpRequestHeader.UserAgent, "Consyst-e .NET Client 1.0");
            req.Headers.Add(HttpRequestHeader.Accept, "application/json");
            req.Headers.Add("X-Consyste-Auth-Token", Config.ApiKey);

            return (HttpWebResponse) await req.GetResponseAsync();
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
                    throw new ArgumentException($"Modelo desconhecido: {modelo}", nameof(modelo));
            }
        }
    }
}
