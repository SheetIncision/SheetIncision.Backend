using Microsoft.AspNetCore.Mvc;
using System.Net;
using Web_API.Controllers;

namespace Web_API.Tests;

[TestFixture]
public class SheetIncisionControllerTests
{
    private SheetIncisionController _controller = null!;

    public static IEnumerable<TestCaseData> DivideCases
    {
        get
        {
            yield return new TestCaseData(3, "{\r\n  \"matrix\": [\r\n    [ 0, 1, 0 ],\r\n    [ 1, 1, 0 ],\r\n    [ 0, 1, 0 ]\r\n  ],\r\n  \"allowDiagonals\": true\r\n}");
            yield return new TestCaseData(5, "{\r\n  \"matrix\": [\r\n    [ 0, 1, 0 ],\r\n    [ 1, 0, 1 ],\r\n    [ 0, 1, 0 ]\r\n  ],\r\n  \"allowDiagonals\": false\r\n}");
            yield return new TestCaseData(1, "{\r\n  \"matrix\": [\r\n    [ 1, 1, 0 ],\r\n    [ 1, 0, 0 ],\r\n    [ 1, 1, 1 ]\r\n  ],\r\n  \"allowDiagonals\": true\r\n}");
            yield return new TestCaseData(1, "{\r\n  \"matrix\": [\r\n    [ 1, 1, 1 ],\r\n    [ 0, 0, 0 ],\r\n    [ 1, 1, 1 ]\r\n  ],\r\n  \"allowDiagonals\": true\r\n}");
        }
    }

    [SetUp]
    public void Setup()
    {
        _controller = new SheetIncisionController();
    }

    [TestCaseSource(nameof(DivideCases))]
    public async Task GetNumberOfPieces_ShouldReturnCorrectResult_WhenProvidedWithQuery(int amountOfPieces, string query)
    {
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