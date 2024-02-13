using Unity.Entities;
using UnityEngine;

namespace Mine.ClientServer
{
    public class UnitAuthoring : MonoBehaviour
    {
        public float Hp;
        public float HpRegen;
        public float MoveSpeed;
        
        public class UnitBaker : Baker<UnitAuthoring>
        {
            public override void Bake(UnitAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent<Unit>(entity);
                AddComponent<UnitInput>(entity);
                AddComponent(entity, new Health(authoring.Hp));
                AddComponent(entity, new HealthRegen(authoring.HpRegen));
                AddComponent(entity, new MoveSpeed(authoring.MoveSpeed));
            }
        }
    }
}