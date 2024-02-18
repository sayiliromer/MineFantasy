using Unity.Entities;
using Unity.NetCode;

namespace Mine.ClientServer
{
    public struct Owner : IComponentData
    {
        [GhostField] public Entity Value;
    }
}