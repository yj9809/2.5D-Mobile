using UnityEngine;

public enum OnboardingTargetType
{
    SupplyPickup,
    ManualInput,
    ManualWork,
    ManualOutput,
    SalesPallet
}

[DisallowMultipleComponent]
public sealed class OnboardingTargetMarker : MonoBehaviour
{
    [SerializeField] private OnboardingTargetType targetType;
    public OnboardingTargetType TargetType => targetType;

    public void Configure(OnboardingTargetType value) => targetType = value;
}
