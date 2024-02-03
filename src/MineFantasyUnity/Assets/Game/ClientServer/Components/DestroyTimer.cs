using Unity.Entities;
using Unity.NetCode;

namespace Mine.ClientServer
{
    public struct DestroyTimer : IComponentData
    {
        [GhostField] public float Remaining;
    }
}
