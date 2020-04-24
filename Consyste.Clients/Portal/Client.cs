using System;
using System.Net;
using System.Threading.Tasks;
using System.Text;

namespace Consyste.Clients.Portal
{
    /// <summary>
    /// Esta classe contem os métodos para realizar os acessos à API da Consyste.
    /// </summary>
    public class Client
    {
        public Configuration Config { get; }

        public Client() : this(new Configuration())
        {
        }

        public Client(string apiKey) : this(new Configuration { ApiKey = apiKey })
        {
        }

        public Client(Configuration config)
        {
            Config = config;
        }

        /// <summary>
        /// Obtém uma lista com dados dos documentos constantes no Portal.
        /// </summary>
        public async Task<ListagemDocumentos> ListaDocumentos(ModeloDocumento modelo, FiltroDocumento filtro, string[] campos = null, string consulta = null)
        {
            return await ListaDocumentos(CodigoModelo(modelo), CodigoFiltro(filtro), campos, consulta);
        }

        /// <summary>
        /// Obtém uma lista com dados dos documentos constantes no Portal.
        /// </summary>
        public async Task<ListagemDocumentos> ListaDocumentos(string modelo, string filtro, string[] campos = null, string consulta = null)
        {
            var campoParametro = "";
            var consultaParametro = "";

            if (campos != null)
                campoParametro = "campos=" + String.Join(",", campos);

            if (consulta != null)
                consultaParametro = "q=" + Uri.EscapeUriString(consulta);

            var res = await PerformGet($"/api/v1/{modelo}/lista/{filtro}?{consultaParametro}&{campoParametro}");

            if (res.StatusCode != HttpStatusCode.OK)
                throw new ApplicationException($"Erro {res.StatusCode} ao solicitar listagem de documentos");

            return JsonHandler.Desserializar<ListagemDocumentos>(res.GetResponseStream());
        }

        /// <summary>
        /// Para continuar uma busca da listagem dos documentos.
        /// </summary>
        public async Task<ListagemDocumentos> ContinuaListagem(ModeloDocumento modelo, string token)
        {
            return await ContinuaListagem(CodigoModelo(modelo), token);
        }

        /// <summary>
        /// Para continuar uma busca da listagem dos documentos.
        /// </summary>
        public async Task<ListagemDocumentos> ContinuaListagem(string modelo, string token)
        {
            var res = await PerformGet($"/api/v1/{modelo}/lista/continua/{token}");

            if (res.StatusCode != HttpStatusCode.OK)
            {
                throw new ApplicationException($"Erro {res.StatusCode} ao continuar listagem de documentos");
            }

            return JsonHandler.Desserializar<ListagemDocumentos>(res.GetResponseStream());
        }

        /// <summary>
        /// Obtém os dados de um documento constante no Portal.
        /// </summary>
        public async Task<Documento> ConsultaDocumento(ModeloDocumento modelo, string id)
        {
            return await ConsultaDocumento(CodigoModelo(modelo), id);
        }

        /// <summary>
        /// Obtém os dados de um documento constante no Portal.
        /// </summary>
        public async Task<Documento> ConsultaDocumento(string modelo, string id)
        {
            var res = await PerformGet($"/api/v1/{modelo}/{id}");

            if (res.StatusCode != HttpStatusCode.OK)
            {
                throw new ApplicationException($"Erro {res.StatusCode} ao consultar documento");
            }

            return JsonHandler.Desserializar<Documento>(res.GetResponseStream());
        }

        /// <summary>
        /// Envia um XML individual para custódia. Pode ser utilizado para enviar NF-e ou CT-e. Tanto podem ser enviados documentos relacionados ao CNPJ quanto documentos de terceiros.
        /// </summary>
        public async Task<RootDocumento> EnviaDocumento(string xml, string terceiroCnpj = null)
        {
            var documento = new EnviaDocumento { Xml = xml, TerceiroCnpj = terceiroCnpj };
            var postData = JsonHandler.Serializar(documento);

            var res = await PerformPost("/api/v1/envio", postData);

            if (res.StatusCode != HttpStatusCode.Created)
                throw new ApplicationException($"Erro {res.StatusCode} ao enviar documento");

            return JsonHandler.Desserializar<RootDocumento>(res.GetResponseStream());
        }

        /// <summary>
        /// Solicita os arquivos XML ou PDF de um lote de documentos fiscais.
        /// </summary>        
        public async Task<SolicitaDownload> SolicitaDownload(ModeloDocumento modelo, FiltroDocumento filtro, FormatoDocumento formato, string consulta = "")
        {
            return await SolicitaDownload(CodigoModelo(modelo), CodigoFiltro(filtro), CodigoFormato(formato), consulta);
        }

        /// <summary>
        /// Solicita os arquivos XML ou PDF de um lote de documentos fiscais.
        /// </summary>  
        public async Task<SolicitaDownload> SolicitaDownload(string modelo, string filtro, string formato, string consulta = "")
        {
            var res = await PerformPost($"/api/v1/{modelo}/lista/{filtro}/download/{formato}?q={consulta}");

            if (res.StatusCode != HttpStatusCode.Accepted)
                throw new ApplicationException($"Erro {res.StatusCode} ao solicitar documento");

            return JsonHandler.Desserializar<SolicitaDownload>(res.GetResponseStream());
        }

        /// <summary>
        /// Consulta a disponibilidade de download dos arquivos previamente solicitados.
        /// </summary>
        public async Task<ConsultaDownload> ConsultaDownloadSolicitado(string id)
        {
            var res = await PerformGet($"/api/v1/download/{id}");

            if (res.StatusCode != HttpStatusCode.OK)
                throw new ApplicationException($"Erro {res.StatusCode} ao consultar o download solicitado");

            return JsonHandler.Desserializar<ConsultaDownload>(res.GetResponseStream());
        }

        /// <summary>
        /// Obtém o XML ou PDF de um documento constante no Portal.
        /// </summary>
        public async Task<Download> BaixaDocumento(ModeloDocumento modelo, FormatoDocumento formato, string chave)
        {
            return await BaixaDocumento(CodigoModelo(modelo), CodigoFormato(formato), chave);
        }

        /// <summary>
        /// Obtém o XML ou PDF de um documento constante no Portal.
        /// </summary>
        public async Task<Download> BaixaDocumento(string modelo, string formato, string chave)
        {
            var res = await PerformGet($"/api/v1/{modelo}/{chave}/download.{formato}");

            if (res.StatusCode != HttpStatusCode.OK)
                throw new ApplicationException($"Erro {res.StatusCode} ao baixar documento");

            return new Download(res);
        }

        /// <summary>
        /// A empresa poderá informar a manifestação acerca de suas notas destinadas à SEFAZ.
        /// </summary>
        public async Task<HttpStatusCode> ManifestacaoNfe(ModeloDocumento modelo, string id, Manifestacao manifestacao, string justificativa = null)
        {
            return await ManifestacaoNfe(CodigoModelo(modelo), id, CodigoManifestacao(manifestacao), justificativa);
        }

        /// <summary>
        /// A empresa poderá informar a manifestação acerca de suas notas destinadas à SEFAZ.
        /// </summary>
        public async Task<HttpStatusCode> ManifestacaoNfe(string modelo, string id, string manifestacao, string justificativa = null)
        {
            var parametroJustificativa = "";

            if (justificativa != null)
                parametroJustificativa = "?justificativa=" + Uri.EscapeUriString(justificativa);

            var res = await PerformPost($"/api/v1/{modelo}/{id}/manifestar/{manifestacao}{parametroJustificativa}");

            if (res.StatusCode != HttpStatusCode.OK)
                throw new ApplicationException($"Erro {res.StatusCode} ao continuar listagem de documentos");

            return res.StatusCode;
        }

        /// <summary>
        /// Salva a decisão da portaria em documento NF-e.
        /// </summary>
        public async Task<RootDocumento> DecisaoPortariaNFe(string chave, Decisao decisao, string observacao = null)
        {
            return await DecisaoPortariaNFe(chave, CodigoDecisao(decisao), observacao);
        }

        /// <summary>
        /// Salva a decisão da portaria em documento NF-e.
        /// </summary>
        public async Task<RootDocumento> DecisaoPortariaNFe(string chave, string decisao, string observacao = null)
        {
            string postData = null;

            if (observacao != null)
            {
                var obs = new DecisaoObservacao { Observacao = observacao };
                postData = JsonHandler.Serializar(obs);
            }

            var res = await PerformPost($"/api/v1/nfe/{chave}/decisao-portaria/{decisao}", postData);

            if (res.StatusCode != HttpStatusCode.Created)
                throw new ApplicationException($"Erro {res.StatusCode} ao salvar a decisão da portaria ");

            return JsonHandler.Desserializar<RootDocumento>(res.GetResponseStream());
        }

        /// <summary>
        /// Método responsável por realizar as requisições no método GET.
        /// </summary>
        private async Task<HttpWebResponse> PerformGet(string uri)
        {
            var req = (HttpWebRequest) WebRequest.Create(Config.UrlBase + uri);
            req.Headers.Add("X-Consyste-Auth-Token", Config.ApiKey);

            return (HttpWebResponse) await req.GetResponseAsync();
        }

        /// <summary>
        /// Método responsável por realizar as requisições no método POST.
        /// </summary>
        private async Task<HttpWebResponse> PerformPost(string uri, string postData = null)
        {
            var req = (HttpWebRequest) WebRequest.Create(Config.UrlBase + uri);

            req.Method = "POST";
            req.Headers.Add("X-Consyste-Auth-Token", Config.ApiKey);

            if (postData == null)
                return (HttpWebResponse) await req.GetResponseAsync();

            var data = Encoding.UTF8.GetBytes(postData);
            req.ContentType = "Content-Type: application/json";
            req.ContentLength = data.Length;

            using var stream = req.GetRequestStream();
            await stream.WriteAsync(data, 0, data.Length);

            return (HttpWebResponse) await req.GetResponseAsync();
        }

        # region Parâmetros

        private string CodigoModelo(ModeloDocumento modelo) =>
            modelo switch
            {
                ModeloDocumento.Nfe => "nfe",
                ModeloDocumento.Cte => "cte",
                _                   => throw new ArgumentException($"Modelo desconhecido: {modelo}", nameof(modelo))
            };

        private string CodigoFormato(FormatoDocumento formato) =>
            formato switch
            {
                FormatoDocumento.PDF => "pdf",
                FormatoDocumento.XML => "xml",
                _                    => throw new ArgumentException($"Formato desconhecido: {formato}", nameof(formato))
            };

        private string CodigoFiltro(FiltroDocumento filtro) =>
            filtro switch
            {
                FiltroDocumento.Emitidos  => "emitidos",
                FiltroDocumento.Recebidos => "recebidos",
                FiltroDocumento.Tomados   => "tomados",
                FiltroDocumento.Todos     => "todos",
                _                         => throw new ArgumentException($"Filtro desconhecido: {filtro}", nameof(filtro))
            };

        private string CodigoManifestacao(Manifestacao manifestacao) =>
            manifestacao switch
            {
                Manifestacao.Confirmada           => "confirmada",
                Manifestacao.Desconhecida         => "desconhecida",
                Manifestacao.OperacaoNaoRealizada => "operacao_nao_realizada",
                Manifestacao.Ciencia              => "ciencia",
                _                                 => throw new ArgumentException($"Manifestação desconhecida: {manifestacao}", nameof(manifestacao))
            };

        private string CodigoDecisao(Decisao decisao) =>
            decisao switch
            {
                Decisao.Receber             => "receber",
                Decisao.Devolver            => "devolver",
                Decisao.ReceberComPendencia => "receber_com_pendencia",
                _                           => throw new ArgumentException($"Decisão desconhecida: {decisao}", nameof(decisao))
            };

        # endregion
    }
}