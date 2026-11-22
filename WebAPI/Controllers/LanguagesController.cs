using FoundationsBusiness.Models;
using FoundationsBusiness.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Contracts.Languages;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/languages")]
public sealed class LanguagesController(ILanguageService languageService) : ControllerBase
{
    private readonly ILanguageService _languageService = languageService;

    [HttpGet]
    [EndpointSummary("Get all languages")]
    [ProducesResponseType(typeof(IEnumerable<LanguageResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LanguageResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var languages = await _languageService.GetAllAsync(cancellationToken);
        return Ok(languages.Select(ToResponse));
    }

    [HttpGet("{id:int}", Name = nameof(GetLanguageById))]
    [EndpointSummary("Get a language by ID")]
    [ProducesResponseType(typeof(LanguageResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LanguageResponse>> GetLanguageById(
        int id,
        CancellationToken cancellationToken)
    {
        var language = await _languageService.GetByIdAsync(id, cancellationToken);
        return language is null ? NotFound() : Ok(ToResponse(language));
    }

    [HttpPost]
    [EndpointSummary("Create a language")]
    [ProducesResponseType(typeof(LanguageResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<LanguageResponse>> Create(
        CreateLanguageRequest request,
        CancellationToken cancellationToken)
    {
        var language = new Language { Id = 0, Name = request.Name };
        var id = await _languageService.AddAsync(language, cancellationToken);
        var response = new LanguageResponse(id, language.Name);
        return CreatedAtAction(nameof(GetLanguageById), new { id }, response);
    }

    [HttpPut("{id:int}")]
    [EndpointSummary("Replace a language")]
    [ProducesResponseType(typeof(LanguageResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LanguageResponse>> Update(
        int id,
        UpdateLanguageRequest request,
        CancellationToken cancellationToken)
    {
        var language = new Language { Id = id, Name = request.Name };
        if (!await _languageService.UpdateAsync(language, cancellationToken))
        {
            return NotFound();
        }

        return Ok(ToResponse(language));
    }

    [HttpDelete("{id:int}")]
    [EndpointSummary("Delete a language")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _languageService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }

    private static LanguageResponse ToResponse(Language language) => new(language.Id, language.Name);
}
