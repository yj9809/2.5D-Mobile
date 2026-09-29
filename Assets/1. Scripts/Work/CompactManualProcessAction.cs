using UnityEngine;

[DisallowMultipleComponent]
public sealed class CompactManualProcessAction : WorkAction
{
    [SerializeField] private CompactManualStation station;
    public CompactManualStation Station => station;

    public override void Enter(GameObject actor)
    {
        if (actor.TryGetComponent<Player>(out var player))
        {
            station?.SetOperator(player, true);
            CompactProgressEvents.Raise(CompactProgressAction.ProcessingStarted);
        }
        else if (actor.TryGetComponent<Employee>(out var employee))
            station?.SetOperator(employee, true);
    }

    public override void Stay(GameObject actor)
    {
        if (actor.TryGetComponent<Player>(out var player))
            station?.SetOperator(player, true);
        else if (actor.TryGetComponent<Employee>(out var employee))
            station?.SetOperator(employee, true);
    }

    public override void Exit(GameObject actor)
    {
        if (actor.TryGetComponent<Player>(out var player))
            station?.SetOperator(player, false);
        else if (actor.TryGetComponent<Employee>(out var employee))
            station?.SetOperator(employee, false);
    }
}
