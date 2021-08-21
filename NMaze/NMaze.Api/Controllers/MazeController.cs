using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NMaze.Generator;
using System.Collections.Generic;
using System.Text;

namespace NMaze.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class MazeController : ControllerBase
	{
		private readonly ILogger<MazeController> _logger;
		private readonly IMazeGenerator _mazeGenerator;

		public MazeController(
			ILogger<MazeController> logger,
			IMazeGenerator mazeGenerator)
		{
			_logger = logger;
			_mazeGenerator = mazeGenerator;
		}

		[HttpGet]
		public ActionResult Generate(int height = 10, int width = 10)
		{
			var grid = new int[height, width];
			_mazeGenerator.Generate(grid);

			var dict = new Dictionary<int, int[]>();

			for (int i = 0; i < grid.GetLength(0); i++)
			{
				var s = new List<int>();
				for (int j = 0; j < grid.GetLength(1); j++) 
				{
					s.Add(grid[i, j]);
				}

				dict.Add(i, s.ToArray());
			}

			return Ok(dict);
		}
	}
}
