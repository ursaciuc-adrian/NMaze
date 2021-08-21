namespace NMaze.Core.Extensions
{
	public static class IntExtensions
	{
		public static bool IsBetween(this int val, int low, int high)
		{
			return val >= low && val <= high;
		}
	}
}
