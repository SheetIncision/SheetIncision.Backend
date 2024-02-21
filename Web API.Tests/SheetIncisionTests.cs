using Services.Services;
using Web_API.Models;

namespace Web_API.Tests;

[TestFixture]
public class SheetIncisionTests
{
    

    public static IEnumerable<TestCaseData> DivideCases
    {
        get
        {
            yield return new TestCaseData(3, new RequestBody { Matrix = new List<List<int>> { new List<int> { 0, 1, 0 }, new List<int> { 1, 1, 0 }, new List<int> { 0, 1, 0 } }, AllowDiagonals = true });
            yield return new TestCaseData(2, new RequestBody { Matrix = new List<List<int>> { new List<int> { 0, 1, 0 }, new List<int> { 0, 1, 0 }, new List<int> { 0, 1, 0 } }, AllowDiagonals = false });
            yield return new TestCaseData(1, new RequestBody { Matrix = new List<List<int>> { new List<int> { 1, 1, 0 }, new List<int> { 1, 1, 1 }, new List<int> { 1, 1, 1 } }, AllowDiagonals = true });
            yield return new TestCaseData(0, new RequestBody { Matrix = new List<List<int>> { new List<int> { 1, 1, 1 }, new List<int> { 1, 1, 1 }, new List<int> { 1, 1, 1 } }, AllowDiagonals = true });
        }
    }

    [TestCaseSource(nameof(DivideCases))]
    public async Task GetNumberOfZones_ShouldReturnCorrectResult_WhenGivenMatrixAndDiagonalsPermission(int amountOfPieces, RequestBody data)
    {
        var service = new SheetIncisionService();

        var result = await service.GetNumberOfZones(data.Matrix, data.AllowDiagonals);

        Assert.That(result.Equals(amountOfPieces));
    }
}