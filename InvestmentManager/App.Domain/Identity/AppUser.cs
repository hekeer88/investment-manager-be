using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain.Identity;

namespace App.Domain.identity;

public class AppUser : BaseUser
{
    [MinLength(1)]
    [MaxLength(128)]
    public string FirstName { get; set; } = default!;
    
    [MinLength(1)]
    [MaxLength(128)]
    public string LastName { get; set; } = default!;
    
    private ICollection<Portfolio>? Portfolios { get; set; }
    
    public ICollection<RefreshToken>? RefreshTokens { get; set; }

    public string FirstLastName => FirstName + " " + LastName;
    public string LastFirstName => LastName + " " + FirstName;
}