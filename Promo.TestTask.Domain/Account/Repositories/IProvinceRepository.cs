using Promo.TestTask.Core;
using Promo.TestTask.Domain.Account.Entities;

namespace Promo.TestTask.Domain.Account.Repositories;
public interface IProvinceRepository : IRepository<Province>
{
    Task<IList<Province>> GetCountryProvinces(int countryId);
}
