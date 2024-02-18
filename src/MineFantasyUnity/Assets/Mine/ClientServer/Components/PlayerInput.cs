using Unity.Mathematics;
using Unity.NetCode;

namespace Mine.ClientServer
{
    public struct PlayerInput : IInputComponentData
    {
        [GhostField] public float2 Direction;
        [GhostField] public float2 TargetPosition;
    }
}