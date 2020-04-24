using System;
using System.Net;
using System.IO;

namespace Consyste.Clients.Portal
{
    /// <summary>
    /// Esta classe é responsável pelo download dos documentos.
    /// </summary>
    public class Download
    {
        private readonly HttpWebResponse _res;

        public Download(HttpWebResponse res)
        {
            _res = res;
        }

        /// <summary>
        /// Salva o documento no local designado.
        /// </summary>
        /// <param name="caminho">O local onde o documento vai ser salvo, como, "Folder/Documento.xml".</param>
        public void Salva(string caminho)
        {
            using var stream = _res.GetResponseStream();
            if (stream == null)
                throw new ApplicationException("Requisição não retornou resposta");

            using var fileStream = File.Create(caminho);

            stream.CopyTo(fileStream);
        }

        /// <summary>
        /// Retorna o conteúdo do documento.
        /// </summary>
        public string Conteudo
        {
            get
            {
                using var stream = _res.GetResponseStream();
                if (stream == null)
                    throw new ApplicationException("Requisição não retornou resposta");

                using var reader = new StreamReader(stream);
                return reader.ReadToEnd();
            }
        }
    }
}