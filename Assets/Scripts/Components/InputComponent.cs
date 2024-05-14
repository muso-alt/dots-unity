using Unity.Entities;
using Unity.Mathematics;

namespace Dots
{
    public struct InputComponent : IComponentData
    {
        public float3 InputValue;
        public bool PressingLMB;
    }
}