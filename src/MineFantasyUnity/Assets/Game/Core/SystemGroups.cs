using Unity.Entities;
using Unity.NetCode;

namespace Mine.Core
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    [WorldSystemFilter(WorldFilters.ClientServer)]
    public partial class InitGroup : ComponentSystemGroup { }

    [UpdateInGroup(typeof(PresentationSystemGroup))]
    [WorldSystemFilter(WorldFilters.ClientServer)]
    public partial class ViewGroup : ComponentSystemGroup { }

    [UpdateInGroup(typeof(GhostInputSystemGroup))]
    [WorldSystemFilter(WorldFilters.Client)]
    public partial class InputGroup : ComponentSystemGroup { }

    [UpdateInGroup(typeof(PredictedSimulationSystemGroup))]
    [WorldSystemFilter(WorldFilters.ClientServer)]
    public partial class PredictedSimulationGroup : ComponentSystemGroup { }

    [UpdateInGroup(typeof(GhostSimulationSystemGroup))]
    [WorldSystemFilter(WorldFilters.ClientServer)]
    public partial class SimulationGroup : ComponentSystemGroup { }
}
