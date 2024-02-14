using Unity.Entities;
using Unity.Mathematics;
using Unity.NetCode;

namespace Mine.ClientServer
{
    public struct Velocity : IComponentData
    {
        [GhostField] public float2 Value;
    }
}