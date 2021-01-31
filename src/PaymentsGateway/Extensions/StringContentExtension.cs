using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace PaymentsGateway.Extensions
{
    public static class StringContentExtension
    {
        public static StringContent ToJsonStringContent(this object obj)
        {
            return new StringContent(JsonSerializer.Serialize(obj), Encoding.UTF8, "application/json");
        }
    }
}