using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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

    public interface IDynamicStatComponent : IStatComponent
    {
    }

    public interface IStatComponent : IComponentData
    {
    }

    public static class StatExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Stat ToStat<T>(this T iStat) where T : unmanaged, IStatComponent
        {
            return iStat.ReinterpretCast<T, Stat>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ToFinalValue<T>(this T iStat) where T : unmanaged, IStatComponent
        {
            var stat = iStat.ReinterpretCast<T, Stat>();
            return (stat.Base + stat.Add) * (1f + stat.Mul);
        }
    }

    /// <summary>
    /// Used as wrapper for other stats to make it easy to calculate final value
    /// </summary>
    public struct Stat
    {
        public float Base;
        public float Add;
        public float Mul;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Health : IDynamicStatComponent
    {
        public float BaseValue;
        [GhostField] public float Add, Mul;
        [GhostField] public float Current;

        public Health(float baseValue) : this()
        {
            BaseValue = baseValue;
            Current = baseValue;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct HealthRegen : IStatComponent
    {
        public float BaseValue;
        [GhostField] public float Add, Mul;

        public HealthRegen(float baseValue) : this()
        {
            BaseValue = baseValue;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MoveSpeed : IStatComponent
    {
        public float BaseValue;
        [GhostField] public float Add, Mul;

        public MoveSpeed(float baseValue) : this()
        {
            BaseValue = baseValue;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Luck : IStatComponent
    {
        public float BaseValue;
        [GhostField] public float Add, Mul;

        public Luck(float baseValue) : this()
        {
            BaseValue = baseValue;
        }
    }
}