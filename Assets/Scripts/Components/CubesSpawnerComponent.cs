using Unity.Entities;
using Unity.Mathematics;

namespace Dots
{
    public struct CubesSpawnerComponent : IComponentData
    {
        public Entity Prefab;
        public float3 SpawnPos;
        public float NextSpawnTime;
        public float SpawnRate;

    }
}
