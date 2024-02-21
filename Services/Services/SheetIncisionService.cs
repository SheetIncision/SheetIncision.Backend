using Services.Services.Abstraction;

namespace Services.Services;

public class SheetIncisionService: ISheetIncisionService
{
    public IEnumerable<IEnumerable<int>> Matrix { get; set; } = null!;
    public List<List<Tuple<int, int>>> Zones { get; set; } = null!;
    public bool[,] Visited { get; set; } = null!;
    public bool AllowDiagonals { get; set; }

    public async Task<int> GetNumberOfZones(IEnumerable<IEnumerable<int>> matrix, bool allowDiagonals)
    {
        var rows = matrix.Count();
        var cols = matrix.First().Count();

        Matrix = matrix;
        Zones = new List<List<Tuple<int, int>>>();
        Visited = new bool[rows, cols];
        AllowDiagonals = allowDiagonals;

        await FindZones();

        return Zones.Count;
    }

    private async Task FindZones()
    {
        List<Tuple<int, int>> unVisited = new List<Tuple<int, int>>();

        var rows = Matrix.Count();
        var cols = Matrix.First().Count();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (Matrix.ElementAt(i).ElementAt(j) == 1)
                {
                    Visited[i, j] = true;
                }
                else
                {
                    unVisited.Add(new Tuple<int, int>(i, j));
                }
            }
        }

        foreach (var vertex in unVisited)
        {
            if (!Visited[vertex.Item1, vertex.Item2])
            {
                var zone = new List<Tuple<int, int>>();

                await Traversal(
                    vertex.Item1,
                    vertex.Item2,
                    zone);

                Zones.Add(zone);
            }
        }
    }

    private async Task Traversal(int row, int col, List<Tuple<int, int>> zone)
    {
        Visited[row, col] = true;

        var directions = GetDirections();

        for (int i = 0; i < directions.GetLength(1); i++)
        {
            int newRow = row + directions[0, i];
            int newCol = col + directions[1, i];

            if (IsValid(newRow, newCol))
            {
                await Traversal(newRow, newCol, zone);
                i = -1;
            }
        }

        zone.Add(new Tuple<int, int>(row, col));
    }

    private bool IsValid(int row, int col)
    {
        return row >= 0
               && row < Matrix.Count()
               && col >= 0
               && col < Matrix.First().Count()
               && !Visited[row, col]; ;
    }

    private int[,] GetDirections() => AllowDiagonals switch
    {
        true => new int[,]
        {
            { -1, 1, 0, 0, -1, -1, 1, 1},
            { 0, 0, -1, 1, -1, 1, -1, 1 },
        },
        false => new int[,]
        {
            { -1, 1, 0, 0 },
            { 0, 0, -1, 1 },
        }
    };
}