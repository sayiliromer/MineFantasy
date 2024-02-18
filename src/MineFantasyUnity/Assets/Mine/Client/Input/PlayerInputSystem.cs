using Core;
using Mine.ClientServer;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.NetCode;
using UnityEngine;

namespace Mine.Client
{
    [WorldSystemFilter(WorldFilters.NoThinClient)]
    [UpdateInGroup(typeof(GhostInputSystemGroup))]
    public partial struct PlayerInputSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");
            state.Dependency = new Apply(new float2(x, y)).ScheduleParallel(state.Dependency);
        }

        [BurstCompile]
        [WithAll(typeof(GhostOwnerIsLocal))]
        public partial struct Apply : IJobEntity
        {
            private readonly float2 _dir;

            public Apply(float2 dir) : this()
            {
                _dir = dir;
            }

            private void Execute(ref PlayerInput input)
            {
                input.Direction = _dir;
            }
        }
    }
}