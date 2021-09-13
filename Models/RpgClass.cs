using System.Text.Json.Serialization;

namespace CURSO_UDEMY_COGNIZANT_netcore31webapi.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RpgClass
    {
        Knight,
        Mage,
        Cleric
    }
}
