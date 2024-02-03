using Unity.Entities;
using UnityEngine;

namespace Mine.ClientServer
{
    public class UnitAuthoring : MonoBehaviour
    {
        public bool IncludeStats;

        private class Baker : Baker<UnitAuthoring>
        {
            public override void Bake(UnitAuthoring authoring)
            {
                if (!authoring.IncludeStats) return;
                var mainEntity = GetEntity(TransformUsageFlags.None);
                var buffer = AddBuffer<FloatStats>(mainEntity);
            }
        }
    }

}
