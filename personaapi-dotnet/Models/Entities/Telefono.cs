using System.Text.Json.Serialization;

namespace personaapi_dotnet.Models.Entities
{
    public partial class Telefono
    {
        public string Num { get; set; } = null!;
        public string? Oper { get; set; }
        public long Duenio { get; set; }

        [JsonIgnore]
        public virtual Persona? DuenioNavigation { get; set; }
    }
}
