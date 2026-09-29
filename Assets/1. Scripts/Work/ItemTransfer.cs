using UnityEngine;

[DisallowMultipleComponent]
public sealed class ItemTransfer : WorkAction
{
    [SerializeField] private MonoBehaviour endpoint;
    [SerializeField] private bool playerOnly;

    public MonoBehaviour Endpoint => endpoint;

    public override void Stay(GameObject actor)
    {
        if (endpoint == null || !endpoint.isActiveAndEnabled
            || !(endpoint is IItemTransferEndpoint transfer))
            return;

        if (actor.TryGetComponent<Player>(out var player))
        {
            if (transfer.TryTransfer(player.Inventory, player.CarryParent))
            {
                Vibration.VibratePop();
                RaiseCompactProgress();
            }
        }
        else if (!playerOnly && actor.TryGetComponent<Employee>(out var employee))
        {
            // Source pickup still respects the scheduler's reservation and role.
            if (endpoint is IStackable source && !employee.CanCollectFrom(source))
                return;
            transfer.TryTransfer(employee.Inventory, employee.CarryParent);
        }
    }

    private void RaiseCompactProgress()
    {
        if (endpoint is CompactSupplyStation)
            CompactProgressEvents.Raise(CompactProgressAction.IngredientCollected);
        else if (endpoint is CompactManualStation)
            CompactProgressEvents.Raise(CompactProgressAction.IngredientPlaced);
        else if (endpoint is CompactManualOutput)
            CompactProgressEvents.Raise(CompactProgressAction.ProductCollected);
        else if (endpoint is CompactSalesCounter)
            CompactProgressEvents.Raise(CompactProgressAction.ProductStocked);
    }

    private void OnValidate()
    {
        if (endpoint != null && !(endpoint is IItemTransferEndpoint))
            Debug.LogError("ItemTransfer endpoint must implement IItemTransferEndpoint.", this);
    }
}
