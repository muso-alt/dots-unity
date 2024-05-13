using Unity.Entities;
using UnityEngine;
using UnityEngine.Serialization;

namespace Dots
{
    public class CubeSpawnerAuthoring : MonoBehaviour
    {
        public GameObject prefab;
        public float spawnRate;
    }

    class CubeSpawnerBaker : Baker<CubeSpawnerAuthoring>
    {
        public override void Bake(CubeSpawnerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new CubesSpawnerComponent
            {
               Prefab = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic),
               SpawnPos = authoring.transform.position,
               NextSpawnTime = 0f,
               SpawnRate = authoring.spawnRate  
            });
        }
    }
}