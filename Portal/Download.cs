using System.Net;
using System.IO;

namespace Consyste.Clients.Portal
{
    public class Download
    {
        private HttpWebResponse res;

        public Download(HttpWebResponse res)
        {
            this.res = res;
        }

        public void Salva(string caminho)
        {   
            Stream stream = res.GetResponseStream();
            FileStream fileStream = File.Create(caminho);
            stream.CopyTo(fileStream);
        }

        public string Conteudo
        {
            get
            { 
                Stream stream = res.GetResponseStream(); 
                StreamReader reader = new StreamReader(stream);
                string text = reader.ReadToEnd();
                return text;
            }
        }
    }
}