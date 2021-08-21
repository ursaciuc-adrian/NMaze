namespace NMaze.Generator
{
	public interface IMazeGenerator
	{
		void Generate(int[,] grid, int cx = 0, int cy = 0);
	}
}
