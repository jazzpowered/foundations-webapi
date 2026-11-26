using FoundationsBusiness.Models;
using FoundationsBusiness.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Contracts.Attendances;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/attendances")]
public sealed class AttendancesController(IAttendanceService attendanceService) : ControllerBase
{
    private readonly IAttendanceService _attendanceService = attendanceService;

    [HttpGet]
    [EndpointSummary("Get all attendance records")]
    [ProducesResponseType(typeof(IEnumerable<AttendanceResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AttendanceResponse>>> GetAll(
        CancellationToken cancellationToken)
    {
        var attendances = await _attendanceService.GetAllAsync(cancellationToken);
        return Ok(attendances.Select(ToResponse));
    }

    [HttpGet("{id:int}", Name = nameof(GetAttendanceById))]
    [EndpointSummary("Get an attendance record by ID")]
    [ProducesResponseType(typeof(AttendanceResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AttendanceResponse>> GetAttendanceById(
        int id,
        CancellationToken cancellationToken)
    {
        var attendance = await _attendanceService.GetByIdAsync(id, cancellationToken);
        return attendance is null ? NotFound() : Ok(ToResponse(attendance));
    }

    [HttpPost]
    [EndpointSummary("Create an attendance record")]
    [ProducesResponseType(typeof(AttendanceResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AttendanceResponse>> Create(
        CreateAttendanceRequest request,
        CancellationToken cancellationToken)
    {
        var attendance = new Attendance
        {
            Id = 0,
            ParticipantId = request.ParticipantId,
            WeekSessionId = request.WeekSessionId,
            AttendedWeekSession = request.AttendedWeekSession,
            ApproverId = request.ApproverId
        };
        attendance.Id = await _attendanceService.AddAsync(attendance, cancellationToken);
        return CreatedAtAction(
            nameof(GetAttendanceById),
            new { id = attendance.Id },
            ToResponse(attendance));
    }

    [HttpPut("{id:int}")]
    [EndpointSummary("Replace an attendance record")]
    [ProducesResponseType(typeof(AttendanceResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AttendanceResponse>> Update(
        int id,
        UpdateAttendanceRequest request,
        CancellationToken cancellationToken)
    {
        var attendance = new Attendance
        {
            Id = id,
            ParticipantId = request.ParticipantId,
            WeekSessionId = request.WeekSessionId,
            AttendedWeekSession = request.AttendedWeekSession,
            ApproverId = request.ApproverId
        };
        if (!await _attendanceService.UpdateAsync(attendance, cancellationToken))
        {
            return NotFound();
        }

        return Ok(ToResponse(attendance));
    }

    [HttpDelete("{id:int}")]
    [EndpointSummary("Delete an attendance record")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken cancellationToken)
    {
        var deleted = await _attendanceService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }

    private static AttendanceResponse ToResponse(Attendance attendance) =>
        new(
            attendance.Id,
            attendance.ParticipantId,
            attendance.WeekSessionId,
            attendance.AttendedWeekSession,
            attendance.ApproverId);
}
