
namespace MediMove.Shared.Models.DTOs;

/// <summary>
/// Request for registering a new admin.
/// </summary>
/// <param name="Email">email as string</param>
/// <param name="Password">password as string</param>
public record RegisterAdminRequest(string Email, string Password);