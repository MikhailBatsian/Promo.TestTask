using Promo.TestTask.Core;
using Promo.TestTask.Domain.Account.ValueObjects;

namespace Promo.TestTask.Domain.Account.Entities;

public class User : BaseEntity
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public bool IsAgreed { get; set; }
    public Address Address { get; set; }
}
