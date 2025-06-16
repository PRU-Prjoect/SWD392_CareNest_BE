using System.Text.Json.Serialization;

namespace BOL.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AppointmentStatus
    {
        Finish,
        Cancel,
        InProgress,
        NoProgress

    }
}
