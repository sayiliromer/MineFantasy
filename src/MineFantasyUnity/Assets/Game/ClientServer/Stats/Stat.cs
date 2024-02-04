using System;
using Unity.Entities;
using Unity.NetCode;

namespace Mine.ClientServer
{
    [InternalBufferCapacity(10)]
    public struct FloatStaticStat : IBufferElementData
    {
        [GhostField]
        public float BaseValue , Add, Mul;
        public float FinalValue => (BaseValue + Add) * (1f + Mul);

        public FloatStaticStat(float baseValue) : this()
        {
            BaseValue = baseValue;
        }
    }

    [InternalBufferCapacity(10)]
    public struct FloatDynamicStat : IBufferElementData
    {
        [GhostField] 
        public float Value;

        public FloatDynamicStat(float value)
        {
            Value = value;
        }
    }
}
