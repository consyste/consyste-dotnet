# Consyste.Clients

Implementação da biblioteca contendo os acessos à API da Consyste.

Para mais informações sobre a API, acesse a documentação: https://portal.consyste.com.br/doc/api/introducao

## Acessos à API implementados

* Envio de documentos
* Lista de Documentos
* Consulta a Documentos
* Download de Documento
* Solicitação de Download de Documentos
* Consulta de Download Solicitado
* Manifestação do Destinatário de NF-e
* Decisão Portaria de NF-e

## Exemplos de uso

### Utilizando C#

#### Inicializando o cliente

```c#
using Consyste.Clients.Portal;

var client = new Client("sua-chave-de-acesso");
```

#### Enviando um documento

```c#
// envia um documento de terceiros, e adiciona o CNPJ da empresa como interessado
await client.EnviaDocumento(conteudoXml, cnpj);
```

#### Buscando as chaves de todos os documentos emitidos nos últimos 7 dias

```c#
var lista = await client.ListaDocumentos(
    ModeloDocumento.Nfe, FiltroDocumento.Recebidos,
    new string[] { "chave" }, "recebido_em: >now-7d"
);

Console.WriteLine($"{lista.Total} documentos encontrados pela busca");
while (lista.Documentos.Count > 0) {
    foreach (var doc in lista.Documentos)
        Console.WriteLine("Chave: " + doc.Chave);
    lista = await client.ContinuaListagem(ModeloDocumento.Nfe, lista.ProximaPagina);
}
```

#### Consultando um documento

```c#
var doc = await client.ConsultaDocumento(ModeloDocumento.Nfe, "5d3f9a69e0897d0001bdfcc4");
Console.WriteLine(doc.Chave)
```

#### Download do XML de um documento

```c#
var chave = "43190743283811015939550010000139191316599936";
var doc = await client.BaixaDocumento(ModeloDocumento.Nfe, FormatoDocumento.XML, chave);
doc.Salva($"C:\\{chave}.xml")
```

#### Download do PDF (DANFE) de um documento

```c#
var chave = "43190743283811015939550010000139191316599936";
var doc = await client.BaixaDocumento(ModeloDocumento.Nfe, FormatoDocumento.PDF, chave);
doc.Salva($"C:\\{chave}.pdf");
```

#### Download de uma lista de documentos (formato ZIP)

```c#
var solicitacao = await client.SolicitaDownload(
    ModeloDocumento.Nfe,
    FiltroDocumento.Recebidos,
    FormatoDocumento.XML,
    "recebido_em: >now-7d"
);

var download = await client.ConsultaDownloadSolicitado(solicitacao.Id);
while (download.ConcluidoEm == null)
{
    Thread.Sleep(5000); // aguarda 5s entre as tentativas
    download = await client.ConsultaDownloadSolicitado(solicitacao.Id);
}
download.Salva("C:\\Notas.ZIP");
```

#### Manifestação do Destinatário de NF-e

```c#
await client.ManifestacaoNfe(ModeloDocumento.Nfe, "53d2f08f9711f6abe20009e7", Manifestacao.Confirmada);
```

#### Decisão Portaria de NF-e

```c#
await client.DecisaoPortariaNFe("35171107764744000121550010000041351402532164", Decisao.Receber, "Mensagem a adicionar como observação");
```

### Utilizando a biblioteca com PowerShell

No início do script, adicione a linha a seguir para acessar a os métodos de acesso à API da Consyste,
e criar o cliente com sua chave de acesso:

    Add-Type -Path "$PWD\Consyste.Clients\Consyste.Clients.dll"
    $ClientInstance = New-Object Consyste.Clients.Portal.Client("sua-chave-de-acesso")


#### Envio de documentos

    $Resposta = $ClientInstance.EnviaDocumento("<procNFe ...", "000000000").GetAwaiter().GetResult()
    $Resposta.Documento


#### Lista de Documentos

    [string[]]$Campos = 'chave' 
    $Res = $ClientInstance.ListaDocumentos("nfe", "recebidos", $Campos).GetAwaiter().GetResult()
    while ($Res.Documentos.Count -gt 0) {
        foreach ($Documento in $ListaDocumentos.Documentos) {
            # ...
        }

        $Res = $ClientInstance.ContinuaListagem("nfe", $Res.ProximaPagina).GetAwaiter().GetResult()
    }

#### Consulta a Documento

    $ConsultaDocumento = $ClientInstance.ConsultaDocumento("nfe", "5d3f9a69e0897d0001bdfcc4").GetAwaiter().GetResult()
    Write-Host $ConsultaDocumento.Chave


#### Download de XML

    $DownloadDocumento = $ClientInstance.BaixaDocumento("nfe", "xml", "43190743283811015939550010000139191316599936").GetAwaiter().GetResult()
    $DownloadDocumento.Conteudo
    $DownloadDocumento.Salva("43190743283811015939550010000139191316599936.xml")

#### Download de PDF (DANFE)

    $DownloadDocumento = $ClientInstance.BaixaDocumento("nfe", "pdf", "43190743283811015939550010000139191316599936").GetAwaiter().GetResult()
    $DownloadDocumento.Conteudo
    $DownloadDocumento.Salva("43190743283811015939550010000139191316599936.pdf")


#### Solicitação de Download de Documentos

    $SolicitaDownload = $ClientInstance.SolicitaDownload("nfe", "recebidos", "xml", "abc").GetAwaiter().GetResult()
    Write-Host $SolicitaDownload.Id


#### Consulta de Download Solicitado

    $ConsultaDownload = $ClientInstance.ConsultaDownloadSolicitado($SolicitaDownload.Id).GetAwaiter().GetResult()
    Write-Host $ConsultaDownload


#### Manifestação do Destinatário de NF-e

    $ClientInstance.ManifestacaoNfe("nfe", "53d2f08f9711f6abe20009e7", "confirmada").GetAwaiter().GetResult()


#### Decisão Portaria de NF-e

    $ClientInstance.DecisaoPortariaNFe("35171107764744000121550010000041351402532164", "receber", "Mensagem a adicionar como observação").GetAwaiter().GetResult()
