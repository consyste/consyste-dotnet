using System.IO;

using Newtonsoft.Json;

namespace Consyste.Clients.Portal
{
    public static class JsonHandler<T>
    {
        static readonly JsonSerializer jsonSerializer = JsonSerializer.CreateDefault();

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