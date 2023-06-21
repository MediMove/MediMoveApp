
using System.ComponentModel.DataAnnotations;

namespace MediMove.Shared.Models.DTOs;

/// <summary>
/// Request for registering a new admin.
/// </summary>
/// <param name="Email">email as string</param>
/// <param name="Password">password as string</param>
public record RegisterAdminRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}