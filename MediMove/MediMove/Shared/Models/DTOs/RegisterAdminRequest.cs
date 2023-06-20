
namespace MediMove.Shared.Models.DTOs;

/// <summary>
/// Request for registering a new admin.
/// </summary>
/// <param name="Email">email as string</param>
/// <param name="Password">password as string</param>
public record RegisterAdminRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}