using Promo.TestTask.Core;
using Promo.TestTask.Domain.Account.Entities;

namespace Promo.TestTask.Domain.Account.Repositories;
public interface IUserRepository :IRepository<User>
{
    Task<User> Get(string email);
}
