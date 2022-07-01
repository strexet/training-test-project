using UnityEngine;

namespace ObjectsGenerator.PointsGenerator.Poisson
{
    public readonly struct Candidate
    {
        private readonly Point _point;
        private readonly CandidateStaticData _staticData;

        public Point Point => _point;

        public int GridPositionX => (int)(_point.position.x * _staticData.cellSizeInverted);
        public int GridPositionY => (int)(_point.position.y * _staticData.cellSizeInverted);

        public Candidate(Point point, CandidateStaticData staticData)
        {
            _point = point;
            _staticData = staticData;
        }

        public bool IsValid() => IsInsideSampleRegion() && HasNoClosePointsInBlock();

        private bool IsInsideSampleRegion() =>
            _point.position.x - _point.radius >= 0
            && _point.position.x + _point.radius < _staticData.sampleRegionSize.x
            && _point.position.y - _point.radius >= 0
            && _point.position.y + _point.radius < _staticData.sampleRegionSize.y;

        private bool HasNoClosePointsInBlock()
        {
            int halfBlockStep = Mathf.CeilToInt(
                Consts.SquareRootOfTwoInverted * _staticData.minRadiusInverted * (_point.radius + _staticData.maxRadius)
            );

            int cellX = GridPositionX;
            int searchStartX = Mathf.Max(0, cellX - halfBlockStep);
            int searchEndX = Mathf.Min(cellX + halfBlockStep, _staticData.grid.GetLength(0) - 1);

            int cellY = GridPositionY;
            int searchStartY = Mathf.Max(0, cellY - halfBlockStep);
            int searchEndY = Mathf.Min(cellY + halfBlockStep, _staticData.grid.GetLength(1) - 1);

            for (int y = searchStartY; y <= searchEndY; y++)
            {
                for (int x = searchStartX; x <= searchEndX; x++)
                {
                    int pointIndex = _staticData.grid[x, y] - 1;
                    bool pointExists = pointIndex != -1;

                    if (pointExists)
                    {
                        var pointInGrid = _staticData.points[pointIndex];

                        float distanceSqr = (_point.position - pointInGrid.position).sqrMagnitude;
                        float distanceThreshold = _point.radius + pointInGrid.radius;

                        if (distanceSqr < distanceThreshold * distanceThreshold)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }
    }
}