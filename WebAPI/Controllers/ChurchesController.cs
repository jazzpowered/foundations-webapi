using FoundationsBusiness.Models;
using FoundationsBusiness.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Contracts.Churches;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/churches")]
public sealed class ChurchesController(IChurchService churchService) : ControllerBase
{
    private readonly IChurchService _churchService = churchService;

    [HttpGet]
    [EndpointSummary("Get all churches")]
    [ProducesResponseType(typeof(IEnumerable<ChurchResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ChurchResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var churches = await _churchService.GetAllAsync(cancellationToken);
        return Ok(churches.Select(ToResponse));
    }

    [HttpGet("{id:int}", Name = nameof(GetChurchById))]
    [EndpointSummary("Get a church by ID")]
    [ProducesResponseType(typeof(ChurchResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ChurchResponse>> GetChurchById(
        int id,
        CancellationToken cancellationToken)
    {
        var church = await _churchService.GetByIdAsync(id, cancellationToken);
        return church is null ? NotFound() : Ok(ToResponse(church));
    }

    [HttpPost]
    [EndpointSummary("Create a church")]
    [ProducesResponseType(typeof(ChurchResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ChurchResponse>> Create(
        CreateChurchRequest request,
        CancellationToken cancellationToken)
    {
        var church = new Church
        {
            Id = 0,
            Name = request.Name,
            CountryId = request.CountryId,
            Area = request.Area
        };
        var id = await _churchService.AddAsync(church, cancellationToken);
        var response = new ChurchResponse(id, church.Name, church.CountryId, church.Area);
        return CreatedAtAction(nameof(GetChurchById), new { id }, response);
    }

    [HttpPut("{id:int}")]
    [EndpointSummary("Replace a church")]
    [ProducesResponseType(typeof(ChurchResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ChurchResponse>> Update(
        int id,
        UpdateChurchRequest request,
        CancellationToken cancellationToken)
    {
        var church = new Church
        {
            Id = id,
            Name = request.Name,
            CountryId = request.CountryId,
            Area = request.Area
        };
        if (!await _churchService.UpdateAsync(church, cancellationToken))
        {
            return NotFound();
        }

        return Ok(ToResponse(church));
    }

    [HttpDelete("{id:int}")]
    [EndpointSummary("Delete a church")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _churchService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }

    private static ChurchResponse ToResponse(Church church) =>
        new(church.Id, church.Name, church.CountryId, church.Area);
}
