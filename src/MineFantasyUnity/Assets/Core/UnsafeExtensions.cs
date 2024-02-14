namespace Core
{
    public static class UnsafeExtensions
    {
        public static unsafe TResult ReinterpretCast<TResult>(this int original) where TResult : unmanaged
        {
            return *(TResult*)&original;
        }

        public static unsafe TResult ReinterpretCast<TOriginal, TResult>(this TOriginal original)
            where TOriginal : unmanaged where TResult : unmanaged
        {
            return *(TResult*)&original;
        }
    }
}