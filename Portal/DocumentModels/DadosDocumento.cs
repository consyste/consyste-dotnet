using System.Collections.Generic;

namespace Consyste.Clients.Portal
{
    public class Historico
    {
        public string id { get; set; }
        public string created_at { get; set; }
        public object observacoes { get; set; }
        public string operacao { get; set; }
        public string updated_at { get; set; }
        public object usuario_id { get; set; }
    }

    public class Iten
    {
        public int cfop { get; set; }
        public string codigo { get; set; }
        public string descricao { get; set; }
        public object ean { get; set; }
        public object ean_trib { get; set; }
        public int ncm { get; set; }
        public double valor_total { get; set; }
        public double valor_unitario { get; set; }
    }

    public class DadosDocumento
    {
        public string id { get; set; }
        public string ambiente_sefaz_id { get; set; }
        public string autorizado_em { get; set; }
        public string chave { get; set; }
        public object cobr_data_vencimento { get; set; }
        public string created_at { get; set; }
        public object data_decisao_portaria { get; set; }
        public object decisao_portaria { get; set; }
        public string dest_cnpj { get; set; }
        public object dest_email { get; set; }
        public string dest_end_uf { get; set; }
        public object dest_fantasia { get; set; }
        public string dest_ie { get; set; }
        public object dest_isuframa { get; set; }
        public string dest_nome { get; set; }
        public object digest_autorizado { get; set; }
        public string digest_value { get; set; }
        public object emit_cnae { get; set; }
        public string emit_cnpj { get; set; }
        public string emit_crt { get; set; }
        public object emit_email { get; set; }
        public string emit_end_uf { get; set; }
        public string emit_fantasia { get; set; }
        public string emit_ie { get; set; }
        public object emit_im { get; set; }
        public string emit_nome { get; set; }
        public string emitido_em { get; set; }
        public string entrada_saida { get; set; }
        public string fingerprint { get; set; }
        public List<Historico> historicos { get; set; }
        public List<Iten> itens { get; set; }
        public object justificativa_sefaz { get; set; }
        public bool lido { get; set; }
        public string lido_em { get; set; }
        public int manifestacao_cd { get; set; }
        public object manifestacao_justificativa { get; set; }
        public string manifestacao_realizada_em { get; set; }
        public string manifestacao_realizada_por_id { get; set; }
        public int modelo_id { get; set; }
        public object modifier_id { get; set; }
        public int municipio_fato_gerador_id { get; set; }
        public string natureza_operacao { get; set; }
        public int numero { get; set; }
        public object numero_protocolo { get; set; }
        public object observacao_portaria { get; set; }
        public string recebido_em { get; set; }
        public int serie { get; set; }
        public string situacao_custodia { get; set; }
        public int situacao_sefaz { get; set; }
        public int tipo_operacao_id { get; set; }
        public string uf_id { get; set; }
        public object ultima_consulta_em { get; set; }
        public int ultima_manifestacao_cd { get; set; }
        public string ultima_manifestacao_data { get; set; }
        public string ultima_manifestacao_descricao { get; set; }
        public string updated_at { get; set; }
        public string valor { get; set; }
        public string valor_bruto { get; set; }
        public string valor_total_icms { get; set; }
        public string versao { get; set; }
    }
}