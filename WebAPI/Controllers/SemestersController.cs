using FoundationsBusiness.Models;
using FoundationsBusiness.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Contracts.Semesters;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/semesters")]
public sealed class SemestersController(ISemesterService semesterService) : ControllerBase
{
    private readonly ISemesterService _semesterService = semesterService;

    [HttpGet]
    [EndpointSummary("Get all semesters")]
    [ProducesResponseType(typeof(IEnumerable<SemesterResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SemesterResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var semesters = await _semesterService.GetAllAsync(cancellationToken);
        return Ok(semesters.Select(ToResponse));
    }

    [HttpGet("{id:int}", Name = nameof(GetSemesterById))]
    [EndpointSummary("Get a semester by ID")]
    [ProducesResponseType(typeof(SemesterResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SemesterResponse>> GetSemesterById(int id, CancellationToken cancellationToken)
    {
        var semester = await _semesterService.GetByIdAsync(id, cancellationToken);
        return semester is null ? NotFound() : Ok(ToResponse(semester));
    }

    [HttpPost]
    [EndpointSummary("Create a semester")]
    [ProducesResponseType(typeof(SemesterResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SemesterResponse>> Create(CreateSemesterRequest request, CancellationToken cancellationToken)
    {
        var semester = new Semester { Id = 0, Name = request.Name };
        var id = await _semesterService.AddAsync(semester, cancellationToken);
        return CreatedAtAction(nameof(GetSemesterById), new { id }, new SemesterResponse(id, semester.Name));
    }

    [HttpPut("{id:int}")]
    [EndpointSummary("Replace a semester")]
    [ProducesResponseType(typeof(SemesterResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SemesterResponse>> Update(int id, UpdateSemesterRequest request, CancellationToken cancellationToken)
    {
        var semester = new Semester { Id = id, Name = request.Name };
        if (!await _semesterService.UpdateAsync(semester, cancellationToken))
        {
            return NotFound();
        }

        return Ok(ToResponse(semester));
    }

    [HttpDelete("{id:int}")]
    [EndpointSummary("Delete a semester")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _semesterService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }

    private static SemesterResponse ToResponse(Semester semester) => new(semester.Id, semester.Name);
}
