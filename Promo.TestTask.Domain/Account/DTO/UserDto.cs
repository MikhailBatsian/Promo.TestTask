namespace Promo.TestTask.Domain.Account.DTO;

public class UserDto
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public bool IsAgreed { get; set; }
    public int ProvinceId { get; set; }
}
