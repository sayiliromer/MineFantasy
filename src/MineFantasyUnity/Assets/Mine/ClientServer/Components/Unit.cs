using Core;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.NetCode;
using Unity.Transforms;

namespace Mine.ClientServer
{
    public struct Unit : IComponentData
    {
    }

    public struct UnitInput : IInputComponentData
    {
        [GhostField] public float2 Direction;
        [GhostField] public float2 TargetPosition;
    }

    [BurstCompile]
    [UpdateInGroup(typeof(PredictedSimulationSystemGroup))]
    [WorldSystemFilter(WorldFilters.ClientServer)]
    public partial struct WalkerApplySystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var dt = SystemAPI.Time.DeltaTime;
            state.Dependency = new WalkerApplyJob(dt).ScheduleParallel(state.Dependency);
        }

        [BurstCompile]
        [WithAll(typeof(Simulate))]
        public partial struct WalkerApplyJob : IJobEntity
        {
            private readonly float _dt;

            public WalkerApplyJob(float dt) : this()
            {
                _dt = dt;
            }

            private void Execute(in MoveSpeed speed, in UnitInput input, ref LocalTransform transform)
            {
                var delta = _dt * speed.ToFinalValue() * input.Direction;
                transform.Position += new float3(delta.x, 0, delta.y);
            }
        }
    }
}