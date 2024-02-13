using Unity.Entities;
using UnityEngine;

namespace Mine.ClientServer
{
    public class UnitStatAuthoring : MonoBehaviour
    {
        public float Health;
        public float Regen;
        public float MoveSpeed;
        public float Luck;
        
        private class UnitStatAuthoringBaker : Baker<UnitStatAuthoring>
        {
            public override void Bake(UnitStatAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new Health(authoring.Health));
                AddComponent(entity, new HealthRegen(authoring.Regen));
                AddComponent(entity, new MoveSpeed(authoring.MoveSpeed));
                AddComponent(entity, new Luck(authoring.MoveSpeed));
            }
        }
    }
}