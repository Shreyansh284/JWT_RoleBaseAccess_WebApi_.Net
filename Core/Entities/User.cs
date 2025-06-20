using System.Text.Json.Serialization;

namespace Core.Entities;

public class User
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; }= null!;
    public string Password { get; set; }= null!;
}