using Promo.TestTask.Core;

namespace Promo.TestTask.Domain.Account.Entities;

public class User : BaseEntity
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public bool IsAgreed { get; set; }
    public int ProvinceId { get; set; }

    
    public Province Province { get; set; }
}
