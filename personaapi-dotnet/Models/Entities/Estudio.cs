using personaapi_dotnet.Models.Entities;
using System.Text.Json.Serialization;  // 🔹 agrega este using arriba

public partial class Estudio
{
    public int IdProf { get; set; }
    public long CcPer { get; set; }
    public DateOnly Fecha { get; set; }
    public string? Univer { get; set; }

    [JsonIgnore]
    public virtual Persona? CcPerNavigation { get; set; }

    [JsonIgnore]
    public virtual Profesion? IdProfNavigation { get; set; }
}

