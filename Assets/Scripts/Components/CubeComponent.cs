using Unity.Entities;
using Unity.Mathematics;

namespace Dots
{
    public struct CubeComponent : IComponentData
    {
        public float3 MoveDirection;
        public float MoveSpeed;
    }
}