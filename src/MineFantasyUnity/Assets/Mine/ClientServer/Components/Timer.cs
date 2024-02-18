using Unity.Entities;
using Unity.NetCode;

namespace Mine.ClientServer
{
    /// <summary>
    /// Composed component: Use this with combining with other components, ex: Destroy, 
    /// </summary>
    public struct Timer : IComponentData
    {
        [GhostField] public float Value;
    }
}