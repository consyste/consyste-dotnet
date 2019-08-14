using System;
using Xunit;
using Xunit.Abstractions;

namespace Consyste.Clients.Portal {
public class TestClient {

        private readonly ITestOutputHelper output;

        public TestClient(ITestOutputHelper output)
    {
        this.output = output;
    }

        [Theory()]
        //Nfe
        [InlineData(ModeloDocumento.Nfe, FiltroDocumento.Emitidos, 0, "H4sIAAhYVF0AA9PV1eXSVbBKsVJIzc0syUzJL+ZSULAqtFIAiRZnVqVaKRgZGAA5XACQKnVtKgAAAA__")]
        [InlineData(ModeloDocumento.Nfe, FiltroDocumento.Recebidos, 195, "H4sIAMdXVF0AAxXKPQ6DMAxA4T2nMOps1UQNFG9s3KLKjxEZmqgJDHD6hvF7eoioEDgwFPHiYshVAfCP4c41XsKgiRq6crjzaUuxJy+2blGY51vtRwjZH19Je-6kVR4mvN7OaaP1aGQkop6mQXqv-h8lQBtvAAAA")]
        [InlineData(ModeloDocumento.Nfe, FiltroDocumento.Todos, 195, "H4sIACFUVF0AAxXKTQ5AMBBA4X1PMWI9MRr1Mzs7t5BWR1jQaFlweiy-l4eICoE9wxl8SAqAD4Y-pfURBk30IYuXuwsbo715sGlZhbn-9f0IPkzXJvsZxn2W3PiqdU4brRsjDRGV1NVSTuoFIAdepWsAAAA_")]
        // Cte
        [InlineData(ModeloDocumento.Cte, FiltroDocumento.Emitidos, 0, "H4sIADRYVF0AA9PV1eXSVbBKsVJIzc0syUzJL+ZSULAqtFIAiRZnVqVaKRgZGAA5XACQKnVtKgAAAA__")]
        [InlineData(ModeloDocumento.Cte, FiltroDocumento.Tomados, 0, "H4sIANBYVF0AA9PV1eXSVbBKsVIoyc9NTMkv5lJQsCq0UgAJFmdWpVopGBkYADlcAPyX4W4pAAAA")]
        [InlineData(ModeloDocumento.Cte, FiltroDocumento.Todos, 19, "H4sIAO5YVF0AAxWLuw6AIAwAd76ixrkRH0TSzc2-MGhrdFAi4KBfL453uUNEhUBMkDz7qADoIvhV3F8haLTOUIR7fioXgntodHHbhWj4KfcI7Jf7kDP5aUlSGu7sKtxaa9Z8a133xorp1AfeUTzTawAAAA__")]
        public void TestListaDocumentos(ModeloDocumento modelo, FiltroDocumento filtro, long total, string proxPag)
        {   

            Campo [] campos = new Campo[13]  { Campo.Id, Campo.Numero, Campo.Chave, Campo.EmitidoEm, Campo.Serie, Campo.Numero, Campo.Valor, Campo.SituacaoCustodia,
            Campo.SituacaoSefaz, Campo.EmitenteCnpj, Campo.EmitenteNome, Campo.DestinatarioCnpj, Campo.DestinatarioNome};
            var consulta = "";

            var res = Cliente().ListaDocumentos(modelo, filtro, consulta, campos).Result;
            
            Assert.Equal(proxPag, res.ProximaPagina);
            Assert.Equal(total, res.Total);
        }

        [Theory()]
        [InlineData(ModeloDocumento.Nfe, 195, "H4sIACFUVF0AAxXKTQ5AMBBA4X1PMWI9MRr1Mzs7t5BWR1jQaFlweiy-l4eICoE9wxl8SAqAD4Y-pfURBk30IYuXuwsbo715sGlZhbn-9f0IPkzXJvsZxn2W3PiqdU4brRsjDRGV1NVSTuoFIAdepWsAAAA_")]
        [InlineData(ModeloDocumento.Cte, 19, "H4sIAHdUVF0AAxWLuw6AIAwAd76ixrkRH0TSzc2-MGhrdFAi4KBfL453uUNEhUBMkDz7qADoIvhV3F8haLTOUIR7fioXgntodHHbhWj4KfcI7Jf7kDP5aUlSGu7sKtxaa9Z8a133xorp1AfeUTzTawAAAA__")]
        public void TestContinuaListagem(ModeloDocumento modelo, long total, string token)
        {
            var res = Cliente().ContinuaListagem(modelo, token).Result;
	        
            Assert.Equal(total, res.Total);
        }

        [Theory()]
        // Nfe
        [InlineData(ModeloDocumento.Nfe, FormatoDocumento.Pdf, "43190743283811015939550010000139191316599936")]
        [InlineData(ModeloDocumento.Nfe, FormatoDocumento.Pdf, "5d3f9a69e0897d0001bdfcc4")]
        [InlineData(ModeloDocumento.Nfe, FormatoDocumento.Xml, "43190743283811015939550010000139191316599936")]
        [InlineData(ModeloDocumento.Nfe, FormatoDocumento.Xml, "5d3f9a69e0897d0001bdfcc4")]
        // Cte
        [InlineData(ModeloDocumento.Cte, FormatoDocumento.Pdf, "35190843244631002102570040004301431833984987")]
        [InlineData(ModeloDocumento.Cte, FormatoDocumento.Pdf, "5d48fed3885f200001758e54")]
        [InlineData(ModeloDocumento.Cte, FormatoDocumento.Xml, "35190843244631002102570040004301431833984987")]
        [InlineData(ModeloDocumento.Cte, FormatoDocumento.Xml, "5d48fed3885f200001758e54")]
        public void TestBaixaDocumento(ModeloDocumento modelo, FormatoDocumento formato, string chave)
        {      
            var res = Cliente().BaixaDocumento(modelo, formato, chave).Result;

            Assert.NotNull(res);
        }

        Client Cliente() {
            Client c = new Client();
            return c;
        }
    }
}

