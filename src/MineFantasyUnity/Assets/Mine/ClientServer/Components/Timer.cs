using Unity.Entities;
using Unity.NetCode;

namespace Mine.ClientServer
{
    public struct Timer : IComponentData
    {
        [GhostField] public float Remaining;
    }
}