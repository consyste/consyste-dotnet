using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Consyste.Clients.Portal
{
    /// <summary>
    /// Esta classe é responsável pela manipulação do JSON.
    /// </summary>
    internal static class JsonHandler<T>
    {
        private static JsonSerializerSettings defaultJsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };

        static readonly JsonSerializer jsonSerializer = JsonSerializer.Create(defaultJsonSerializerSettings);

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

        /// <summary>
        /// Converte um Objeto para JSON. 
        /// </summary>
        public static string Serializar(T objeto)
        {
            return JsonConvert.SerializeObject(objeto, defaultJsonSerializerSettings);
        }

    }
}