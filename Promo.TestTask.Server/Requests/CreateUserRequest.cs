namespace Promo.TestTask.Api.Requests;

public class CreateUserRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsAgreed { get; set; }
    public int ProvinceId { get; set; }
}
