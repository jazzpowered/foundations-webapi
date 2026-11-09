using FoundationsBusiness.Models;
using FoundationsBusiness.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Contracts.Countries;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/countries")]
public sealed class CountriesController(ICountryService countryService) : ControllerBase
{
    private readonly ICountryService _countryService = countryService;

    [HttpGet]
    [EndpointSummary("Get all countries")]
    [ProducesResponseType(typeof(IEnumerable<CountryResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CountryResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var countries = await _countryService.GetAllAsync(cancellationToken);
        return Ok(countries.Select(ToResponse));
    }

    [HttpGet("{id:int}", Name = nameof(GetCountryById))]
    [EndpointSummary("Get a country by ID")]
    [ProducesResponseType(typeof(CountryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CountryResponse>> GetCountryById(
        int id,
        CancellationToken cancellationToken)
    {
        var country = await _countryService.GetByIdAsync(id, cancellationToken);
        return country is null ? NotFound() : Ok(ToResponse(country));
    }

    [HttpPost]
    [EndpointSummary("Create a country")]
    [ProducesResponseType(typeof(CountryResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CountryResponse>> Create(
        CreateCountryRequest request,
        CancellationToken cancellationToken)
    {
        var country = new Country { Id = 0, Name = request.Name };
        var id = await _countryService.AddAsync(country, cancellationToken);
        var response = new CountryResponse(id, country.Name);
        return CreatedAtAction(nameof(GetCountryById), new { id }, response);
    }

    [HttpPut("{id:int}")]
    [EndpointSummary("Replace a country")]
    [ProducesResponseType(typeof(CountryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CountryResponse>> Update(
        int id,
        UpdateCountryRequest request,
        CancellationToken cancellationToken)
    {
        var country = new Country { Id = id, Name = request.Name };
        if (!await _countryService.UpdateAsync(country, cancellationToken))
        {
            return NotFound();
        }

        return Ok(ToResponse(country));
    }

    [HttpDelete("{id:int}")]
    [EndpointSummary("Delete a country")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _countryService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }

    private static CountryResponse ToResponse(Country country) => new(country.Id, country.Name);
}
