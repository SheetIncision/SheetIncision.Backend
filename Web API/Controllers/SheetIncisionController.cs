﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web_API.Models;

namespace Web_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SheetIncisionController : Controller
{
    [HttpGet("GetNumberOfPieces")]
    public async Task<IActionResult> GetNumberOfPieces([FromQuery] string? data)
    {
        if (data is not null)
        {
            var requestBody = JsonConvert.DeserializeObject<RequestBody>(data);

            var matrixOfIncision = new MatrixOfIncision(requestBody.Matrix, requestBody.AllowDiagonals);

            return Ok(await matrixOfIncision.GetNumberOfZones());
        }
        else
        {
            return NoContent();
        }
    }

}