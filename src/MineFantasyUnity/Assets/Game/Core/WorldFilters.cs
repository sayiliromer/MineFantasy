using Unity.Entities;

namespace Mine.Core
{
    public static class WorldFilters
    {
        public const WorldSystemFilterFlags NoThinClient = WorldSystemFilterFlags.ClientSimulation;
        public const WorldSystemFilterFlags Client = WorldSystemFilterFlags.ClientSimulation | WorldSystemFilterFlags.ThinClientSimulation;
        public const WorldSystemFilterFlags Server = WorldSystemFilterFlags.ServerSimulation;
        public const WorldSystemFilterFlags ClientServer = WorldSystemFilterFlags.ClientSimulation | WorldSystemFilterFlags.ServerSimulation | WorldSystemFilterFlags.ThinClientSimulation;
        public const WorldSystemFilterFlags NoThinClientServer = WorldSystemFilterFlags.ClientSimulation | WorldSystemFilterFlags.ServerSimulation;
    }
}
