using System;
using System.Runtime.InteropServices;
using Core;
using Unity.Entities;
using Unity.NetCode;

namespace Mine.ClientServer
{
    [StructLayout(LayoutKind.Sequential)]
    public struct IntState : IComponentData
    {
        [GhostField] public int Value;
    
        public T ToEnum<T>() where T : unmanaged, Enum
        {
            return Value.ReinterpretCast<int, T>();
        }

        public static IntState FromEnum<T>(T ready) where T : unmanaged, Enum
        {
            var value = ready.ReinterpretCast<T, int>();
            return new IntState()
            {
                Value = value
            };
        }
    }
}