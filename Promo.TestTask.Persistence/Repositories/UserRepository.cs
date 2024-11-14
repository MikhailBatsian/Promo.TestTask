using Microsoft.EntityFrameworkCore;
using Promo.TestTask.Domain.Account.Entities;
using Promo.TestTask.Domain.Account.Repositories;

namespace Promo.TestTask.Persistence.Repositories;
public class UserRepository : EFRepository<User>, IUserRepository
{
    public UserRepository(PromoDbContext dbContext) : base(dbContext)
    {
    }

    public Task<User> Get(string email)
    {  
        return GetQuery().FirstOrDefaultAsync(x => x.Email == email);
    }
}
