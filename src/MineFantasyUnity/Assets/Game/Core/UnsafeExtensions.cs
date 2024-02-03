namespace Mine.Core
{
    public static class UnsafeExtensions
    {
        public unsafe static TResult ReinterpretCast<TResult>(this int original) where TResult : unmanaged
        {
            return *(TResult*)(void*)&original;
        }

        public unsafe static TResult ReinterpretCast<TOriginal, TResult>(this TOriginal original) where TOriginal : unmanaged where TResult : unmanaged
        {
            return *(TResult*)(void*)&original;
        }
    }
}
