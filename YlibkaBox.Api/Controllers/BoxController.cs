using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;
using YlibkaBox.Api.Domain.Contracts;

namespace YlibkaBox.Api.Controllers;

[Authorize]
[Produces("application/json")]
[Route("api/[controller]")]
[ProducesResponseType(typeof(ModelStateDictionary), (int)HttpStatusCode.BadRequest)]
[ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
[ApiController]
public class BoxController : ControllerBase
{
    private readonly IBoxService _boxService;

    public BoxController(IBoxService boxService)
    {
        _boxService = boxService;
    }


    /// <summary>
    ///     Метод должен получать данные по номеру маршрута список коробок.
    /// </summary>
    [HttpGet("getdocumentlist")]
    [SwaggerOperation(Tags = new[] { "Box" })]
    [ProducesResponseType(typeof(FileStreamResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDocuments()
    {
        var result = await _boxService.GetDocuments();

        if (result.StatusCode == StatusCodes.Status200OK)
            return result.Value as FileContentResult;

        return result;
    }


    /// <summary>
    ///     Метод должен получать данные по номеру документа список товаров (наименование) в этой поставке.
    /// </summary>
    [HttpGet("getdocument")]
    [SwaggerOperation(Tags = new[] { "Box" })]
    [ProducesResponseType(typeof(FileStreamResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDocument([FromQuery] int id)
    {
        var result = await _boxService.GetDocument(id);

        if (result.StatusCode == StatusCodes.Status200OK)
            return result.Value as FileContentResult;


        return result;
    }
}