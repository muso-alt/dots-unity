using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Random = Unity.Mathematics.Random;

namespace Dots.Systems
{
    [BurstCompile]
    public partial struct CubeSpawnerSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            if (!SystemAPI.TryGetSingletonEntity<CubesSpawnerComponent>(out var spawnerEntity))
            {
                return;
            }

            var spawner = SystemAPI.GetComponentRW<CubesSpawnerComponent>(spawnerEntity);
            var ecb = new EntityCommandBuffer(Allocator.Temp);

            if (spawner.ValueRO.NextSpawnTime < SystemAPI.Time.ElapsedTime)
            {
                var entity = ecb.Instantiate(spawner.ValueRO.Prefab);

                ecb.AddComponent(entity,
                    new CubeComponent
                    {
                        MoveDirection = Random
                            .CreateFromIndex((uint)(SystemAPI.Time.ElapsedTime / SystemAPI.Time.DeltaTime))
                            .NextFloat3(),
                        MoveSpeed = 10
                    });
                
                
                spawner.ValueRW.NextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.SpawnRate;
                ecb.Playback(state.EntityManager);
            }
        }
    }
}
