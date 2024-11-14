using Promo.TestTask.Core;

namespace Promo.TestTask.Domain.Account.Entities;
public class Country : BaseEntity
{
    public string Name { get; set; }
    public string Code { get; set; }

    public ICollection<Province> Provinces { get; set; }
}
