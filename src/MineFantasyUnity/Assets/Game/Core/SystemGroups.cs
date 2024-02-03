using Unity.Entities;
using Unity.NetCode;

namespace Mine.Core
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial class InitGruop : ComponentSystemGroup { }

    [UpdateInGroup(typeof(PresentationSystemGroup))]
    public partial class ViewGroup : ComponentSystemGroup { }

    [UpdateInGroup(typeof(GhostInputSystemGroup))]
    public partial class InputGroup : ComponentSystemGroup { }

    [UpdateInGroup(typeof(PredictedSimulationSystemGroup))]
    public partial class PredictedSimulationGroup : ComponentSystemGroup { }

    [UpdateInGroup(typeof(GhostSimulationSystemGroup))]
    public partial class SimulationGroup : ComponentSystemGroup { }
}
