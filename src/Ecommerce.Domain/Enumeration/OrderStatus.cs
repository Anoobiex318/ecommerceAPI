using System.Text.Json.Serialization;

namespace Ecommerce.Domain.Enumeration
{

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OrderStatus
    {

        Pending,

        Processed,

        Cancelled

    }
}