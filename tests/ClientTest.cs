using System;
using Xunit;

namespace Consyste.Clients.Portal {
public class TestClient {

        [Fact]
        public void TestContinuaListagem()
        {
            var modelo = "55";
            var token = "";

            var res = Cliente().ContinuaListagem(modelo, token).Result;

	        Assert.NotNull(res);
        }

        [Fact]
        public void TestListaDocumentos()
        {
            var modelo = "55";
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

