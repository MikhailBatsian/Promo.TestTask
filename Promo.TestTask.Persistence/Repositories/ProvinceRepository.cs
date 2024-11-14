using Microsoft.EntityFrameworkCore;
using Promo.TestTask.Domain.Account.Entities;
using Promo.TestTask.Domain.Account.Repositories;

namespace Promo.TestTask.Persistence.Repositories;
public class ProvinceRepository : EFRepository<Province>, IProvinceRepository
{
    public ProvinceRepository(PromoDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IList<Province>> GetCountryProvinces(int countryId)
    {
        var result = await GetQuery()
            .Where(x => x.CountryId == countryId)
            .ToListAsync();
    ;
        return result;
    }
}
