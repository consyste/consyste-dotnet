using System.IO;
using System.Runtime.CompilerServices;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

[assembly: InternalsVisibleTo("Consyste.Clients.Tests")]

namespace Consyste.Clients.Portal
{
    /// <summary>
    /// Esta classe é responsável pela manipulação do JSON.
    /// </summary>
    internal static class JsonHandler
    {
        private static readonly JsonSerializerSettings DefaultJsonSerializerSettings =
            new JsonSerializerSettings { ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() } };

        private static readonly JsonSerializer JsonSerializer = JsonSerializer.Create(DefaultJsonSerializerSettings);

        /// <summary>
        /// Converte o JSON em um objeto. 
        /// </summary>
        public static T Desserializar<T>(Stream stream)
        {
            using var sr = new StreamReader(stream);
            using var jr = new JsonTextReader(sr);

            return JsonSerializer.Deserialize<T>(jr);
        }

        /// <summary>
        /// Converte um Objeto para JSON. 
        /// </summary>
        public static string Serializar<T>(T objeto)
        {
            return JsonConvert.SerializeObject(objeto, DefaultJsonSerializerSettings);
        }
    }
}