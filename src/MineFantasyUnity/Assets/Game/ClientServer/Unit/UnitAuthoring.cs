using Unity.Entities;
using UnityEngine;

namespace Mine.ClientServer
{
    public interface IStaticStatMarker : IComponentData { }
    public interface IDynamicStatMarker : IComponentData { }
    
    public class UnitAuthoring : MonoBehaviour
    {
        public class UnitBaker : StatBaker<UnitAuthoring>
        {
            protected override void OnBake(UnitAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent<Unit>(entity);
                AddComponent<UnitInput>(entity);
                AddFloatDynamicStat<HealthMarker>(100);
                AddFloatStaticStat<MoveSpeedMarker>(8);
            }
        }
    }
}