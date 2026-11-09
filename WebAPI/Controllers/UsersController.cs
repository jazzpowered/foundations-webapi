using FoundationsBusiness.Models;
using FoundationsBusiness.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Contracts.Users;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/users")]
public sealed class UsersController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpGet]
    [EndpointSummary("Get all users")]
    [ProducesResponseType(typeof(IEnumerable<UserResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<UserResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var users = await _userService.GetAllAsync(cancellationToken);
        return Ok(users.Select(user => ToResponse(user)));
    }

    [HttpGet("{id:int}", Name = nameof(GetUserById))]
    [EndpointSummary("Get a user by ID")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserResponse>> GetUserById(
        int id,
        CancellationToken cancellationToken)
    {
        var user = await _userService.GetByIdAsync(id, cancellationToken);
        return user is null ? NotFound() : Ok(ToResponse(user));
    }

    [HttpPost]
    [EndpointSummary("Create a user")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserResponse>> Create(
        CreateUserRequest request,
        CancellationToken cancellationToken)
    {
        var user = new User
        {
            Id = 0,
            ChurchId = request.ChurchId,
            CountryId = request.CountryId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Gender = request.Gender,
            Area = request.Area,
            ZipCode = request.ZipCode
        };
        var id = await _userService.AddAsync(user, cancellationToken);
        var response = ToResponse(user, id);
        return CreatedAtAction(nameof(GetUserById), new { id }, response);
    }

    [HttpPut("{id:int}")]
    [EndpointSummary("Replace a user")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserResponse>> Update(
        int id,
        UpdateUserRequest request,
        CancellationToken cancellationToken)
    {
        var user = new User
        {
            Id = id,
            ChurchId = request.ChurchId,
            CountryId = request.CountryId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Gender = request.Gender,
            Area = request.Area,
            ZipCode = request.ZipCode
        };
        if (!await _userService.UpdateAsync(user, cancellationToken))
        {
            return NotFound();
        }

        return Ok(ToResponse(user));
    }

    [HttpDelete("{id:int}")]
    [EndpointSummary("Delete a user")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _userService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }

    private static UserResponse ToResponse(User user, int? id = null) =>
        new(
            id ?? user.Id,
            user.ChurchId,
            user.CountryId,
            user.FirstName,
            user.LastName,
            user.Email,
            user.Gender,
            user.Area,
            user.ZipCode);
}
