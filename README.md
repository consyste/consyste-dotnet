## consyste-dotnet
Implementação da biblioteca contendo os acessos à API da Consyste. 
Para mais informações sobre a API a documentação pode ser encontrada em https://portal.consyste.com.br/doc/api/introducao

## Acessos à API implementados
* Envio de documentos
* Lista de Documentos
* Consulta a Documentos
* Download de Documento
* Solicitação de Download de Documentos
* Consulta de Download Solicitado
* Manifestação do Destinatário de NF-e
* Decisão Portaria de NF-e


## Utilizando a biblioteca com PowerShell
Para carregar a DLL da biblioteca com os métodos de acesso à API da Consyste

    Add-Type -Path "$PWD\Consyste.Clients\Consyste.Clients.dll"


Instanciar a classe `Client` através da chave de acesso

    $ClientInstance = New-Object Consyste.Clients.Portal.Client("Yuom51QL22X_RNsJpG-z")


Exemplo de Envio de documentos e obtendo o Documento

    $EnviaDoc = $ClientInstance.EnviaDocumento("<procNFe ...", "000000000").GetAwaiter().GetResult()
    $EnviaDoc.Documento


Exemplo Lista de Documentos e obtendo a Lista dos Documentos

    $ListaDocumentos = $ClientInstance.ListaDocumentos("nfe", "recebidos").GetAwaiter().GetResult()
    $ListaDocumentos.Documentos


Exemplo de Continua Listagem e obtendo o Total

    $ClientInstance.ContinuaListagem("nfe", $ListaDocumentos.ProximaPagina).GetAwaiter().GetResult()
    $ContinuaListagem.Total


Consulta a Documento e obtendo a Chave

    $ConsultaDocumento = $ClientInstance.ConsultaDocumento("nfe", "5d3f9a69e0897d0001bdfcc4").GetAwaiter().GetResult()
    $ConsultaDocumento.Chave


Exemplo de Download, necessariamente nesta ordem: Download do Documento, Acesso ao Conteudo do Documento e Salvamento do Documento no disco 

    $DownloadDocumento = $ClientInstance.BaixaDocumento("nfe", "xml", "43190743283811015939550010000139191316599936").GetAwaiter().GetResult()
    $DownloadDocumento.Conteudo
    $DownloadDocumento.Salva("Pasta\Nfe.xml")


Solicitação de Download de Documentos e obtendo o Id

    $SolicitaDownload = $ClientInstance.SolicitaDownload("nfe", "recebidos", "xml", "abc").GetAwaiter().GetResult() 
    $SolicitaDownload.Id 


Consulta de Download Solicitado e obtendo o Id

    $ConsultaDownload = $ClientInstance.ConsultaDownloadSolicitado("53eb0ce86661621e4e000000").GetAwaiter().GetResult()
    $ConsultaDownload.Id


Manifestação do Destinatário de NF-e

    $ClientInstance.ManifestacaoNfe("nfe", "53d2f08f9711f6abe20009e7", "confirmada").GetAwaiter().GetResult()


Decisão Portaria de NF-e

    $DecisaoPortaria = $ClientInstance.DecisaoPortariaNFe("35171107764744000121550010000041351402532164", "receber", "Mensagem a adicionar como observação").GetAwaiter().GetResult()
    $DecisaoPortaria.Documento

