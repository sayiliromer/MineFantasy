using Unity.Entities;
using Unity.NetCode;

namespace Mine.Core
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial class MineInitGruop : ComponentSystemGroup { }

    [UpdateInGroup(typeof(PresentationSystemGroup))]
    public partial class MineViewGroup : ComponentSystemGroup { }

    [UpdateInGroup(typeof(GhostInputSystemGroup))]
    public partial class MineInputGroup : ComponentSystemGroup { }

    [UpdateInGroup(typeof(PredictedSimulationSystemGroup))]
    public partial class MinePredictedSimulationGroup : ComponentSystemGroup { }

    [UpdateInGroup(typeof(GhostSimulationSystemGroup))]
    public partial class MineSimulationGroup : ComponentSystemGroup { }
}
