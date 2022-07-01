using System.Collections.Generic;

namespace ObjectsGenerator.Spawn.Collections
{
    public interface ISpawnCollection
    {
        IReadOnlyList<ISpawnedObject> ObjectsToSpawn { get; }
    }
}