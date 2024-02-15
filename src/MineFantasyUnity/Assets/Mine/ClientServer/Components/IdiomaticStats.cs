using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Core;
using Unity.Entities;
using Unity.NetCode;

namespace Mine.ClientServer
{
    #region Core
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
    #endregion
    #region StatComponents

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

    #endregion
}