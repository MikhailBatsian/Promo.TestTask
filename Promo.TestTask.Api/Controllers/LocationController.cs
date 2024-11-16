using Microsoft.AspNetCore.Mvc;
using Promo.TestTask.Domain.Account.Services;

namespace Promo.TestTask.Api.Controllers;

[ApiController]
[Route("api/location")]
public class LocationController : ControllerBase
{
    private readonly ILocationService _locationService;

    public LocationController(ILocationService locationService)
    {
        _locationService = locationService;
    }
    
    [HttpGet("countries")]
    public async Task<ActionResult> GetCountries()
    {
        var countries = await _locationService.GetCountries();
        return Ok(countries);
    }

    [HttpGet("provinces")]
    public async Task<ActionResult> GetCountryProvinces(int countryId)
    {
        var provinces = await _locationService.GetProvinces(countryId);
        return Ok(provinces);
    }
}
