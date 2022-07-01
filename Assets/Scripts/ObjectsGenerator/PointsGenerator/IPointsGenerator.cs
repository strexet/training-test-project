using System.Collections.Generic;
using System.Threading.Tasks;

namespace ObjectsGenerator.PointsGenerator
{
    public interface IPointsGenerator
    {
        IReadOnlyList<Point> GeneratedPoints { get; }
        int GeneratedCount { get; }
        bool GetNextPointWithRadius(float radius, out Point point, out bool finished);
    }
}