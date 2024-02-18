using System.Runtime.InteropServices;
using Core;
using Unity.Entities;
using Unity.NetCode;
// ReSharper disable UnassignedField.Global

namespace Mine.ClientServer
{
    #region Core
    public interface IDynamicStatComponent : IStatComponent
    {
    }

    public interface IStatComponent : IComponentData
    {
    }

    /// <summary>
    /// Used as wrapper for other stats to make it easy to calculate final value
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct FloatStat
    {
        [GhostField(SendData = false)] 
        public float Base;
        public float Add;
        public float Mul;

        public FloatStat(float baseValue)
        {
            Base = baseValue;
            Add = 0;
            Mul = 0;
        }

        public readonly float ToFinalValue()
        {
            return (Base + Add) * (1f + Mul);
        }

        public T ToStatComp<T>() where T : unmanaged, IStatComponent
        {
            return this.ReinterpretCast<FloatStat, T>();
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DynamicFloatStat
    {
        [GhostField(SendData = false)]
        public float Base;
        public float Add;
        public float Mul;
        public float Current;
        public DynamicFloatStat(float baseValue)
        {
            Base = baseValue;
            Current = baseValue;
            Add = 0;
            Mul = 0;
        }
        public readonly float ToFinalValue()
        {
            return (Base + Add) * (1f + Mul);
        }
        
        public T ToStatComp<T>() where T : unmanaged, IDynamicStatComponent
        {
            return this.ReinterpretCast<DynamicFloatStat, T>();
        }
    }
    #endregion
    
    #region StatComponents

    [StructLayout(LayoutKind.Sequential)]
    public struct Strength : IStatComponent
    {
        [GhostField]
        public FloatStat Value;

        public Strength(float value)
        {
            Value = new FloatStat(value);
        }
    }
    
    [StructLayout(LayoutKind.Sequential)]
    public struct IntelligenceStat : IStatComponent
    {
        [GhostField]
        public FloatStat Value;
        
        public IntelligenceStat(float value)
        {
            Value = new FloatStat(value);
        }
    }
    
    [StructLayout(LayoutKind.Sequential)]
    public struct AgilityStat : IStatComponent
    {
        [GhostField]
        public FloatStat Value;

        public AgilityStat(float value)
        {
            Value = new FloatStat(value);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct HealthStat : IDynamicStatComponent
    {
        [GhostField]
        public DynamicFloatStat Value;

        public HealthStat(float value) : this()
        {
            Value = new DynamicFloatStat(value);
        }
    }
    
    [StructLayout(LayoutKind.Sequential)]
    public struct ShieldStat : IDynamicStatComponent
    {
        [GhostField]
        public DynamicFloatStat Value;
        
        public ShieldStat(float value)
        {
            Value = new DynamicFloatStat(value);
        }
    }
    
    [StructLayout(LayoutKind.Sequential)]
    public struct ManaStat : IDynamicStatComponent
    {
        [GhostField]
        public DynamicFloatStat Value;

        public ManaStat(float value)
        {
            Value = new DynamicFloatStat(value);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MoveSpeedStat : IStatComponent
    {
        [GhostField]
        public FloatStat Value;

        public MoveSpeedStat(float baseValue) : this()
        {
            Value = new FloatStat()
            {
                Base = baseValue
            };
        }
    }

    #endregion
}