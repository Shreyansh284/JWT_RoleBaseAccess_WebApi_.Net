using System.Text.Json.Serialization;
using Core.Enums;

namespace Core.Entities;

public class User
{
    // [JsonIgnore]
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; }= null!;
    public string Password { get; set; }= null!;

    public Role Role { get; set; } = Role.User;
}