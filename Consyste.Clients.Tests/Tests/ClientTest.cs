using System.IO;
using System.Threading.Tasks;

using Consyste.Clients.Portal;

using Xunit;

namespace Consyste.Clients.Tests
{
    public class TestClient
    {
        private readonly Client _cliente = new Client();

        # region ListaDocumentosNfe

        [Theory]
        [InlineData(ModeloDocumento.Nfe, FiltroDocumento.Emitidos, 0)]
        [InlineData(ModeloDocumento.Nfe, FiltroDocumento.Recebidos, 195)]
        [InlineData(ModeloDocumento.Nfe, FiltroDocumento.Todos, 195)]
        public async Task TestListaDocumentosNfeTotal(ModeloDocumento modelo, FiltroDocumento filtro, long total)
        {
            var res = await _cliente.ListaDocumentos(modelo, filtro);

            Assert.Equal(total, res.Total);
        }

        [Theory]
        [InlineData(ModeloDocumento.Nfe, FiltroDocumento.Emitidos, 0)]
        [InlineData(ModeloDocumento.Nfe, FiltroDocumento.Recebidos, 68)]
        [InlineData(ModeloDocumento.Nfe, FiltroDocumento.Todos, 68)]
        public async void TestListaDocumentosNfeDocumentos(ModeloDocumento modelo, FiltroDocumento filtro, int documentosCount)
        {
            var res = await _cliente.ListaDocumentos(modelo, filtro, new[] { "dest_nome", "emit_nome" }, "Kalunga");

            Assert.Equal(documentosCount, res.Documentos.Count);
        }

        # endregion

        # region ListaDocumentosCte

        [Theory]
        [InlineData(ModeloDocumento.Cte, FiltroDocumento.Emitidos, 0)]
        [InlineData(ModeloDocumento.Cte, FiltroDocumento.Tomados, 0)]
        [InlineData(ModeloDocumento.Cte, FiltroDocumento.Todos, 19)]
        public async Task TestListaDocumentosCteTotal(ModeloDocumento modelo, FiltroDocumento filtro, long total)
        {
            var res = await _cliente.ListaDocumentos(modelo, filtro);

            Assert.Equal(total, res.Total);
        }

        [Theory]
        [InlineData(ModeloDocumento.Cte, FiltroDocumento.Emitidos, 0)]
        [InlineData(ModeloDocumento.Cte, FiltroDocumento.Tomados, 0)]
        [InlineData(ModeloDocumento.Cte, FiltroDocumento.Todos, 19)]
        public async void TestListaDocumentosCteDocumentos(ModeloDocumento modelo, FiltroDocumento filtro, int documentosCount)
        {
            var res = await _cliente.ListaDocumentos(modelo, filtro, new[] { "toma_cnpj", "toma_nome" });

            Assert.Equal(documentosCount, res.Documentos.Count);
        }

        # endregion

        # region ContinuaListagemNfe

        [Theory]
        [InlineData(ModeloDocumento.Nfe, 195,
                    "H4sIACFUVF0AAxXKTQ5AMBBA4X1PMWI9MRr1Mzs7t5BWR1jQaFlweiy-l4eICoE9wxl8SAqAD4Y-pfURBk30IYuXuwsbo715sGlZhbn-9f0IPkzXJvsZxn2W3PiqdU4brRsjDRGV1NVSTuoFIAdepWsAAAA_")]
        [InlineData(ModeloDocumento.Nfe, 0, "H4sIANYrXV0AA9PV1eXSVbBKsVJIzc0syUzJL+ZSULAqtFIAiRZnVqVaKRgZGAA5XACQKnVtKgAAAA__")]
        public async Task TestContinuaListagemNfe(ModeloDocumento modelo, long total, string token)
        {
            var res = await _cliente.ContinuaListagem(modelo, token);

            Assert.Equal(total, res.Total);
        }

        # endregion

        # region ContinuaListagemCte

        [Theory]
        [InlineData(ModeloDocumento.Cte, 19,
                    "H4sIAHdUVF0AAxWLuw6AIAwAd76ixrkRH0TSzc2-MGhrdFAi4KBfL453uUNEhUBMkDz7qADoIvhV3F8haLTOUIR7fioXgntodHHbhWj4KfcI7Jf7kDP5aUlSGu7sKtxaa9Z8a133xorp1AfeUTzTawAAAA__")]
        [InlineData(ModeloDocumento.Cte, 0, "H4sIAKwvXV0AA9PV1eXSVbBKsVJIzc0syUzJL+ZSULAqtFIAiRZnVqVaKRgZGAA5XACQKnVtKgAAAA__")]
        public async Task TestContinuaListagemCte(ModeloDocumento modelo, long total, string token)
        {
            var res = await _cliente.ContinuaListagem(modelo, token);

            Assert.Equal(total, res.Total);
        }

        # endregion

        # region ConsultaDocumentoNfe

        [Theory]
        [InlineData(ModeloDocumento.Nfe, "43190743283811015939550010000139191316599936")]
        [InlineData(ModeloDocumento.Nfe, "5d3f9a69e0897d0001bdfcc4")]
        public async Task TestConsultaDocumentoNfe(ModeloDocumento modelo, string id)
        {
            var res = await _cliente.ConsultaDocumento(modelo, id);

            Assert.Equal("Kalunga Comercio Industria Grafica Ltda", res.EmitNome);
        }

        # endregion

        # region ConsultaDocumentoCte

        [Theory]
        [InlineData(ModeloDocumento.Cte, "35190843244631002102570040004301431833984987")]
        [InlineData(ModeloDocumento.Cte, "5d48fed3885f200001758e54")]
        public async Task TestConsultaDocumentoCte(ModeloDocumento modelo, string id)
        {
            var res = await _cliente.ConsultaDocumento(modelo, id);

            Assert.Equal("DELL COMPUTADORES DO BRASIL LTDA", res.TomaNome);
        }

        # endregion

        # region BaixaDocumentosNfe

        [Theory]
        [InlineData(ModeloDocumento.Nfe, FormatoDocumento.PDF, "43190743283811015939550010000139191316599936")]
        [InlineData(ModeloDocumento.Nfe, FormatoDocumento.PDF, "5d3f9a69e0897d0001bdfcc4")]
        public async Task TestBaixaDocumentoNfePdfConteudo(ModeloDocumento modelo, FormatoDocumento formato, string chave)
        {
            var res = await _cliente.BaixaDocumento(modelo, formato, chave);

            Assert.StartsWith("%PDF", res.Conteudo);
        }

        [Theory]
        [InlineData(ModeloDocumento.Nfe, FormatoDocumento.PDF, "43190743283811015939550010000139191316599936")]
        [InlineData(ModeloDocumento.Nfe, FormatoDocumento.PDF, "5d3f9a69e0897d0001bdfcc4")]
        public async Task TestBaixaDocumentoNfePdfSalva(ModeloDocumento modelo, FormatoDocumento formato, string chave)
        {
            var res = await _cliente.BaixaDocumento(modelo, formato, chave);
            const string caminho = "../../../tests/fixtures/temp/NfePdf.pdf";

            res.Salva(caminho);

            var result = await File.ReadAllTextAsync(caminho);

            Assert.StartsWith("%PDF", result);
        }

        [Theory]
        [InlineData(ModeloDocumento.Nfe, FormatoDocumento.XML, "43190743283811015939550010000139191316599936")]
        [InlineData(ModeloDocumento.Nfe, FormatoDocumento.XML, "5d3f9a69e0897d0001bdfcc4")]
        public async Task TestBaixaDocumentoNfeXmlConteudo(ModeloDocumento modelo, FormatoDocumento formato, string chave)
        {
            var res = await _cliente.BaixaDocumento(modelo, formato, chave);

            Assert.StartsWith("<nfeProc", res.Conteudo);
        }

        [Theory]
        [InlineData(ModeloDocumento.Nfe, FormatoDocumento.XML, "43190743283811015939550010000139191316599936")]
        [InlineData(ModeloDocumento.Nfe, FormatoDocumento.XML, "5d3f9a69e0897d0001bdfcc4")]
        public async Task TestBaixaDocumentoNfeXmlSalva(ModeloDocumento modelo, FormatoDocumento formato, string chave)
        {
            var res = await _cliente.BaixaDocumento(modelo, formato, chave);
            const string caminho = "../../../tests/fixtures/temp/NfeXml.xml";

            res.Salva(caminho);

            var result = await File.ReadAllTextAsync(caminho);
            var expect = await File.ReadAllTextAsync($"../../../tests/fixtures/NfeXml-{chave}.xml");

            Assert.Equal(result, expect);
        }

        # endregion

        # region BaixaDocumentosCte

        [Theory]
        [InlineData(ModeloDocumento.Cte, FormatoDocumento.PDF, "35190843244631002102570040004301431833984987")]
        [InlineData(ModeloDocumento.Cte, FormatoDocumento.PDF, "5d48fed3885f200001758e54")]
        public async Task TestBaixaDocumentoCtePdfConteudo(ModeloDocumento modelo, FormatoDocumento formato, string chave)
        {
            var res = await _cliente.BaixaDocumento(modelo, formato, chave);

            Assert.StartsWith("%PDF", res.Conteudo);
        }

        [Theory]
        [InlineData(ModeloDocumento.Cte, FormatoDocumento.PDF, "35190843244631002102570040004301431833984987")]
        [InlineData(ModeloDocumento.Cte, FormatoDocumento.PDF, "5d48fed3885f200001758e54")]
        public async Task TestBaixaDocumentoCtePdfSalva(ModeloDocumento modelo, FormatoDocumento formato, string chave)
        {
            var res = await _cliente.BaixaDocumento(modelo, formato, chave);
            const string caminho = "../../../tests/fixtures/temp/CtePdf.pdf";

            res.Salva(caminho);

            var result = await File.ReadAllTextAsync(caminho);

            Assert.StartsWith("%PDF", result);
        }

        [Theory]
        [InlineData(ModeloDocumento.Cte, FormatoDocumento.XML, "35190843244631002102570040004301431833984987")]
        [InlineData(ModeloDocumento.Cte, FormatoDocumento.XML, "5d48fed3885f200001758e54")]
        public async Task TestBaixaDocumentoCteXmlConteudo(ModeloDocumento modelo, FormatoDocumento formato, string chave)
        {
            var res = await _cliente.BaixaDocumento(modelo, formato, chave);

            Assert.StartsWith("<cteProc", res.Conteudo);
        }

        [Theory]
        [InlineData(ModeloDocumento.Cte, FormatoDocumento.XML, "35190843244631002102570040004301431833984987")]
        [InlineData(ModeloDocumento.Cte, FormatoDocumento.XML, "5d48fed3885f200001758e54")]
        public async Task TestBaixaDocumentoCteXmlSalva(ModeloDocumento modelo, FormatoDocumento formato, string chave)
        {
            var res = await _cliente.BaixaDocumento(modelo, formato, chave);
            const string caminho = "../../../tests/fixtures/temp/CteXml.xml";

            res.Salva(caminho);

            var result = await File.ReadAllTextAsync(caminho);
            var expect = await File.ReadAllTextAsync($"../../../tests/fixtures/CteXml-{chave}.xml");

            Assert.Equal(result, expect);
        }

        #endregion

        # region Testa DesserializarJson

        [Fact]
        public void TestDesserializarConsultaDocumento()
        {
            const string caminho = "../../../tests/fixtures/DocumentosJson/ConsultaDocumento.txt";

            using var reader = new BinaryReader(File.Open(caminho, FileMode.Open));
            var stream = reader.BaseStream;

            var res = JsonHandler.Desserializar<Documento>(stream);

            Assert.Equal("72381189001001", res.TomaCnpj);
            Assert.Equal("DELL COMPUTADORES DO BRASIL LTDA", res.TomaNome);
            Assert.Equal("ok", res.SituacaoCustodia);
        }

        [Fact]
        public void TestDesserializarConsultaDownload()
        {
            const string caminho = "../../../tests/fixtures/DocumentosJson/ConsultaDownload.txt";
            using var reader = new BinaryReader(File.Open(caminho, FileMode.Open));
            var stream = reader.BaseStream;

            var res = JsonHandler.Desserializar<ConsultaDownload>(stream);

            Assert.Equal("53d71e27666162088d040000", res.Id);
            Assert.Equal("xml", res.Formato);
            Assert.Equal("nfe", res.TipoDocumento);
            Assert.Equal("http://download-consyste.s3.amazonaws.com/xmls_2014-07-29T0108.zip", res.Arquivo);
        }

        [Fact]
        public void TestDesserializarListagemDocumentos()
        {
            const string caminho = "../../../tests/fixtures/DocumentosJson/ListagemDocumentos.txt";
            using var reader = new BinaryReader(File.Open(caminho, FileMode.Open));
            var stream = reader.BaseStream;

            var res = JsonHandler.Desserializar<ListagemDocumentos>(stream);

            Assert.Equal(3, res.Total);
            Assert.Equal("c2NhbjswOzE7dG90YWxfaGl0czozOw==", res.ProximaPagina);
            Assert.Equal("53d2f08f9711f6abe20009e7", res.Documentos[0].Id);
            Assert.Equal(1, res.Documentos[0].Serie);
        }

        [Fact]
        public void TestDesserializarRootDocumento()
        {
            const string caminho = "../../../tests/fixtures/DocumentosJson/RootDocumento.txt";
            using var reader = new BinaryReader(File.Open(caminho, FileMode.Open));

            var stream = reader.BaseStream;
            var res = JsonHandler.Desserializar<RootDocumento>(stream);

            Assert.Equal("53d2f08f9711f6abe20009e7", res.Documento.Id);
            Assert.Equal(1, res.Documento.Serie);
            Assert.Equal("111.4", res.Documento.Valor);
        }

        [Fact]
        public void TestDesserializarSolicitaDownload()
        {
            const string caminho = "../../../tests/fixtures/DocumentosJson/SolicitaDownload.txt";
            using var reader = new BinaryReader(File.Open(caminho, FileMode.Open));

            var stream = reader.BaseStream;
            var res = JsonHandler.Desserializar<SolicitaDownload>(stream);

            Assert.Equal("53eb0ce86661621e4e000000", res.Id);
            Assert.Equal("xml", res.Formato);
            Assert.Equal("nfe", res.TipoDocumento);
        }

        # endregion
    }
}