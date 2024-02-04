using Mine.Core;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.NetCode;
using Unity.Transforms;

namespace Mine.ClientServer
{
    public struct Unit : IComponentData { }

    public struct UnitInput : IInputComponentData
    {
        [GhostField] public float2 Direction;
        [GhostField] public float2 TargetPosition;   
    }
    
    public readonly partial struct UnitAspect : IAspect
    {
        private readonly DynamicBuffer<FloatDynamicStat> _dynamicStats;
        private readonly DynamicBuffer<FloatStaticStat> _staticStats;
        private readonly RefRO<HealthMarker> _health;
        private readonly RefRO<MoveSpeedMarker> _moveSpeed;

        public float Health
        {
            get => _dynamicStats[_health.ValueRO.DynamicIndex].Value;
            set => _dynamicStats.ElementAt(_health.ValueRO.DynamicIndex).Value = value;
        }
        
        public float MaxHealth => _staticStats[_health.ValueRO.StaticIndex].FinalValue;
        public float MoveSpeed => _staticStats[_moveSpeed.ValueRO.StaticIndex].FinalValue;
    }

    public struct HealthMarker : IDynamicStatMarker
    {
        public int StaticIndex;
        public int DynamicIndex;
    }
    
    public struct MoveSpeedMarker : IStaticStatMarker
    {
        public int StaticIndex;
    }
    
    [BurstCompile]
    [UpdateInGroup(typeof(PredictedSimulationGroup))]
    public partial struct WalkerApplySystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var dt = SystemAPI.Time.DeltaTime;
            
            state.Dependency = new WalkerApplyJob()
            {
                Dt = dt
            }.ScheduleParallel(state.Dependency);
        }
        
        [BurstCompile]
        [WithAll(typeof(Simulate))]
        public partial struct WalkerApplyJob : IJobEntity
        {
            public float Dt;
            
            public void Execute(UnitAspect unit, in UnitInput input, ref LocalTransform transform)
            {
                var delta = Dt * unit.MoveSpeed * input.Direction;
                transform.Position += new float3(delta.x, 0, delta.y);
            }
        }
    }
}
