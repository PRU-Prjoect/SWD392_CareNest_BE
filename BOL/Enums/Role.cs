using System.Text.Json.Serialization;

namespace BOL.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Role
    {
        Customer,
        Shop,
        Staff,
        Guest
    }
}
