using Promo.TestTask.Domain.Account.DTO;
using Promo.TestTask.Domain.Account.Entities;

namespace Promo.TestTask.Domain.Account.Mappers;
internal static class UserMapper
{
    public static UserDto ToUserDto(this User user, bool hasPassword = true)
    {
        if (user == null) return null;

        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            PasswordHash = user.PasswordHash,
            ProvinceId = user.ProvinceId,
            IsAgreed = user.IsAgreed
        };
    }
}
