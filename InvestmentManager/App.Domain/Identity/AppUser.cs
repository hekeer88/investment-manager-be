using Base.Domain.Identity;

namespace App.Domain.identity;

public class AppUser : BaseUser
{
    private ICollection<Portfolio>? Portfolios { get; set; }
}