using UnityEngine;

namespace ObjectsGenerator.Spawn
{
    public interface ISpawnedObject
    {
        public GameObject Object { get; }
        public float Radius { get; }
    }
}