using System.IO;

using Newtonsoft.Json;

namespace Consyste.Clients.Portal
{   
    /// <summary>
    /// Esta classe é responsável por manipular o JSON recebido da API.
    /// </summary>
    public static class JsonHandler<T>
    {
        static readonly JsonSerializer jsonSerializer = JsonSerializer.CreateDefault();
        /// <summary>
        /// Converte o JSON em um objeto. 
        /// </summary>
        public static T Desserializar(Stream stream)
        {
            using (var sr = new StreamReader(stream))
            {
                using (var jr = new JsonTextReader(sr))
                {
                    return jsonSerializer.Deserialize<T>(jr);
                }
            }
        }
    }
}