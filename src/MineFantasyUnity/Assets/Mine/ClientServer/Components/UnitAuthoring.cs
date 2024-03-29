﻿using Unity.Entities;
using UnityEngine;

namespace Mine.ClientServer
{
    public class UnitAuthoring : MonoBehaviour
    {
        public float Hp;
        public float MoveSpeed;

        public class UnitBaker : Baker<UnitAuthoring>
        {
            public override void Bake(UnitAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                
                AddComponent<Unit>(entity);
                AddComponent<PlayerInput>(entity);
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