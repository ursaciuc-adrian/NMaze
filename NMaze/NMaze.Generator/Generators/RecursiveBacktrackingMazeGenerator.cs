using NMaze.Core.Extensions;
using System.Collections.Generic;

namespace NMaze.Generator.Generators
{
	public class RecursiveBacktrackingMazeGenerator : IMazeGenerator
	{
		private static readonly Dictionary<string, int> DX = new Dictionary<string, int>
		{
			{"E", 1 }, {"W", -1}, {"N", 0}, {"S", 0}
		};

		private static readonly Dictionary<string, int> DY = new Dictionary<string, int>
		{
			{"E", 0 }, {"W", 0}, {"N", -1}, {"S", 1}
		};

		private static readonly Dictionary<string, int> POINT = new Dictionary<string, int>
		{
			{"N", 1 }, {"S", 2}, {"E", 4}, {"W", 8}
		};

		private static readonly Dictionary<string, string> OPPOSITE = new Dictionary<string, string>
		{
			{"E", "W" }, {"W", "E"}, {"N", "S"}, {"S", "N"}
		};

		public void Generate(int[,] grid, int cx = 0, int cy = 0)
		{
			var directions = new string[] { "N", "S", "E", "W" };
			directions.Shuffle();

			foreach (var direction in directions)
			{
				var nx = cx + DX[direction];
				var ny = cy + DY[direction];

				if (ny.IsBetween(0, grid.GetLength(0) - 1) && nx.IsBetween(0, grid.GetLength(1) - 1) && grid[ny, nx] == 0)
				{
					grid[cy, cx] |= POINT[direction];
					grid[ny, nx] |= POINT[OPPOSITE[direction]];

					Generate(grid, nx, ny);
				}
			}
		}
	}
}
