using System;
using System.Net;
using System.Threading.Tasks;
using System.Text;

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

        public async Task<ListagemDocumentos> ListaDocumentos(ModeloDocumento modelo, FiltroDocumento filtro, string[] campos = null, string consulta = null)
        {
            return await ListaDocumentos(CodigoModelo(modelo), CodigoFiltro(filtro), campos, consulta);
        }

        public async Task<ListagemDocumentos> ListaDocumentos(string modelo, string filtro, string[] campos = null, string consulta = null)
        {
            string campoParametro = "";
            string consultaParametro = "";

            if (campos != null)
            {
                foreach (string campo in campos)
                {
                    campoParametro = campoParametro + campo + ",";
                }
                campoParametro = "campos=" + campoParametro;
            }

            if (consulta != null)
            {
                consultaParametro = "q=" + Uri.EscapeUriString(consulta);
            }

            var res = await PerformGet($"/api/v1/{modelo}/lista/{filtro}?{consultaParametro}&{campoParametro}");

            if (res.StatusCode != HttpStatusCode.OK)
            {
                throw new ApplicationException($"Erro {res.StatusCode} ao solicitar listagem de documentos");
            }

            return JsonHandler<ListagemDocumentos>.Desserializar(res.GetResponseStream());
        }

        public async Task<ListagemDocumentos> ContinuaListagem(ModeloDocumento modelo, string token)
        {
            return await ContinuaListagem(CodigoModelo(modelo), token);
        }

        public async Task<ListagemDocumentos> ContinuaListagem(string modelo, string token)
        {
            var res = await PerformGet($"/api/v1/{modelo}/lista/continua/{token}");

            if (res.StatusCode != HttpStatusCode.OK)
            {
                throw new ApplicationException($"Erro {res.StatusCode} ao continuar listagem de documentos");
            }

            return JsonHandler<ListagemDocumentos>.Desserializar(res.GetResponseStream());
        }

        public async Task<HttpStatusCode> ManifestacaoNfe(ModeloDocumento modelo, string id, string manifestacao, string justificativa = null)
        {
            return await ManifestacaoNfe(CodigoModelo(modelo), id, manifestacao, justificativa);
        }
        public async Task<HttpStatusCode> ManifestacaoNfe(string modelo, string id, string manifestacao, string justificativa = null)
        {
            var res = await PerformPost($"/api/v1/{modelo}/{id}/manifestar/{manifestacao}?justificativa={justificativa}");

            if (res.StatusCode != HttpStatusCode.OK)
            {
                throw new ApplicationException($"Erro {res.StatusCode} ao continuar listagem de documentos");
            }

            return res.StatusCode;
        }

        public async Task<PortariaDocumento> DecisaoPortariaNFe(string chave, string decisao, string observacao = null)
        {
            var res = await PerformPost($"/api/v1/nfe/{chave}/decisao-portaria/{decisao}", observacao);

            if (res.StatusCode != HttpStatusCode.OK)
            {
                throw new ApplicationException($"Erro {res.StatusCode} ao continuar listagem de documentos");
            }

            return JsonHandler<PortariaDocumento>.Desserializar(res.GetResponseStream());
        }

        public async Task<SolicitaDownload> SolicitaDownload(ModeloDocumento modelo, FiltroDocumento filtro, FormatoDocumento formato, string consulta = "")
        {
            return await SolicitaDownload(CodigoModelo(modelo), CodigoFiltro(filtro), CodigoFormato(formato), consulta);
        }

        public async Task<SolicitaDownload> SolicitaDownload(string modelo, string filtro, string formato, string consulta = "")
        {
            var res = await PerformPost($"/api/v1/{modelo}/lista/{filtro}/download/{formato}?q={consulta}");

            if (res.StatusCode != HttpStatusCode.OK)
            {
                throw new ApplicationException($"Erro {res.StatusCode} ao solicitar documento");
            }

            return JsonHandler<SolicitaDownload>.Desserializar(res.GetResponseStream());
        }

        public async Task<ConsultaDownload> ConsultaDownloadSolicitado(string id)
        {
            var res = await PerformGet($"/api/v1/download/{id}");

            if (res.StatusCode != HttpStatusCode.OK)
            {
                throw new ApplicationException($"Erro {res.StatusCode} ao consultar o download solicitado");
            }

            return JsonHandler<ConsultaDownload>.Desserializar(res.GetResponseStream());
        }

        public async Task<Download> BaixaDocumento(ModeloDocumento modelo, FormatoDocumento formato, string chave)
        {
            return await BaixaDocumento(CodigoModelo(modelo), CodigoFormato(formato), chave);
        }

        public async Task<Download> BaixaDocumento(string modelo, string formato, string chave)
        {
            var res = await PerformGet($"/api/v1/{modelo}/{chave}/download{formato}");

            if (res.StatusCode != HttpStatusCode.OK)
            {
                throw new ApplicationException($"Erro {res.StatusCode} ao baixar documento");
            }

            return new Download(res);
        }

        private async Task<HttpWebResponse> PerformGet(string uri)
        {
            var req = (HttpWebRequest) HttpWebRequest.Create(Config.UrlBase + uri);
            req.Headers.Add(HttpRequestHeader.UserAgent, "Consyst-e .NET Client 1.0");
            req.Headers.Add(HttpRequestHeader.Accept, "application/json");
            req.Headers.Add("X-Consyste-Auth-Token", Config.ApiKey);

            return (HttpWebResponse) await req.GetResponseAsync();
        }

        private async Task<HttpWebResponse> PerformPost(string uri, string postData = null)
        {
            HttpWebRequest req = (HttpWebRequest) HttpWebRequest.Create(Config.UrlBase + uri);

            req.Method = "POST";
            req.Headers.Add(HttpRequestHeader.UserAgent, "Consyst-e .NET Client 1.0");
            req.Headers.Add(HttpRequestHeader.Accept, "application/json");
            req.Headers.Add("X-Consyste-Auth-Token", Config.ApiKey);

            if (postData != null)
            {
                var data = Encoding.ASCII.GetBytes(postData);

                req.ContentType = "Content-Type: application/json";
                req.ContentLength = data.Length;

                using (var stream = req.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }

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

        private string CodigoFormato(FormatoDocumento formato)
        {
            switch (formato)
            {
                case FormatoDocumento.Pdf:
                    return ".pdf";
                case FormatoDocumento.Xml:
                    return ".xml";
                default:
                    throw new ArgumentException($"Formato desconhecido: {formato}", nameof(formato));
            }
        }

        private string CodigoFiltro(FiltroDocumento filtro)
        {
            switch (filtro)
            {
                case FiltroDocumento.Emitidos:
                    return "emitidos";
                case FiltroDocumento.Recebidos:
                    return "recebidos";
                case FiltroDocumento.Tomados:
                    return "tomados";
                case FiltroDocumento.Todos:
                    return "todos";
                default:
                    throw new ArgumentException($"Filtro desconhecido: {filtro}", nameof(filtro));
            }
        }
    }
}
