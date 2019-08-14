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

        public async Task<ListagemDocumentos> ListaDocumentos(ModeloDocumento modelo, FiltroDocumento filtro, string consulta = null, Campo[] campos = null)
        {   
            string campo = "";
            foreach (Campo c in campos)
            {   
                var campoParametro = CodigoCampo(c);
                campo = campo + campoParametro + ",";
            }

            return await ListaDocumentos(CodigoModelo(modelo), CodigoFiltro(filtro), consulta, campo);
        }

        public async Task<ListagemDocumentos> ListaDocumentos(string modelo, string filtro, string consulta = null, string campos = null)
        {   
            var res = await PerformGet($"/api/v1/{modelo}/lista/{filtro}?q=" + Uri.EscapeUriString(consulta) + "&campos={campos}");

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

        public async Task<HttpWebResponse> BaixaDocumento(ModeloDocumento modelo, FormatoDocumento formato, string chave)
        {
            return await BaixaDocumento(CodigoModelo(modelo), CodigoFormato(formato), chave);
        }

        public async Task<HttpWebResponse> BaixaDocumento(string modelo, string formato, string chave)
        {
            var res = await PerformGet($"/api/v1/{modelo}/{chave}/download{formato}");

            if (res.StatusCode != HttpStatusCode.OK)
                throw new ApplicationException($"Erro {res.StatusCode} ao baixar documento");

            return res;
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

        private string CodigoCampo(Campo campo)
        {
            switch (campo)
            {
                case Campo.Id:
                    return "id";
                case Campo.Chave:
                    return "chave";
                case Campo.EmitidoEm:
                    return "emitido_em";
                case Campo.Serie:
                    return "serie";
                case Campo.Numero:
                    return "numero";
                case Campo.Valor:
                    return "valor";
                case Campo.SituacaoCustodia:
                    return "situacao_custodia";
                case Campo.SituacaoSefaz:
                    return "situacao_sefaz";
                case Campo.EmitenteCnpj:
                    return "emit_cnpj";
                case Campo.EmitenteNome:
                    return "emit_nome";
                case Campo.DestinatarioCnpj:
                    return "dest_cnpj";
                case Campo.DestinatarioNome:
                    return "dest_nome";
                // Apenas Cte
                case Campo.TomadoCnpj:
                    return "toma_cnpj";
                // Apenas Cte
                case Campo.TomadoNome:
                    return "toma_cnpj";
                default:
                    throw new ArgumentException($"Campo desconhecido: {campo}", nameof(campo));
            }
        }

    }
}
