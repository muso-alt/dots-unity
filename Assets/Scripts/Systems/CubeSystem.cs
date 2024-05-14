using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

namespace Dots.Systems
{
    public partial struct CubeSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var entityManager = state.EntityManager;

            var entities = entityManager.GetAllEntities(Allocator.Temp);

            foreach (var entity in entities)
            {
                continue;
                if (entityManager.HasComponent<CubeComponent>(entity))
                {
                    var cube = entityManager.GetComponentData<CubeComponent>(entity);
                    var transform = entityManager.GetComponentData<LocalTransform>(entity);

                    var moveDirection = cube.MoveDirection * SystemAPI.Time.DeltaTime * cube.MoveSpeed;

                    transform.Position += moveDirection;
                    entityManager.SetComponentData(entity, transform);

                    if (cube.MoveSpeed > 0)
                    {
                        cube.MoveSpeed -= 1 * SystemAPI.Time.DeltaTime;
                    }
                    else
                    {
                        cube.MoveSpeed = 0;
                    }

                    entityManager.SetComponentData(entity, cube);
                }
            }
        }
    }
}