using System;
using Unity.Entities;
using UnityEngine;

namespace Mine.ClientServer
{
    public class BaseActiveAbilityItemAuthoring : MonoBehaviour
    {
        //public AbilityTimings Timings;
        public class Baker : Baker<BaseActiveAbilityItemAuthoring>
        {
            public override void Bake(BaseActiveAbilityItemAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent<Item>(entity);
                AddComponent(entity, IntState.FromEnum(ActiveItemState.Ready));
                //AddComponent<Equipment>(entity);
                AddComponent<Timer>(entity);
                //AddComponent(entity,authoring.Timings);
            }
        }
    }
}