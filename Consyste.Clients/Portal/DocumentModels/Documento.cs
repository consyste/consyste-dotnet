namespace Consyste.Clients.Portal
{
    public class Documento
    {
        public string Id { get; set; }
        public string Chave { get; set; }
        public string EmitidoEm { get; set; }
        public int? Serie { get; set; }
        public int? Numero { get; set; }
        public string Valor { get; set; }
        public string SituacaoCustodia { get; set; }
        public int? SituacaoSefaz { get; set; }
        public string EmitCnpj { get; set; }
        public string EmitNome { get; set; }
        public string DestCnpj { get; set; }
        public string DestNome { get; set; }
        public string TomaNome { get; set; }
        public string TomaCnpj { get; set; }
        public string DataDecisaoPortaria { get; set; }
        public string ObservacaoPortaria { get; set; }
    }

    public class RootDocumento
    {
        public Documento Documento { get; set; }
    }
}