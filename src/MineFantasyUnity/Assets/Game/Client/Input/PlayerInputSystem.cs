using System.Diagnostics;
using Mine.ClientServer;
using Mine.Core;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.NetCode;
using UnityEngine;
using UnityEngine.Profiling;
using Debug = UnityEngine.Debug;

namespace Mine.Client
{
    [WorldSystemFilter(WorldFilters.NoThinClient)]
    [UpdateInGroup(typeof(InputGroup))]
    public partial struct PlayerInputSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");
            state.Dependency.Complete();
            var sw = Stopwatch.StartNew();
            //Profiler.BeginSample("Apply");
            state.Dependency = new Apply()
            {
                Dir = new float2(x, y)
            }.ScheduleParallel(state.Dependency);
            
            
            // for (int i = 0; i < 1; i++)
            // {
            //     Apply(x, y, ref state);
            // }
            //Profiler.EndSample();
            //Debug.Log(sw.Elapsed.TotalMilliseconds);
        }

        // [BurstCompile]
        // private void Apply(float x, float y, ref SystemState state)
        // {
        //     foreach (var input in SystemAPI.Query<RefRW<UnitInput>>().WithAll<GhostOwnerIsLocal>())
        //     {
        //         input.ValueRW.Direction = new float2(x, y);
        //     }
        // }
        
        [BurstCompile]
        [WithAll(typeof(GhostOwnerIsLocal))]
        public partial struct Apply : IJobEntity
        {
            public float2 Dir;

            public void Execute(ref UnitInput input)
            {
                input.Direction = Dir;
            }
        }
    }
}