using Xunit;
using System.Threading.Tasks;


namespace Consyste.Clients.Portal
{
    public class TestClient {

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
            string [] campos = new string[2]  { "id", "chave" };
            var consulta = "";
            
            var res = await Cliente().ListaDocumentos(modelo, filtro, null, consulta);
            
            Assert.Equal(total, res.Total);
        }

        // Lista Documentos Nfe
        [Theory()]
        [InlineData(ModeloDocumento.Nfe, FiltroDocumento.Emitidos, "H4sIADCtVl0AA9PV1eXSVbBKsVJIzc0syUzJL+ZSULAqtFJQVweJF2dWpVopGBkYADlcAKThI+0sAAAA")]
        [InlineData(ModeloDocumento.Nfe, FiltroDocumento.Recebidos, "H4sIAHWtVl0AAxXLOQ6DMBBA0d6nmIiCapTBilmmS8ctIi+DcAEWNhRw+kD59PURUSFwYMjixcWQigLgjaGun1DiJQya6MYrH+5825ztyaMtcxTm76P7QAjJH4use-qtk1QmfHrntNG6M9IRUUNDK41XfyolPERxAAAA")]
        [InlineData(ModeloDocumento.Nfe, FiltroDocumento.Todos, "H4sIAAmkVl0AAxXKTQ5AMBBA4X1PMWI9MRr1Mzs7t5BWR1jQaFlweiy-l4eICoE9wxl8SAqAD4Y-pfURBk30IYuXuwsbo715sGlZhbn-9f0IPkzXJvsZxn2W3PiqdU4brRsjDRGV1NVSTuoFIAdepWsAAAA_")]
		public async Task TestListaDocumentosNfeProxPagina(ModeloDocumento modelo, FiltroDocumento filtro, string proxPag)
        {   
            var res = await Cliente().ListaDocumentos(modelo, filtro);
            
            Assert.Equal(proxPag, res.ProximaPagina);
        }
        # endregion

        # region ListaDocumentosCte
        [Theory()]
        [InlineData(ModeloDocumento.Cte, FiltroDocumento.Emitidos, 0)]
        [InlineData(ModeloDocumento.Cte, FiltroDocumento.Tomados, 0)]
        [InlineData(ModeloDocumento.Cte, FiltroDocumento.Todos, 19)]
        public async Task TestListaDocumentosCteTotal(ModeloDocumento modelo, FiltroDocumento filtro, long total)
        {   
            string [] campos = new string[2]  {"toma_cnpj", "toma_nome"};
            
			var consulta = "";

            var res = await Cliente().ListaDocumentos(modelo, filtro, campos, consulta);
            
            Assert.Equal(total, res.Total);
        }

        // Lista Documentos Cte
        [Theory()]
        [InlineData(ModeloDocumento.Cte, FiltroDocumento.Emitidos, "H4sIAEmuVl0AA9PV1eXSVbBKsVJIzc0syUzJL+ZSULAqtFJQVweJF2dWpVopGBkYADlcAKThI+0sAAAA")]
        [InlineData(ModeloDocumento.Cte, FiltroDocumento.Tomados, "H4sIAHeuVl0AA9PV1eXSVbBKsVIoyc9NTMkv5lJQsCq0UlBXBwkXZ1alWikYGRgAOVwAqgm7kSsAAAA_")]
        [InlineData(ModeloDocumento.Cte, FiltroDocumento.Todos, "H4sIAMGtVl0AAxXLMQ6EIBBA0Z5TjLGwmjgSWd3p7LyFARmzFkoWsNDTq+X7yUdEhcCeIQcfkgLgP0NVvTGtlzBoogdFPNxZ2xjtyaNNv1WYh1fPgeDDfGyy5zDti5TGt71z2mjdGemIqKHvR5pZ3ezwgsltAAAA")]
        public async Task TestListaDocumentosCteProxPagina(ModeloDocumento modelo, FiltroDocumento filtro, string proxPag)
        {   
            var res = await Cliente().ListaDocumentos(modelo, filtro);
            
            Assert.Equal(proxPag, res.ProximaPagina);
        }
        # endregion

        # region ContinuaListagemNfe
        [Theory()]
        [InlineData(ModeloDocumento.Nfe, 195, "H4sIACFUVF0AAxXKTQ5AMBBA4X1PMWI9MRr1Mzs7t5BWR1jQaFlweiy-l4eICoE9wxl8SAqAD4Y-pfURBk30IYuXuwsbo715sGlZhbn-9f0IPkzXJvsZxn2W3PiqdU4brRsjDRGV1NVSTuoFIAdepWsAAAA_")]
        public async Task TestContinuaListagemNfe(ModeloDocumento modelo, long total, string token)
        {
            var res = await Cliente().ContinuaListagem(modelo, token);
	        
            Assert.Equal(total, res.Total);
        }
		# endregion
        
        # region ContinuaListagemCte
		[Theory()]
        [InlineData(ModeloDocumento.Cte, 19, "H4sIAHdUVF0AAxWLuw6AIAwAd76ixrkRH0TSzc2-MGhrdFAi4KBfL453uUNEhUBMkDz7qADoIvhV3F8haLTOUIR7fioXgntodHHbhWj4KfcI7Jf7kDP5aUlSGu7sKtxaa9Z8a133xorp1AfeUTzTawAAAA__")]
        public async Task TestContinuaListagemCte(ModeloDocumento modelo, long total, string token)
        {
            var res = await Cliente().ContinuaListagem(modelo, token);
            
            Assert.Equal(total, res.Total);
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
            
            res.Salva($"../../../tests/fixtures/temp/NfePdf.pdf");
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

            res.Salva($"../../../tests/fixtures/temp/NfeXml.xml");
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
			
		   res.Salva($"../../../tests/fixtures/temp/CtePdf.pdf");
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
            
            res.Salva($"../../../tests/fixtures/temp/CteXml.xml");
        }
        # endregion
        
        Client Cliente() {
            Client c = new Client();
            return c;
        }
    }
}

