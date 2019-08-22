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
        public string data_decisao_portaria { get; set; }
        public string observacao_portaria { get; set; }
    }

    public class RootDocumento
    {
        public Documento documento { get; set; }
    }
}