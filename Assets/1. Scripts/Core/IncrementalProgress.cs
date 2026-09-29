using System;
using System.Collections.Generic;

namespace Churub.Core
{
    public static class IncrementalProgress
    {
        public const int CurrentOnboardingVersion = 1;
        public const int CurrentUpgradeGraphVersion = 1;
        public const int OnboardingStepCount = 6;

        public static bool Migrate(GameDataState state, bool hadOnboardingFields = true)
        {
            if (state == null) throw new ArgumentNullException(nameof(state));
            bool changed = false;
            state.upgradeNodeLevels ??= new Dictionary<string, int>();
            state.revealedUpgradeNodes ??= new List<string>();

            if (!hadOnboardingFields)
            {
                // Saves written before versioned onboarding belong to existing players.
                state.onboardingCompleted = true;
                state.onboardingStep = OnboardingStepCount;
                changed = true;
            }

            int clampedStep = Math.Max(0, Math.Min(OnboardingStepCount, state.onboardingStep));
            if (clampedStep != state.onboardingStep)
            {
                state.onboardingStep = clampedStep;
                changed = true;
            }
            if (state.onboardingCompleted && state.onboardingStep != OnboardingStepCount)
            {
                state.onboardingStep = OnboardingStepCount;
                changed = true;
            }
            if (state.onboardingVersion != CurrentOnboardingVersion)
            {
                // Completed players stay completed across content revisions; replay is opt-in.
                state.onboardingVersion = CurrentOnboardingVersion;
                changed = true;
            }
            if (state.upgradeGraphVersion != CurrentUpgradeGraphVersion)
            {
                state.upgradeGraphVersion = CurrentUpgradeGraphVersion;
                changed = true;
            }
            return changed;
        }

        public static void RestartOnboarding(GameDataState state)
        {
            state.onboardingVersion = CurrentOnboardingVersion;
            state.onboardingStep = 0;
            state.onboardingCompleted = false;
        }

        public static void CompleteOnboarding(GameDataState state)
        {
            state.onboardingVersion = CurrentOnboardingVersion;
            state.onboardingStep = OnboardingStepCount;
            state.onboardingCompleted = true;
        }
    }
}
