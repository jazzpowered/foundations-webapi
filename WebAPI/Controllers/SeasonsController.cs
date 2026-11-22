using FoundationsBusiness.Models;
using FoundationsBusiness.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Contracts.Seasons;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/seasons")]
public sealed class SeasonsController(ISeasonService seasonService) : ControllerBase
{
    private readonly ISeasonService _seasonService = seasonService;

    [HttpGet]
    [EndpointSummary("Get all seasons")]
    [ProducesResponseType(typeof(IEnumerable<SeasonResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SeasonResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var seasons = await _seasonService.GetAllAsync(cancellationToken);
        return Ok(seasons.Select(ToResponse));
    }

    [HttpGet("{id:int}", Name = nameof(GetSeasonById))]
    [EndpointSummary("Get a season by ID")]
    [ProducesResponseType(typeof(SeasonResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SeasonResponse>> GetSeasonById(int id, CancellationToken cancellationToken)
    {
        var season = await _seasonService.GetByIdAsync(id, cancellationToken);
        return season is null ? NotFound() : Ok(ToResponse(season));
    }

    [HttpPost]
    [EndpointSummary("Create a season")]
    [ProducesResponseType(typeof(SeasonResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SeasonResponse>> Create(CreateSeasonRequest request, CancellationToken cancellationToken)
    {
        var season = new Season
        {
            Id = 0, Year = request.Year, LanguageId = request.LanguageId,
            ChurchId = request.ChurchId, SemesterId = request.SemesterId, Active = request.Active
        };
        var id = await _seasonService.AddAsync(season, cancellationToken);
        season.Id = id;
        return CreatedAtAction(nameof(GetSeasonById), new { id }, ToResponse(season));
    }

    [HttpPut("{id:int}")]
    [EndpointSummary("Replace a season")]
    [ProducesResponseType(typeof(SeasonResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SeasonResponse>> Update(int id, UpdateSeasonRequest request, CancellationToken cancellationToken)
    {
        var season = new Season
        {
            Id = id, Year = request.Year, LanguageId = request.LanguageId,
            ChurchId = request.ChurchId, SemesterId = request.SemesterId, Active = request.Active
        };
        if (!await _seasonService.UpdateAsync(season, cancellationToken))
        {
            return NotFound();
        }

        return Ok(ToResponse(season));
    }

    [HttpDelete("{id:int}")]
    [EndpointSummary("Delete a season")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _seasonService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }

    private static SeasonResponse ToResponse(Season season) =>
        new(season.Id, season.Year, season.LanguageId, season.ChurchId, season.SemesterId, season.Active);
}
