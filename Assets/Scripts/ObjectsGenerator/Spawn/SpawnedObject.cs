using UnityEngine;

namespace ObjectsGenerator.Spawn
{
    public class SpawnedObject : MonoBehaviour, ISpawnedObject
    {
        [SerializeField] private float _radius;

        public GameObject Object => gameObject;

        public float Radius => _radius;

        private void OnDrawGizmos() => Gizmos.DrawWireSphere(transform.position, _radius);
    }
}