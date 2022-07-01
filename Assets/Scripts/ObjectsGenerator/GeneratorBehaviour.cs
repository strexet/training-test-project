using System.Collections.Generic;
using ObjectsGenerator.Extensions;
using ObjectsGenerator.PointsGenerator;
using ObjectsGenerator.PointsGenerator.Poisson;
using ObjectsGenerator.Spawn.Collections;
using ObjectsGenerator.Spawn.Surfaces;
using UnityEngine;

namespace ObjectsGenerator
{
    public class GeneratorBehaviour : MonoBehaviour
    {
        [SerializeField] private SpawnSurface _surface;
        [SerializeField] private int _objectsCount;

        [Header("Settings")]
        [SerializeField] private int _rejectionSamplesThreshold = 30;
        [SerializeField] private int _getPointTriesThreshold = 7;

        [Header("Debug")]
        [SerializeField] private int _generatedObjectsCount;

        private ISpawnCollection _spawnCollection;
        private IPointsGenerator _pointsGenerator;

        private List<int> _spawnCollectionIndexes;
        private List<GameObject> _generatedObjects;

        private bool _needRedraw;

        private void OnValidate()
        {
            _spawnCollection ??= GetComponent<ISpawnCollection>();
            _spawnCollectionIndexes ??= new List<int>();
            Init();
        }

        private void Awake()
        {
            _spawnCollection = GetComponent<ISpawnCollection>();
            _spawnCollectionIndexes = new List<int>();
            _generatedObjects = new List<GameObject>();
            Init();
        }

        private void Update()
        {
            if (_needRedraw)
            {
                Redraw();
            }
        }

        private void Init()
        {
            (float minRadius, float maxRadius) = GetRadiusMinMax(_spawnCollection);

            _pointsGenerator = new PoissonDiscSamplingGenerator(_objectsCount, _surface.Size, minRadius, maxRadius,
                _rejectionSamplesThreshold,
                _getPointTriesThreshold);

            GeneratePoints(_pointsGenerator, _spawnCollection);

            _generatedObjectsCount = _spawnCollectionIndexes.Count;
            _needRedraw = true;
        }

        private void GeneratePoints(IPointsGenerator generator, ISpawnCollection spawnCollection)
        {
            _spawnCollectionIndexes.Clear();
            _generatedObjectsCount = 0;

            bool finished;

            do
            {
                var spawnedObject = spawnCollection.ObjectsToSpawn.GetRandom(out int index);
                float radius = spawnedObject.Radius;

                if (generator.GetNextPointWithRadius(radius, out _, out finished))
                {
                    _spawnCollectionIndexes.Add(index);
                }
            }
            while (!finished);
        }

        private void Redraw()
        {
            ClearObjects(_generatedObjects);

            for (int i = 0; i < _pointsGenerator.GeneratedPoints.Count; i++)
            {
                int spawnIndex = _spawnCollectionIndexes[i];
                var spawnedObject = _spawnCollection.ObjectsToSpawn[spawnIndex];

                var point = _pointsGenerator.GeneratedPoints[i];
                var obj = Instantiate(spawnedObject.Object, GetPointPosition(point.position), GetRandomRotationXZ());

                _generatedObjects.Add(obj);
            }

            _needRedraw = false;
        }

        private static (float minRadius, float maxRadius) GetRadiusMinMax(ISpawnCollection spawnCollection)
        {
            float minRadius = float.MaxValue;
            float maxRadius = 0;

            foreach (var objectToSpawn in spawnCollection.ObjectsToSpawn)
            {
                float radius = objectToSpawn.Radius;

                if (radius < minRadius)
                {
                    minRadius = radius;
                }

                if (radius > maxRadius)
                {
                    maxRadius = radius;
                }
            }

            return (minRadius, maxRadius);
        }

        private static void ClearObjects(List<GameObject> generatedObjects)
        {
            if (generatedObjects == null || generatedObjects.Count == 0)
            {
                return;
            }

            foreach (var generatedObject in generatedObjects)
                Destroy(generatedObject);

            generatedObjects.Clear();
        }

        private Vector3 GetPointPosition(Vector2 point) =>
            transform.position + new Vector3(point.x, 0, point.y) - 0.5f * _surface.Size3D;

        private static Quaternion GetRandomRotationXZ()
        {
            return Quaternion.AngleAxis(Random.Range(-180f, 180f), Vector3.up);
        }

        private void OnDrawGizmos()
        {
            if (Application.isPlaying || _pointsGenerator == null)
            {
                return;
            }

            foreach (var point in _pointsGenerator.GeneratedPoints)
                Gizmos.DrawSphere(GetPointPosition(point.position), point.radius);
        }
    }
}