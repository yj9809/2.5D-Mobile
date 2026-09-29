using Churub.Core;
using NUnit.Framework;

public sealed class GameDataStateTests
{
    [Test]
    public void Defaults_PreserveReleasedGameBalance()
    {
        var state = new GameDataState();

        Assert.That(state.PlayerSpeed, Is.EqualTo(5f));
        Assert.That(state.PlayerCartSpeed, Is.EqualTo(2.5f));
        Assert.That(state.PlayerMaxStackCount, Is.EqualTo(3f));
        Assert.That(state.PlayerGold, Is.EqualTo(100f));
        Assert.That(state.PlayerGoldPerBox, Is.EqualTo(50f));
        Assert.That(state.EmployeeSpeed, Is.EqualTo(3f));
        Assert.That(state.EmployeeCartSpeed, Is.EqualTo(1.5f));
        Assert.That(state.EmployeeMaxStackCount, Is.EqualTo(3f));
        Assert.That(state.GetFacilityInvestment(GameDataSchema.Progress.Office), Is.Zero);
        Assert.That(state.newGame, Is.True);
    }

    [Test]
    public void TypedProperties_WriteToLegacyDictionaryKeys()
    {
        var state = new GameDataState
        {
            PlayerGold = 250f,
            EmployeeSpeed = 4.5f,
            TruckBoxCount = 7,
            EmployeeAddCount = 2
        };

        Assert.That(state.playerData["gold"], Is.EqualTo(250f));
        Assert.That(state.employeeData["employeeSpeed"], Is.EqualTo(4.5f));
        Assert.That(state.objectData["truckBoxStackCount"], Is.EqualTo(7));
        Assert.That(state.upgradeCosts["baseEmployeeAddCount"], Is.EqualTo(2));
    }

    [Test]
    public void LegacyDictionaryValues_AreVisibleThroughTypedProperties()
    {
        var state = new GameDataState();
        state.playerData["gold"] = 999f;
        state.objectData["packagingWaitObjCount"] = 4;

        Assert.That(state.PlayerGold, Is.EqualTo(999f));
        Assert.That(state.PackagingWaitCount, Is.EqualTo(4));
    }

    [Test]
    public void UnlockAccessors_UseExistingProgressSchema()
    {
        var state = new GameDataState();

        state.SetUnlocked(GameDataSchema.Progress.Office, true);

        Assert.That(state.IsUnlocked(GameDataSchema.Progress.Office), Is.True);
        Assert.That(state.gameProgressBool["Office"], Is.True);
        Assert.That(state.IsUnlocked("UnknownProgress"), Is.False);
    }

    [Test]
    public void FacilityInvestment_PersistsByProgressKeyAndClampsInvalidValues()
    {
        var state = new GameDataState();

        state.SetFacilityInvestment(GameDataSchema.Progress.Office, 125);
        state.SetFacilityInvestment(GameDataSchema.Progress.Container1, -10);

        Assert.That(state.GetFacilityInvestment(GameDataSchema.Progress.Office), Is.EqualTo(125));
        Assert.That(state.GetFacilityInvestment(GameDataSchema.Progress.Container1), Is.Zero);
        Assert.That(BalanceTable.FacilityRemainingCost(state, GameDataSchema.Progress.Office), Is.EqualTo(175));
    }

    [Test]
    public void FacilityInvestment_NeverExceedsFacilityCostWhenReadByGameplay()
    {
        var state = new GameDataState();
        state.SetFacilityInvestment(GameDataSchema.Progress.Office, 999);

        Assert.That(BalanceTable.FacilityInvestment(state, GameDataSchema.Progress.Office), Is.EqualTo(300));
        Assert.That(BalanceTable.FacilityRemainingCost(state, GameDataSchema.Progress.Office), Is.Zero);
    }

    [Test]
    public void BackendSchemaConstants_PreserveReleasedFieldNames()
    {
        Assert.That(GameDataSchema.TableName, Is.EqualTo("TestUserData"));
        Assert.That(GameDataSchema.Fields.GuestId, Is.EqualTo("guestID"));
        Assert.That(GameDataSchema.Fields.GameProgress, Is.EqualTo("gameProgressBool"));
        Assert.That(GameDataSchema.Fields.FacilityInvestments, Is.EqualTo("facilityInvestments"));
    }

    [Test]
    public void CompactFactorySaveFields_DefaultToEmptyWithoutChangingLegacyCounts()
    {
        var state = new GameDataState();
        state.objectData[GameDataSchema.Objects.PackagingWaitCount] = 4;

        Assert.That(state.objectData[GameDataSchema.Objects.CompactSupplyCount], Is.Zero);
        Assert.That(state.objectData[GameDataSchema.Objects.CompactInputCount], Is.Zero);
        Assert.That(state.objectData[GameDataSchema.Objects.CompactOutputCount], Is.Zero);
        Assert.That(state.objectData[GameDataSchema.Objects.CompactSalesCount], Is.Zero);
        Assert.That(state.objectData[GameDataSchema.Objects.CompactCarryCount], Is.Zero);
        Assert.That(state.objectData[GameDataSchema.Objects.CompactCarryType], Is.EqualTo(-1));
        Assert.That(state.PackagingWaitCount, Is.EqualTo(4));
    }

    [Test]
    public void VersionedIncrementalProgress_DefaultsToFreshOnboarding()
    {
        var state = new GameDataState();

        Assert.That(state.onboardingVersion, Is.EqualTo(IncrementalProgress.CurrentOnboardingVersion));
        Assert.That(state.onboardingStep, Is.Zero);
        Assert.That(state.onboardingCompleted, Is.False);
        Assert.That(state.upgradeGraphVersion, Is.EqualTo(IncrementalProgress.CurrentUpgradeGraphVersion));
    }

    [Test]
    public void LegacySaveMigration_DoesNotForceOnboardingOnExistingPlayer()
    {
        var state = new GameDataState { onboardingStep = 0, onboardingCompleted = false };

        IncrementalProgress.Migrate(state, hadOnboardingFields: false);

        Assert.That(state.onboardingCompleted, Is.True);
        Assert.That(state.onboardingStep, Is.EqualTo(IncrementalProgress.OnboardingStepCount));
    }
}
