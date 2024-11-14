using Promo.TestTask.Core;

namespace Promo.TestTask.Domain.Account.Entities;
public class Province : BaseEntity
{
    public string Name { get; set; }
    public int CountryId { get; set; }

    public Country Country { get; set; }
}
