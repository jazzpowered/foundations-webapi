using FoundationsBusiness.Models;
using FoundationsBusiness.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Contracts.ParticipantTables;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/participant-tables")]
public sealed class ParticipantTablesController(IParticipantTableService participantTableService) : ControllerBase
{
    private readonly IParticipantTableService _participantTableService = participantTableService;

    [HttpGet]
    [EndpointSummary("Get all participant tables")]
    [ProducesResponseType(typeof(IEnumerable<ParticipantTableResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ParticipantTableResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var participantTables = await _participantTableService.GetAllAsync(cancellationToken);
        return Ok(participantTables.Select(ToResponse));
    }

    [HttpGet("{id:int}", Name = nameof(GetParticipantTableById))]
    [EndpointSummary("Get a participant table by ID")]
    [ProducesResponseType(typeof(ParticipantTableResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ParticipantTableResponse>> GetParticipantTableById(
        int id, CancellationToken cancellationToken)
    {
        var participantTable = await _participantTableService.GetByIdAsync(id, cancellationToken);
        return participantTable is null ? NotFound() : Ok(ToResponse(participantTable));
    }

    [HttpPost]
    [EndpointSummary("Create a participant table")]
    [ProducesResponseType(typeof(ParticipantTableResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ParticipantTableResponse>> Create(
        CreateParticipantTableRequest request, CancellationToken cancellationToken)
    {
        var participantTable = new ParticipantTable
        {
            Id = 0,
            Name = request.Name,
            SeasonId = request.SeasonId,
            Gender = request.Gender,
            Day = request.Day,
            Time = request.Time,
            Leaders = request.Leaders,
            Participants = request.Participants
        };
        participantTable.Id = await _participantTableService.AddAsync(participantTable, cancellationToken);
        return CreatedAtAction(
            nameof(GetParticipantTableById),
            new { id = participantTable.Id },
            ToResponse(participantTable));
    }

    [HttpPut("{id:int}")]
    [EndpointSummary("Replace a participant table")]
    [ProducesResponseType(typeof(ParticipantTableResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ParticipantTableResponse>> Update(
        int id, UpdateParticipantTableRequest request, CancellationToken cancellationToken)
    {
        var participantTable = new ParticipantTable
        {
            Id = id,
            Name = request.Name,
            SeasonId = request.SeasonId,
            Gender = request.Gender,
            Day = request.Day,
            Time = request.Time,
            Leaders = request.Leaders,
            Participants = request.Participants
        };

        if (!await _participantTableService.UpdateAsync(participantTable, cancellationToken))
        {
            return NotFound();
        }

        return Ok(ToResponse(participantTable));
    }

    [HttpDelete("{id:int}")]
    [EndpointSummary("Delete a participant table")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _participantTableService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }

    private static ParticipantTableResponse ToResponse(ParticipantTable participantTable) =>
        new(
            participantTable.Id,
            participantTable.Name,
            participantTable.SeasonId,
            participantTable.Gender,
            participantTable.Day,
            participantTable.Time,
            participantTable.Leaders,
            participantTable.Participants);
}
