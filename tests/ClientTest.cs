using Xunit;
using System.Threading.Tasks;
using System.IO;
using System.Text;

namespace Consyste.Clients.Portal
{
    public class TestClient
    {

        public TestClient()
        {
        }

        # region ListaDocumentosNfe
        [Theory()]
        [InlineData(ModeloDocumento.Nfe, FiltroDocumento.Emitidos, 0)]
        [InlineData(ModeloDocumento.Nfe, FiltroDocumento.Recebidos, 195)]
        [InlineData(ModeloDocumento.Nfe, FiltroDocumento.Todos, 195)]
        public async Task TestListaDocumentosNfeTotal(ModeloDocumento modelo, FiltroDocumento filtro, long total)
        {
            var res = await Cliente().ListaDocumentos(modelo, filtro);

            Assert.Equal(total, res.total);
        }

        [Theory()]
        [InlineData(ModeloDocumento.Nfe, FiltroDocumento.Emitidos, 0)]
        [InlineData(ModeloDocumento.Nfe, FiltroDocumento.Recebidos, 68)]
        [InlineData(ModeloDocumento.Nfe, FiltroDocumento.Todos, 68)]
        public async void TestListaDocumentosNfeDocumentos(ModeloDocumento modelo, FiltroDocumento filtro, int documentosCount)
        {
            string[] campos = new string[2] { "dest_nome", "emit_nome" };

            var consulta = "Kalunga";

            var res = await Cliente().ListaDocumentos(modelo, filtro, campos, consulta);
            Assert.Equal(documentosCount, res.documentos.Count);

        }
        # endregion

        # region ListaDocumentosCte
        [Theory()]
        [InlineData(ModeloDocumento.Cte, FiltroDocumento.Emitidos, 0)]
        [InlineData(ModeloDocumento.Cte, FiltroDocumento.Tomados, 0)]
        [InlineData(ModeloDocumento.Cte, FiltroDocumento.Todos, 19)]
        public async Task TestListaDocumentosCteTotal(ModeloDocumento modelo, FiltroDocumento filtro, long total)
        {
            var res = await Cliente().ListaDocumentos(modelo, filtro);

            Assert.Equal(total, res.total);
        }

        [Theory()]
        [InlineData(ModeloDocumento.Cte, FiltroDocumento.Emitidos, 0)]
        [InlineData(ModeloDocumento.Cte, FiltroDocumento.Tomados, 0)]
        [InlineData(ModeloDocumento.Cte, FiltroDocumento.Todos, 19)]
        public async void TestListaDocumentosCteDocumentos(ModeloDocumento modelo, FiltroDocumento filtro, int documentosCount)
        {
            string[] campos = new string[2] { "toma_cnpj", "toma_nome" };

            var res = await Cliente().ListaDocumentos(modelo, filtro, campos);

            Assert.Equal(documentosCount, res.documentos.Count);
        }
        # endregion

        # region ContinuaListagemNfe
        [Theory()]
        [InlineData(ModeloDocumento.Nfe, 195, "H4sIACFUVF0AAxXKTQ5AMBBA4X1PMWI9MRr1Mzs7t5BWR1jQaFlweiy-l4eICoE9wxl8SAqAD4Y-pfURBk30IYuXuwsbo715sGlZhbn-9f0IPkzXJvsZxn2W3PiqdU4brRsjDRGV1NVSTuoFIAdepWsAAAA_")]
        [InlineData(ModeloDocumento.Nfe, 0, "H4sIANYrXV0AA9PV1eXSVbBKsVJIzc0syUzJL+ZSULAqtFIAiRZnVqVaKRgZGAA5XACQKnVtKgAAAA__")]
        public async Task TestContinuaListagemNfe(ModeloDocumento modelo, long total, string token)
        {
            var res = await Cliente().ContinuaListagem(modelo, token);

            Assert.Equal(total, res.total);
        }
        # endregion

        # region ContinuaListagemCte
        [Theory()]
        [InlineData(ModeloDocumento.Cte, 19, "H4sIAHdUVF0AAxWLuw6AIAwAd76ixrkRH0TSzc2-MGhrdFAi4KBfL453uUNEhUBMkDz7qADoIvhV3F8haLTOUIR7fioXgntodHHbhWj4KfcI7Jf7kDP5aUlSGu7sKtxaa9Z8a133xorp1AfeUTzTawAAAA__")]
        [InlineData(ModeloDocumento.Cte, 0, "H4sIAKwvXV0AA9PV1eXSVbBKsVJIzc0syUzJL+ZSULAqtFIAiRZnVqVaKRgZGAA5XACQKnVtKgAAAA__")]
        public async Task TestContinuaListagemCte(ModeloDocumento modelo, long total, string token)
        {
            var res = await Cliente().ContinuaListagem(modelo, token);

            Assert.Equal(total, res.total);
        }
        # endregion

        # region Decisão PortariaNFe
        [Fact]
        public async Task TestDecisaoPortariaNFe()
        {
            string chave = "chave";
            var decisao = Decisao.Receber;
            string observacao = "Mensagem com uma observação";

            var res = await Cliente().DecisaoPortariaNFe(chave, decisao, observacao);

            Assert.Equal(chave, res.documento.chave);
        }
        # endregion

        # region Manifestação destinatário NFe
        [Fact]
        public async Task TestManifestacaoNfe()
        {
            var modelo = ModeloDocumento.Nfe;
            string id = "id";
            var manifestacao = Manifestacao.Confirmada;
            string justificativa = "obrigatória no caso de operacao_nao_realizada";

            var expect = System.Net.HttpStatusCode.OK;

            var res = await Cliente().ManifestacaoNfe(modelo, id, manifestacao, justificativa);

            Assert.Equal(expect, res);
        }
        # endregion

        # region EnviaDocumento
        [Fact]
        public async Task TestEnviaDocumento()
        {
            string xml = "";
            string terceiro_cnpj = "";

            string id = "";

            var res = await Cliente().EnviaDocumento(xml, terceiro_cnpj);

            Assert.Equal(id, res.documento.id);
        }
        # endregion

        # region SolicitaDownload
        [Theory()]
        [InlineData(ModeloDocumento.Nfe, FiltroDocumento.Emitidos, FormatoDocumento.Pdf, "")]
        public async Task TestSolicitaDownload(ModeloDocumento modelo, FiltroDocumento filtro, FormatoDocumento formato, string consulta)
        {
            var res = await Cliente().SolicitaDownload(modelo, filtro, formato, consulta);

            Assert.StartsWith("", res.id);
        }
        # endregion

        # region ConsultaDownloadSolicitado
        [Theory()]
        [InlineData("")]
        public async Task TestConsultaDownload(string id)
        {
            var res = await Cliente().ConsultaDownloadSolicitado(id);

            Assert.StartsWith("", res.id);
        }
        # endregion

        # region BaixaDocumentosNfe
        [Theory()]
        [InlineData(ModeloDocumento.Nfe, FormatoDocumento.Pdf, "43190743283811015939550010000139191316599936")]
        [InlineData(ModeloDocumento.Nfe, FormatoDocumento.Pdf, "5d3f9a69e0897d0001bdfcc4")]
        public async Task TestBaixaDocumentoNfePdfConteudo(ModeloDocumento modelo, FormatoDocumento formato, string chave)
        {
            var res = await Cliente().BaixaDocumento(modelo, formato, chave);

            Assert.StartsWith("%PDF", res.Conteudo);
        }

        [Theory()]
        [InlineData(ModeloDocumento.Nfe, FormatoDocumento.Pdf, "43190743283811015939550010000139191316599936")]
        [InlineData(ModeloDocumento.Nfe, FormatoDocumento.Pdf, "5d3f9a69e0897d0001bdfcc4")]
        public async Task TestBaixaDocumentoNfePdfSalva(ModeloDocumento modelo, FormatoDocumento formato, string chave)
        {
            var res = await Cliente().BaixaDocumento(modelo, formato, chave);
            var caminho = $"../../../tests/fixtures/temp/NfePdf.pdf";

            res.Salva(caminho);

            var result = File.ReadAllText(caminho);

            Assert.StartsWith("%PDF", result);
        }

        [Theory()]
        [InlineData(ModeloDocumento.Nfe, FormatoDocumento.Xml, "43190743283811015939550010000139191316599936")]
        [InlineData(ModeloDocumento.Nfe, FormatoDocumento.Xml, "5d3f9a69e0897d0001bdfcc4")]
        public async Task TestBaixaDocumentoNfeXmlConteudo(ModeloDocumento modelo, FormatoDocumento formato, string chave)
        {
            var res = await Cliente().BaixaDocumento(modelo, formato, chave);

            Assert.StartsWith("<nfeProc", res.Conteudo);
        }

        [Theory()]
        [InlineData(ModeloDocumento.Nfe, FormatoDocumento.Xml, "43190743283811015939550010000139191316599936")]
        [InlineData(ModeloDocumento.Nfe, FormatoDocumento.Xml, "5d3f9a69e0897d0001bdfcc4")]
        public async Task TestBaixaDocumentoNfeXmlSalva(ModeloDocumento modelo, FormatoDocumento formato, string chave)
        {
            var res = await Cliente().BaixaDocumento(modelo, formato, chave);
            var caminho = $"../../../tests/fixtures/temp/NfeXml.xml";

            res.Salva(caminho);

            var result = File.ReadAllText(caminho);
            var expect = File.ReadAllText($"../../../tests/fixtures/NfeXml-{chave}.xml");

            Assert.Equal(result, expect);
        }
        # endregion

        # region BaixaDocumentosCte
        [Theory()]
        [InlineData(ModeloDocumento.Cte, FormatoDocumento.Pdf, "35190843244631002102570040004301431833984987")]
        [InlineData(ModeloDocumento.Cte, FormatoDocumento.Pdf, "5d48fed3885f200001758e54")]
        public async Task TestBaixaDocumentoCtePdfConteudo(ModeloDocumento modelo, FormatoDocumento formato, string chave)
        {
            var res = await Cliente().BaixaDocumento(modelo, formato, chave);

            Assert.StartsWith("%PDF", res.Conteudo);
        }

        [Theory()]
        [InlineData(ModeloDocumento.Cte, FormatoDocumento.Pdf, "35190843244631002102570040004301431833984987")]
        [InlineData(ModeloDocumento.Cte, FormatoDocumento.Pdf, "5d48fed3885f200001758e54")]
        public async Task TestBaixaDocumentoCtePdfSalva(ModeloDocumento modelo, FormatoDocumento formato, string chave)
        {
            var res = await Cliente().BaixaDocumento(modelo, formato, chave);
            var caminho = $"../../../tests/fixtures/temp/CtePdf.pdf";

            res.Salva(caminho);

            var result = File.ReadAllText(caminho);

            Assert.StartsWith("%PDF", result);
        }

        [Theory()]
        [InlineData(ModeloDocumento.Cte, FormatoDocumento.Xml, "35190843244631002102570040004301431833984987")]
        [InlineData(ModeloDocumento.Cte, FormatoDocumento.Xml, "5d48fed3885f200001758e54")]
        public async Task TestBaixaDocumentoCteXmlConteudo(ModeloDocumento modelo, FormatoDocumento formato, string chave)
        {
            var res = await Cliente().BaixaDocumento(modelo, formato, chave);

            Assert.StartsWith("<cteProc", res.Conteudo);
        }

        [Theory()]
        [InlineData(ModeloDocumento.Cte, FormatoDocumento.Xml, "35190843244631002102570040004301431833984987")]
        [InlineData(ModeloDocumento.Cte, FormatoDocumento.Xml, "5d48fed3885f200001758e54")]
        public async Task TestBaixaDocumentoCteXmlSalva(ModeloDocumento modelo, FormatoDocumento formato, string chave)
        {
            var res = await Cliente().BaixaDocumento(modelo, formato, chave);
            var caminho = $"../../../tests/fixtures/temp/CteXml.xml";

            res.Salva(caminho);

            var result = File.ReadAllText(caminho);
            var expect = File.ReadAllText($"../../../tests/fixtures/CteXml-{chave}.xml");

            Assert.Equal(result, expect);
        }
        #endregion

        #region Testa DesserializarJson
        [Fact]
        public void TestDesserializarDadosDocumento()
        {
            string caminho = "../../../tests/fixtures/DocumentosJson/DadosDocumento.txt";
            using (BinaryReader reader = new BinaryReader(File.Open(caminho, FileMode.Open)))
            {
                var stream = reader.BaseStream;

                var res = JsonHandler<DadosDocumento>.Desserializar(stream);

                Assert.Equal("SP", res.dest_end_uf);
                Assert.Equal("1", res.ambiente_sefaz_id);
                Assert.Equal("551045bf706f725f36262201", res.historicos[0].id);
                Assert.Equal("7891308759621", res.itens[0].codigo);
            }
        }

        [Fact]
        public void TestDesserializarRootDocumento()
        {
            string caminho = "../../../tests/fixtures/DocumentosJson/RootDocumento.txt";
            using (BinaryReader reader = new BinaryReader(File.Open(caminho, FileMode.Open)))
            {
                var stream = reader.BaseStream;
                var res = JsonHandler<RootDocumento>.Desserializar(stream);

                Assert.Equal("53d2f08f9711f6abe20009e7", res.documento.id);
                Assert.Equal(1, res.documento.serie);
                Assert.Equal("111.4", res.documento.valor);
            }
        }

        [Fact]
        public void TestDesserializarSolicitaDownload()
        {
            string caminho = "../../../tests/fixtures/DocumentosJson/SolicitaDownload.txt";
            using (BinaryReader reader = new BinaryReader(File.Open(caminho, FileMode.Open)))
            {
                var stream = reader.BaseStream;
                var res = JsonHandler<SolicitaDownload>.Desserializar(stream);

                Assert.Equal("53eb0ce86661621e4e000000", res.id);
                Assert.Equal("xml", res.formato);
                Assert.Equal("nfe", res.tipo_documento);
            }
        }

        [Fact]
        public void TestDesserializarConsultaDownload()
        {
            string caminho = "../../../tests/fixtures/DocumentosJson/ConsultaDownload.txt";
            using (BinaryReader reader = new BinaryReader(File.Open(caminho, FileMode.Open)))
            {
                var stream = reader.BaseStream;

                var res = JsonHandler<ConsultaDownload>.Desserializar(stream);

                Assert.Equal("53d71e27666162088d040000", res.id);
                Assert.Equal("xml", res.formato);
                Assert.Equal("nfe", res.tipo_documento);
                Assert.Equal("http://download-consyste.s3.amazonaws.com/xmls_2014-07-29T0108.zip", res.arquivo);
            }
        }

        [Fact]
        public void TestDesserializarListagemDocumentos()
        {
            string caminho = "../../../tests/fixtures/DocumentosJson/ListagemDocumentos.txt";
            using (BinaryReader reader = new BinaryReader(File.Open(caminho, FileMode.Open)))
            {
                var stream = reader.BaseStream;

                var res = JsonHandler<ListagemDocumentos>.Desserializar(stream);

                Assert.Equal(3, res.total);
                Assert.Equal("c2NhbjswOzE7dG90YWxfaGl0czozOw==", res.proxima_pagina);
                Assert.Equal("53d2f08f9711f6abe20009e7", res.documentos[0].id);
                Assert.Equal(1, res.documentos[0].serie);
            }
        }
        # endregion

        Client Cliente()
        {
            Client c = new Client();
            return c;
        }
    }
}

