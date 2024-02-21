namespace Services.Services.Abstraction;

public interface ISheetIncisionService
{
    Task<int> GetNumberOfZones(IEnumerable<IEnumerable<int>> inputMatrix, bool allowDiagonals);
}