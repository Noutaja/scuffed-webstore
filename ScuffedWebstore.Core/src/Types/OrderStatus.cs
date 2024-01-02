using System.Text.Json.Serialization;

namespace ScuffedWebstore.Core.src.Types;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OrderStatus
{
    Pending, Sent, Delivered, Cancelled
}