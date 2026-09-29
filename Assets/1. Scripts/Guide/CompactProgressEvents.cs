using System;

public enum CompactProgressAction
{
    IngredientCollected,
    IngredientPlaced,
    ProcessingStarted,
    ProductCompleted,
    ProductCollected,
    ProductStocked,
    ProductSold
}

public static class CompactProgressEvents
{
    public static event Action<CompactProgressAction> ActionCompleted;
    public static void Raise(CompactProgressAction action) => ActionCompleted?.Invoke(action);
}
