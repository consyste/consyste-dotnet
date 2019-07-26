namespace Consyste.Clients.Portal
{
    public class Configuration
    {
        private const string UrlBaseProducao = "https://portal.consyste.com.br";
        private const string UrlBaseHomologacao = "https://hml.consyste.com.br";

        /// <summary>
        /// A chave de acesso ao Portal Consyste. É individual por usuário.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// A URL base a utilizar para as chamadas. Deve ser alterado, caso seja necessário realizar testes em homologação.
        /// </summary>
        public string UrlBase { get; set; } = UrlBaseProducao;
    }
}
