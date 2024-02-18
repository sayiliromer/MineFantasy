using Unity.Entities;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Mine.ClientServer
{
    public class SomeUnitAuthoring : MonoBehaviour
    {
        public float Hp;
        public float MoveSpeed;

        public class UnitBaker : Baker<SomeUnitAuthoring>
        {
            public override void Bake(SomeUnitAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new Strength(1));
                AddComponent(entity, new AgilityStat(1));
                AddComponent(entity, new IntelligenceStat(1));
                AddComponent(entity, new ManaStat(10));
                AddComponent(entity, new ShieldStat(0));
                AddComponent(entity, new HealthStat(authoring.Hp));
                AddComponent(entity, new MoveSpeedStat(authoring.MoveSpeed));
            }
        }
    }
}