using Akka.Actor;
using NMaze.Core.Extensions;

namespace NMaze.Generator.Generators
{
    public class Generate
    {
        public Generate(int[,] grid, int cx = 0, int cy = 0)
        {
            Cx = cx;
            Cy = cy;
            Grid = grid;
        }

        public int Cx { get; }
        public int Cy { get; }
        public int[,] Grid { get; set; }
    }

    public class GeneratorActor : ReceiveActor
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

        public GeneratorActor()
        {
            ReceiveAsync<Generate>(async (generate) =>
            {
                var directions = new string[] { "N", "S", "E", "W" };
                directions.Shuffle();

                foreach (var direction in directions)
                {
                    var nx = generate.Cx + DX[direction];
                    var ny = generate.Cy + DY[direction];

                    if (ny.IsBetween(0, generate.Grid.GetLength(0) - 1) && nx.IsBetween(0, generate.Grid.GetLength(1) - 1) && generate.Grid[ny, nx] == 0)
                    {
                        generate.Grid[generate.Cy, generate.Cx] |= POINT[direction];
                        generate.Grid[ny, nx] |= POINT[OPPOSITE[direction]];

                        var generatorActor = Context.ActorOf<GeneratorActor>();

                        generate.Grid = await generatorActor.Ask<int[,]>(new Generate(generate.Grid, nx, ny));
                    }
                }

                Sender.Tell(generate.Grid);
            });
        }
    }
}
