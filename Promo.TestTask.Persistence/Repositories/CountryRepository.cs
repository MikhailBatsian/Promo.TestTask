using Promo.TestTask.Domain.Account.Entities;
using Promo.TestTask.Domain.Account.Repositories;

namespace Promo.TestTask.Persistence.Repositories;
public class CountryRepository : EFRepository<Country>, ICountryRepository
{
    public CountryRepository(PromoDbContext dbContext) : base(dbContext)
    {
    }
}
