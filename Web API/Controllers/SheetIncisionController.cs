using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.Services.Abstraction;
using Web_API.Models;

namespace Web_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SheetIncisionController(ISheetIncisionService sheetIncisionService) : Controller
{
    private ISheetIncisionService _sheetIncisionService = sheetIncisionService;

    [HttpGet("GetNumberOfPieces")]
    public async Task<IActionResult> GetNumberOfPieces([FromQuery] string? data)
    {
        if (data is not null)
        {
            var requestBody = JsonConvert.DeserializeObject<RequestBody>(data);

            var result = await _sheetIncisionService.GetNumberOfZones(requestBody.Matrix, requestBody.AllowDiagonals);

            return Ok(result);
        }
        else
        {
            return NoContent();
        }
    }

}