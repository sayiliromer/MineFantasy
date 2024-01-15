using Unity.Entities;

namespace Mine.Core
{
    public static class EcsUtility
    {
        public static World GetWorld(WorldFlags worldFlag)
        {
            foreach (var world in World.All)
            {
                if ((world.Flags & worldFlag) == worldFlag) return world;
            }

            return null;
        }

        public static World GetWorld(string name)
        {
            foreach (var world in World.All)
            {
                if (world.Name == name) return world;
            }

            return null;
        }
    }
}
