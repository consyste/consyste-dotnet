using System.Net;
using System.IO;

namespace Consyste.Clients.Portal
{
    /// <summary>
    /// Esta classe é responsável pelo download dos documentos.
    /// </summary>
    public class Download
    {
        private HttpWebResponse res;

        public Download(HttpWebResponse res)
        {
            this.res = res;
        }
        /// <summary>
        /// Salva o documento no local designado.
        /// </summary>
        /// <param name="caminho">O local onde o documento vai ser salvo, como, "Folder/Documento.xml".</param>
        public void Salva(string caminho)
        {
            using (Stream stream = res.GetResponseStream())
            {
                using (FileStream fileStream = File.Create(caminho))
                {
                    stream.CopyTo(fileStream);
                }
            }
        }

        /// <summary>
        /// Retorna o conteúdo do documento.
        /// </summary>
        public string Conteudo
        {
            get
            {
                using (Stream stream = res.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }
    }
}