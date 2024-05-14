using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace Dots.Systems
{
    [BurstCompile]
    public partial class CubeSpawnerSystem : SystemBase
    {
        private InputComponent _inputComponent;
        protected override void OnUpdate()
        {
            if (!SystemAPI.TryGetSingletonEntity<CubesSpawnerComponent>(out var spawnerEntity))
            {
                return;
            }

            var spawner = SystemAPI.GetComponentRW<CubesSpawnerComponent>(spawnerEntity);
            
            if (SystemAPI.TryGetSingleton(out _inputComponent))
            {
                if(_inputComponent.PressingLMB)
                {
                    Debug.Log(_inputComponent.InputValue);
                    var entity = EntityManager.Instantiate(spawner.ValueRO.Prefab);
                    EntityManager.SetComponentData(entity, LocalTransform.FromPosition(_inputComponent.InputValue));
                }
            }
            
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
                ecb.Playback(EntityManager);
            }
        }
    }
}
