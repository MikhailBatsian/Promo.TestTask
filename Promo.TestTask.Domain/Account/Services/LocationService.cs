using Promo.TestTask.Domain.Account.DTO;
using Promo.TestTask.Domain.Account.Repositories;

namespace Promo.TestTask.Domain.Account.Services;

public interface ILocationService
{
    Task<IList<CountryDto>> GetCountries();
    Task<IList<ProvinceDto>> GetProvinces(int countryId);
}

public class LocationService : ILocationService
{
    private readonly ICountryRepository _countryRepository;
    private readonly IProvinceRepository _provinceRepository;

    public LocationService(
        ICountryRepository countryRepository,
        IProvinceRepository provinceRepository)
    {
        _countryRepository = countryRepository;
        _provinceRepository = provinceRepository;
    }
    
    public async Task<IList<CountryDto>> GetCountries()
    {
        var countries = await _countryRepository.GetAllAsync();
        var result = countries.Select(x => new CountryDto
        {
            Id = x.Id,
            Name = x.Name
        }).ToList();

        return result;
    }

    public async Task<IList<ProvinceDto>> GetProvinces(int countryId)
    {
        var provinces = await _provinceRepository.GetCountryProvinces(countryId);
        var result = provinces.Select(x => new ProvinceDto
        {
            Id = x.Id,
            Name = x.Name
        }).ToList();

        return result;
    }
}
