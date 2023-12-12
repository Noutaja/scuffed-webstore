using System.Text.Json.Serialization;

namespace ScuffedWebstore.Core.src.Types;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserRole
{
    Normal, Admin
}