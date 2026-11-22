using FoundationsBusiness.Models;
using FoundationsBusiness.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Contracts.WeekSessions;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/week-sessions")]
public sealed class WeekSessionsController(IWeekSessionService weekSessionService) : ControllerBase
{
    private readonly IWeekSessionService _weekSessionService = weekSessionService;

    [HttpGet]
    [EndpointSummary("Get all week sessions")]
    [ProducesResponseType(typeof(IEnumerable<WeekSessionResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<WeekSessionResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var weekSessions = await _weekSessionService.GetAllAsync(cancellationToken);
        return Ok(weekSessions.Select(ToResponse));
    }

    [HttpGet("{id:int}", Name = nameof(GetWeekSessionById))]
    [EndpointSummary("Get a week session by ID")]
    [ProducesResponseType(typeof(WeekSessionResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WeekSessionResponse>> GetWeekSessionById(
        int id,
        CancellationToken cancellationToken)
    {
        var weekSession = await _weekSessionService.GetByIdAsync(id, cancellationToken);
        return weekSession is null ? NotFound() : Ok(ToResponse(weekSession));
    }

    [HttpPost]
    [EndpointSummary("Create a week session")]
    [ProducesResponseType(typeof(WeekSessionResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<WeekSessionResponse>> Create(
        CreateWeekSessionRequest request,
        CancellationToken cancellationToken)
    {
        var weekSession = new WeekSession
        {
            Id = 0,
            LanguageId = request.LanguageId,
            Week = request.Week,
            Active = request.Active,
            Content = request.Content
        };
        var id = await _weekSessionService.AddAsync(weekSession, cancellationToken);
        var response = new WeekSessionResponse(
            id, weekSession.LanguageId, weekSession.Week, weekSession.Active, weekSession.Content);
        return CreatedAtAction(nameof(GetWeekSessionById), new { id }, response);
    }

    [HttpPut("{id:int}")]
    [EndpointSummary("Replace a week session")]
    [ProducesResponseType(typeof(WeekSessionResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WeekSessionResponse>> Update(
        int id,
        UpdateWeekSessionRequest request,
        CancellationToken cancellationToken)
    {
        var weekSession = new WeekSession
        {
            Id = id,
            LanguageId = request.LanguageId,
            Week = request.Week,
            Active = request.Active,
            Content = request.Content
        };
        if (!await _weekSessionService.UpdateAsync(weekSession, cancellationToken))
        {
            return NotFound();
        }

        return Ok(ToResponse(weekSession));
    }

    [HttpDelete("{id:int}")]
    [EndpointSummary("Delete a week session")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _weekSessionService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }

    private static WeekSessionResponse ToResponse(WeekSession weekSession) =>
        new(weekSession.Id, weekSession.LanguageId, weekSession.Week, weekSession.Active, weekSession.Content);
}
