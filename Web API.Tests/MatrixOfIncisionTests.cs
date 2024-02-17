using Web_API.Models;

namespace Web_API.Tests;

[TestFixture]
public class MatrixOfIncisionTests
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
        var matrixIncision = new MatrixOfIncision(data.Matrix, data.AllowDiagonals);

        var result = await matrixIncision.GetNumberOfZones();

        Assert.That(result.Equals(amountOfPieces));
    }
}