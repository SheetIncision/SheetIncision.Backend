using Microsoft.AspNetCore.Mvc;
using System.Net;
using NSubstitute;
using Services.Services.Abstraction;
using Web_API.Controllers;

namespace Web_API.Tests;

[TestFixture]
public class SheetIncisionControllerTests
{
    private SheetIncisionController _controller = null!;
    private readonly ISheetIncisionService _service = Substitute.For<ISheetIncisionService>();

    [SetUp]
    public void Setup()
    {
        _service.GetNumberOfZones(Arg.Any<IEnumerable<IEnumerable<int>>>(), Arg.Any<bool>())
            .Returns(1);

        _controller = new SheetIncisionController(_service);
    }

    [Test]
    public async Task GetNumberOfPieces_ShouldReturnCorrectResult_WhenProvidedWithQuery()
    {
        var amountOfPieces = 1;
        var query =
            "{\r\n  \"matrix\": [\r\n    [ 1, 1, 1 ],\r\n    [ 0, 0, 0 ],\r\n    [ 1, 1, 1 ]\r\n  ],\r\n  \"allowDiagonals\": true\r\n}";

        var result = await _controller
            .GetNumberOfPieces(query) as OkObjectResult;
        var statusCode = result.StatusCode;

        Assert.That(statusCode.Equals((int)HttpStatusCode.OK));
        Assert.That(result.Value!.Equals(amountOfPieces));
    }

    [Test]
    public async Task GetNumberOfPieces_ShouldReturnNoContentResult_WhenProvidedWithNoQuery()
    {
        var result = await _controller.GetNumberOfPieces(null) as NoContentResult;
        var statusCode = result.StatusCode;

        Assert.That(statusCode.Equals((int)HttpStatusCode.NoContent));
    }
}