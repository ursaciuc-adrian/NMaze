using NMaze.Core.Extensions;
using NMaze.Generator.Generators;
using System;
using System.Collections.Generic;

namespace NMaze.Cmd
{
	class Program
	{
		private static readonly Dictionary<string, int> POINT = new Dictionary<string, int>
		{
			{"N", 1 }, {"S", 2}, {"E", 4}, {"W", 8}
		};

		public static void Print(int[,] grid)
		{
			for (int i = 0; i < grid.GetLength(0); i++)
			{
				for (int j = 0; j < grid.GetLength(1); j++)
				{
					Console.Write(grid[i, j] + " ");
				}

				Console.WriteLine();
			}
			Console.WriteLine();


			Console.Write(" ");
			for (int i = 0; i < grid.GetLength(1) * 2 - 1; i++)
			{
				Console.Write("_");
			}
			Console.WriteLine();

			for (int i = 0; i < grid.GetLength(0); i++)
			{
				Console.Write("|");
				for (int j = 0; j < grid.GetLength(1); j++)
				{
					Console.Write((grid[i, j] & POINT["S"]) != 0 ? " " : "_");

					if ((grid[i, j] & POINT["E"]) != 0)
					{
						Console.Write(((grid[i, j] | grid[i, j + 1]) & POINT["S"]) != 0 ? " " : "_");
					}
					else
					{
						Console.Write("|");
					}
				}

				Console.WriteLine();
			}
			Console.WriteLine();


			
		}

		static void Main(string[] args)
		{
			var generator = new RecursiveBacktrackingMazeGenerator();
			int width = 10;
			int height = 10;

			int[,] grid = new int[height, width];
			generator.Generate(grid);

			Print(grid);
		}
	}
}
