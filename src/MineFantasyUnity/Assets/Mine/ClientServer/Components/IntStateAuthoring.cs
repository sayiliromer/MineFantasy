using Unity.Entities;
using Unity.NetCode;
using UnityEngine;

namespace Mine.ClientServer
{
    public class IntStateAuthoring : MonoBehaviour
    {
        public class Baker : Baker<IntStateAuthoring>
        {
            public override void Bake(IntStateAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.ManualOverride);
                AddComponent<IntState>(entity);
                //AddComponent<GhostChildEntity>(entity);
            }
        }
    }
}