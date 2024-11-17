using Promo.TestTask.Domain.Account.DTO;
using Promo.TestTask.Domain.Account.Entities;
using Promo.TestTask.Domain.Account.Mappers;
using Promo.TestTask.Domain.Account.Repositories;
using Promo.TestTask.Domain.Account.ValueObjects;

namespace Promo.TestTask.Domain.Account.Services;

public interface IUserService
{
    Task<UserDto> Create(UserDto userDto);
    Task<UserDto> Get(string email);
}

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> Create(UserDto userDto)
    {
        var user = new User
        {
            Email = userDto.Email,
            PasswordHash = userDto.PasswordHash,
            IsAgreed = userDto.IsAgreed,
            Address = new Address
            {
                Province = userDto.Province,
                Country = userDto.Country
            }
        };

        await _userRepository.AddAsync(user);

        var result = user.ToUserDto(hasPassword:false);

        return result;
    }

    public async Task<UserDto> Get(string email)
    {
        var user = await _userRepository.Get(email);
        var result = user.ToUserDto();

        return result;
    }
}
