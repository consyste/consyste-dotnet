using System;
using Xunit;

namespace Consyste.Clients.Portal {
public class TestClient {

        [Theory()]
        [InlineData(ModeloDocumento.Nfe)]
        [InlineData(ModeloDocumento.Cte)]
        public void TestContinuaListagem(ModeloDocumento modelo)
        {
            var token = "";

            var res = Cliente().ContinuaListagem(modelo, token).Result;
	        
            Assert.NotNull(res);
        }

        [Theory()]
        [InlineData(ModeloDocumento.Nfe)]
        [InlineData(ModeloDocumento.Cte)]
        public void TestListaDocumentos(ModeloDocumento modelo)
        {
            var filtro = "";
            
            var res = Cliente().ListaDocumentos(modelo, filtro).Result;
         
	        Assert.NotNull(res);
        }

        Client Cliente() {
            Client c = new Client();
            return c;
        }
    }
}

