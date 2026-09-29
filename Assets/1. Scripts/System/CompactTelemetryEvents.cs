using System;

public enum CompactTelemetryMetric
{
    IngredientProduced,
    ProductProcessed,
    ProductSold,
    GoldEarned
}

public static class CompactTelemetryEvents
{
    public static event Action<CompactTelemetryMetric, double> MetricRecorded;

    public static void Record(CompactTelemetryMetric metric, double amount = 1d)
    {
        if (amount <= 0d || double.IsNaN(amount) || double.IsInfinity(amount)) return;
        MetricRecorded?.Invoke(metric, amount);
    }
}
