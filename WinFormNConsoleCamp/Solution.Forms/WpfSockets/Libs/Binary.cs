using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace WpfSockets.Libs
{
    public static class Binary
    {
        /// <summary>
        /// Convert an object to a Byte Array.
        /// </summary>
        public static byte[]? ObjectToByteArray<T>(T data) where T : class
        {
            if (data == null)
                return default;


            return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data, GetJsonSerializerOptions()));
        }

        /// <summary>
        /// Convert a byte array to an Object of T.
        /// </summary>
        public static T? ByteArrayToObject<T>(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
                return default;

            return JsonSerializer.Deserialize<T>(byteArray, GetJsonSerializerOptions());
        }

        private static JsonSerializerOptions? GetJsonSerializerOptions()
        {
            return new JsonSerializerOptions()
            {
                PropertyNamingPolicy = null,
                WriteIndented = true,
                AllowTrailingCommas = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            };
        }
    }
}
