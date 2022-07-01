using System.Collections.Generic;
using UnityEngine;

namespace ObjectsGenerator.PointsGenerator.Poisson
{
    public class CandidateStaticData
    {
        public Vector2 sampleRegionSize;
        public float cellSizeInverted;
        public float minRadiusInverted;
        public float maxRadius;

        public List<Point> points;
        public int[,] grid;
    }
}