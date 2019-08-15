using Xunit;
using Xunit.Abstractions;

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
		public async void TestListaDocumentosNfeTotal(ModeloDocumento modelo, FiltroDocumento filtro, long total)
        {   
            string [] campos = new string[2]  { "id", "numero"};

            var res = await Cliente().ListaDocumentos(modelo, filtro, campos);
            
            Assert.Equal(total, res.Total);
        }
		
		[Theory()]
        [InlineData(ModeloDocumento.Nfe, FiltroDocumento.Emitidos, 0)]
        [InlineData(ModeloDocumento.Nfe, FiltroDocumento.Recebidos, 0)]
        [InlineData(ModeloDocumento.Nfe, FiltroDocumento.Todos, 0)]
		public async void TestListaDocumentosNfeDocumentos(ModeloDocumento modelo, FiltroDocumento filtro, int documentos)
        {   
            string [] campos = new string[2]  { "id", "numero"};
            
            var consulta = "";

            var res = await Cliente().ListaDocumentos(modelo, filtro, campos, consulta);
            
            Assert.Equal(documentos, res.Documentos.Count);
        }

        [Theory()]
        [InlineData(ModeloDocumento.Nfe, FiltroDocumento.Emitidos, "H4sIAAhYVF0AA9PV1eXSVbBKsVJIzc0syUzJL+ZSULAqtFIAiRZnVqVaKRgZGAA5XACQKnVtKgAAAA__")]
        [InlineData(ModeloDocumento.Nfe, FiltroDocumento.Recebidos, "H4sIAMdXVF0AAxXKPQ6DMAxA4T2nMOps1UQNFG9s3KLKjxEZmqgJDHD6hvF7eoioEDgwFPHiYshVAfCP4c41XsKgiRq6crjzaUuxJy+2blGY51vtRwjZH19Je-6kVR4mvN7OaaP1aGQkop6mQXqv-h8lQBtvAAAA")]
        [InlineData(ModeloDocumento.Nfe, FiltroDocumento.Todos, "H4sIACFUVF0AAxXKTQ5AMBBA4X1PMWI9MRr1Mzs7t5BWR1jQaFlweiy-l4eICoE9wxl8SAqAD4Y-pfURBk30IYuXuwsbo715sGlZhbn-9f0IPkzXJvsZxn2W3PiqdU4brRsjDRGV1NVSTuoFIAdepWsAAAA_")]
		public async void TestListaDocumentosNfeProxPagina(ModeloDocumento modelo, FiltroDocumento filtro, string proxPag)
        {   
            var res = await Cliente().ListaDocumentos(modelo, filtro);
            
            Assert.Equal(proxPag, res.ProximaPagina);
        }
        #endregion

        # region ListaDocumentosCte
        [Theory()]
        [InlineData(ModeloDocumento.Cte, FiltroDocumento.Emitidos, 0)]
        [InlineData(ModeloDocumento.Cte, FiltroDocumento.Tomados, 0)]
        [InlineData(ModeloDocumento.Cte, FiltroDocumento.Todos, 19)]
        public async void TestListaDocumentosCteTotal(ModeloDocumento modelo, FiltroDocumento filtro, long total)
        {   
            string [] campos = new string[2]  {"toma_cnpj", "toma_nome"};
            
			var consulta = "";

            var res = await Cliente().ListaDocumentos(modelo, filtro, campos, consulta);
            
            Assert.Equal(total, res.Total);
        }
		
		[Theory()]
        [InlineData(ModeloDocumento.Cte, FiltroDocumento.Emitidos, 0)]
        [InlineData(ModeloDocumento.Cte, FiltroDocumento.Tomados, 0)]
        [InlineData(ModeloDocumento.Cte, FiltroDocumento.Todos, 0)]
        public async void TestListaDocumentosCteDocumentos(ModeloDocumento modelo, FiltroDocumento filtro, int documentos)
        {   

            string [] campos = new string[2]  {"toma_cnpj", "toma_nome"};
            var consulta = "";

            var res = await Cliente().ListaDocumentos(modelo, filtro, campos, consulta);
            
            Assert.Equal(documentos, res.Documentos.Count);
        }

        [Theory()]
        [InlineData(ModeloDocumento.Cte, FiltroDocumento.Emitidos, "H4sIADRYVF0AA9PV1eXSVbBKsVJIzc0syUzJL+ZSULAqtFIAiRZnVqVaKRgZGAA5XACQKnVtKgAAAA__")]
        [InlineData(ModeloDocumento.Cte, FiltroDocumento.Tomados, "H4sIANBYVF0AA9PV1eXSVbBKsVIoyc9NTMkv5lJQsCq0UgAJFmdWpVopGBkYADlcAPyX4W4pAAAA")]
        [InlineData(ModeloDocumento.Cte, FiltroDocumento.Todos, "H4sIAO5YVF0AAxWLuw6AIAwAd76ixrkRH0TSzc2-MGhrdFAi4KBfL453uUNEhUBMkDz7qADoIvhV3F8haLTOUIR7fioXgntodHHbhWj4KfcI7Jf7kDP5aUlSGu7sKtxaa9Z8a133xorp1AfeUTzTawAAAA__")]
        public async void TestListaDocumentosCteProxPagina(ModeloDocumento modelo, FiltroDocumento filtro, string proxPag)
        {   
            var res = await Cliente().ListaDocumentos(modelo, filtro);
            
            Assert.Equal(proxPag, res.ProximaPagina);
        }
        #endregion

        # region ContinuaListagemNfe
        [Theory()]
        [InlineData(ModeloDocumento.Nfe, 195, "H4sIACFUVF0AAxXKTQ5AMBBA4X1PMWI9MRr1Mzs7t5BWR1jQaFlweiy-l4eICoE9wxl8SAqAD4Y-pfURBk30IYuXuwsbo715sGlZhbn-9f0IPkzXJvsZxn2W3PiqdU4brRsjDRGV1NVSTuoFIAdepWsAAAA_")]
        public async void TestContinuaListagemNfe(ModeloDocumento modelo, long total, string token)
        {
            var res = await Cliente().ContinuaListagem(modelo, token);
	        
            Assert.Equal(total, res.Total);
        }
		# endregion
        
        # region ContinuaListagemCte
		[Theory()]
        [InlineData(ModeloDocumento.Cte, 19, "H4sIAHdUVF0AAxWLuw6AIAwAd76ixrkRH0TSzc2-MGhrdFAi4KBfL453uUNEhUBMkDz7qADoIvhV3F8haLTOUIR7fioXgntodHHbhWj4KfcI7Jf7kDP5aUlSGu7sKtxaa9Z8a133xorp1AfeUTzTawAAAA__")]
        public async void TestContinuaListagemCte(ModeloDocumento modelo, long total, string token)
        {
            var res = await Cliente().ContinuaListagem(modelo, token);
	        
            Assert.Equal(total, res.Total);
        }
        #endregion

        # region BaixaDocumentosNfe
        [Theory()]
        [InlineData(ModeloDocumento.Nfe, FormatoDocumento.Pdf, "43190743283811015939550010000139191316599936")]
        [InlineData(ModeloDocumento.Nfe, FormatoDocumento.Pdf, "5d3f9a69e0897d0001bdfcc4")]
        public async void TestBaixaDocumentoNfePdfConteudo(ModeloDocumento modelo, FormatoDocumento formato, string chave)
        {      
            var res = await Cliente().BaixaDocumento(modelo, formato, chave);
            Assert.StartsWith("%PDF", res.Conteudo);
        }

        [Theory()]
        [InlineData(ModeloDocumento.Nfe, FormatoDocumento.Pdf, "43190743283811015939550010000139191316599936")]
        [InlineData(ModeloDocumento.Nfe, FormatoDocumento.Pdf, "5d3f9a69e0897d0001bdfcc4")]
        public async void TestBaixaDocumentoNfePdfSalva(ModeloDocumento modelo, FormatoDocumento formato, string chave)
        {      
            var res = await Cliente().BaixaDocumento(modelo, formato, chave);
            res.Salva($"../../../tests/fixtures/temp/NfePdf.pdf");
        }
		
		[Theory()]
		[InlineData(ModeloDocumento.Nfe, FormatoDocumento.Xml, "43190743283811015939550010000139191316599936")]
        [InlineData(ModeloDocumento.Nfe, FormatoDocumento.Xml, "5d3f9a69e0897d0001bdfcc4")]
        public async void TestBaixaDocumentoNfeXmlConteudo(ModeloDocumento modelo, FormatoDocumento formato, string chave)
        {      
            var res = await Cliente().BaixaDocumento(modelo, formato, chave);

            Assert.StartsWith("<nfeProc", res.Conteudo);
        }
		
		[Theory()]
		[InlineData(ModeloDocumento.Nfe, FormatoDocumento.Xml, "43190743283811015939550010000139191316599936")]
        [InlineData(ModeloDocumento.Nfe, FormatoDocumento.Xml, "5d3f9a69e0897d0001bdfcc4")]
        public async void TestBaixaDocumentoNfeXmlSalva(ModeloDocumento modelo, FormatoDocumento formato, string chave)
        {      
            var res = await Cliente().BaixaDocumento(modelo, formato, chave);

            res.Salva($"../../../tests/fixtures/temp/NfeXml.xml");
        }
        # endregion
		
		# region BaixaDocumentosCte
        [Theory()]
        [InlineData(ModeloDocumento.Cte, FormatoDocumento.Pdf, "35190843244631002102570040004301431833984987")]
        [InlineData(ModeloDocumento.Cte, FormatoDocumento.Pdf, "5d48fed3885f200001758e54")]
        public async void TestBaixaDocumentoCtePdfConteudo(ModeloDocumento modelo, FormatoDocumento formato, string chave)
        {      
            var res = await Cliente().BaixaDocumento(modelo, formato, chave);
			
			Assert.StartsWith("%PDF", res.Conteudo);
        }
		
		[Theory()]
        [InlineData(ModeloDocumento.Cte, FormatoDocumento.Pdf, "35190843244631002102570040004301431833984987")]
        [InlineData(ModeloDocumento.Cte, FormatoDocumento.Pdf, "5d48fed3885f200001758e54")]
        public async void TestBaixaDocumentoCtePdfSalva(ModeloDocumento modelo, FormatoDocumento formato, string chave)
        {      
            var res = await Cliente().BaixaDocumento(modelo, formato, chave);
			
			res.Salva($"../../../tests/fixtures/temp/CtePdf.pdf");
            
        }
		
		[Theory()]
        [InlineData(ModeloDocumento.Cte, FormatoDocumento.Xml, "35190843244631002102570040004301431833984987")]
        [InlineData(ModeloDocumento.Cte, FormatoDocumento.Xml, "5d48fed3885f200001758e54")]
        public async void TestBaixaDocumentoCteXmlConteudo(ModeloDocumento modelo, FormatoDocumento formato, string chave)
        {      
            var res = await Cliente().BaixaDocumento(modelo, formato, chave);
			
			Assert.StartsWith("<cteProc", res.Conteudo);
        }
		
		[Theory()]
        [InlineData(ModeloDocumento.Cte, FormatoDocumento.Xml, "35190843244631002102570040004301431833984987")]
        [InlineData(ModeloDocumento.Cte, FormatoDocumento.Xml, "5d48fed3885f200001758e54")]
        public async void TestBaixaDocumentoCteXmlSalva(ModeloDocumento modelo, FormatoDocumento formato, string chave)
        {      
            var res = await Cliente().BaixaDocumento(modelo, formato, chave);
            
            res.Salva($"../../../tests/fixtures/temp/CteXml.xml");
        }
        #endregion
        
        Client Cliente() {
            Client c = new Client();
            return c;
        }
    }
}

