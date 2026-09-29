using UnityEngine;

[DisallowMultipleComponent]
public sealed class UpgradeInteraction : WorkAction
{
    private UIManager ui;

    public override void Enter(GameObject actor)
    {
        if (actor.TryGetComponent<Player>(out _))
        {
            ui = UIManager.Instance;
            if (ui != null)
                ui.ShowUpgradeUI();
        }
    }

    // Opening on Stay would immediately reopen the panel after the player closes it.
    // Re-entering the work point is the explicit action that opens it again.
    public override void Stay(GameObject actor) { }

    public override void Exit(GameObject actor)
    {
        if (actor.TryGetComponent<Player>(out _) && ui != null)
            ui.CloseUpgradeUI();
    }
}
