using Core;
using Unity.Burst;
using Unity.Entities;
using Unity.NetCode;

namespace Mine.ClientServer
{
    [WorldSystemFilter(WorldFilters.ClientServer)]
    [UpdateInGroup(typeof(PredictedSimulationSystemGroup))]
    public partial struct HealthSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var dt = SystemAPI.Time.DeltaTime;
            state.Dependency = new HealthRegenJob(dt).ScheduleParallel(state.Dependency);
        }

        [BurstCompile]
        [WithAll(typeof(Simulate))]
        private partial struct HealthRegenJob : IJobEntity
        {
            private readonly float _dt;

            public HealthRegenJob(float dt) : this()
            {
                _dt = dt;
            }

            private void Execute(ref Health health, in HealthRegen regen)
            {
                var maxHealth = health.ToFinalValue();
                var regenAmount = regen.ToFinalValue();
                if (health.Current >= maxHealth) return;
                var delta = regenAmount * _dt;
                health.Current += delta;
                if (health.Current < maxHealth) return;
                health.Current = maxHealth;
            }
        }
    }
}