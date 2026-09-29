using System;
using System.Collections.Generic;
using System.Linq;

namespace Churub.Core
{
    public enum UpgradeNodeState { Hidden, Locked, Purchasable, Purchased, Maxed }
    public enum UpgradeNodeCategory { Manual, Sales, Automation, Transport }

    public sealed class UpgradeNodeDefinition
    {
        public UpgradeNodeDefinition(string id, string displayName, string description, string icon,
            UpgradeNodeCategory category, UpgradeType upgradeType, float x, float y,
            params string[] requiredNodeIds)
        {
            Id = id;
            DisplayName = displayName;
            Description = description;
            Icon = icon;
            Category = category;
            UpgradeType = upgradeType;
            X = x;
            Y = y;
            RequiredNodeIds = requiredNodeIds ?? Array.Empty<string>();
        }

        public string Id { get; }
        public string DisplayName { get; }
        public string Description { get; }
        public string Icon { get; }
        public UpgradeNodeCategory Category { get; }
        public UpgradeType UpgradeType { get; }
        public float X { get; }
        public float Y { get; }
        public IReadOnlyList<string> RequiredNodeIds { get; }
    }

    public static class UpgradeGraphCatalog
    {
        public static IReadOnlyList<UpgradeNodeDefinition> CreateDefault() =>
            new[]
            {
                new UpgradeNodeDefinition("manual.speed", "직접 작업 속도", "플레이어 이동 속도를 높입니다.", "⚡", UpgradeNodeCategory.Manual, UpgradeType.PlayerSpeed, -300, 120),
                new UpgradeNodeDefinition("sales.price", "판매 수익", "제품 한 개의 판매 수익을 높입니다.", "₩", UpgradeNodeCategory.Sales, UpgradeType.GoldPerBox, 300, 120),
                new UpgradeNodeDefinition("manual.capacity", "운반 용량", "플레이어가 한 번에 운반하는 수량을 늘립니다.", "▣", UpgradeNodeCategory.Manual, UpgradeType.PlayerMaxStack, -300, -80, "manual.speed"),
                new UpgradeNodeDefinition("automation.employee", "첫 직원", "직원을 고용해 운반 자동화를 시작합니다.", "♟", UpgradeNodeCategory.Automation, UpgradeType.EmployeeAdd, 0, -280, "manual.capacity", "sales.price"),
                new UpgradeNodeDefinition("transport.speed", "직원 이동 속도", "직원의 이동 속도를 높입니다.", "▶", UpgradeNodeCategory.Transport, UpgradeType.EmployeeSpeed, -260, -480, "automation.employee"),
                new UpgradeNodeDefinition("transport.capacity", "직원 운반량", "직원이 한 번에 운반하는 수량을 늘립니다.", "▤", UpgradeNodeCategory.Transport, UpgradeType.EmployeeMaxStack, 260, -480, "automation.employee")
            };
    }

    public sealed class UpgradeGraphService
    {
        private readonly GameDataState state;
        private readonly UpgradeService upgrades;
        private readonly Dictionary<string, UpgradeNodeDefinition> definitions;

        public UpgradeGraphService(GameDataState state,
            IEnumerable<UpgradeNodeDefinition> definitions = null)
        {
            this.state = state ?? throw new ArgumentNullException(nameof(state));
            upgrades = new UpgradeService(state);
            this.definitions = (definitions ?? UpgradeGraphCatalog.CreateDefault())
                .ToDictionary(node => node.Id, StringComparer.Ordinal);
            Synchronize();
        }

        public IEnumerable<UpgradeNodeDefinition> Definitions => definitions.Values;

        public UpgradeNodeDefinition GetDefinition(string id) =>
            id != null && definitions.TryGetValue(id, out var node) ? node : null;

        public UpgradeNodeState GetState(string id)
        {
            UpgradeNodeDefinition node = GetDefinition(id);
            if (node == null || !state.revealedUpgradeNodes.Contains(id)) return UpgradeNodeState.Hidden;
            UpgradeProgress progress = upgrades.GetProgress(node.UpgradeType);
            if (progress.IsMaxLevel) return UpgradeNodeState.Maxed;
            if (progress.Level > 0) return UpgradeNodeState.Purchased;
            if (!PrerequisitesMet(node)) return UpgradeNodeState.Locked;
            return upgrades.EvaluatePurchase(node.UpgradeType) == UpgradePurchaseStatus.Success
                ? UpgradeNodeState.Purchasable
                : UpgradeNodeState.Locked;
        }

        public UpgradePurchaseStatus EvaluatePurchase(string id)
        {
            UpgradeNodeDefinition node = GetDefinition(id);
            if (node == null) return UpgradePurchaseStatus.InvalidUpgrade;
            UpgradeNodeState nodeState = GetState(id);
            if (nodeState == UpgradeNodeState.Hidden || nodeState == UpgradeNodeState.Locked)
                return UpgradePurchaseStatus.Locked;
            return upgrades.EvaluatePurchase(node.UpgradeType);
        }

        public UpgradeProgress GetProgress(string id)
        {
            UpgradeNodeDefinition node = GetDefinition(id) ??
                throw new ArgumentException("Unknown upgrade node.", nameof(id));
            return upgrades.GetProgress(node.UpgradeType);
        }

        public void NotifyPurchaseCommitted(string id)
        {
            UpgradeNodeDefinition node = GetDefinition(id);
            if (node == null) return;
            state.upgradeNodeLevels[id] = upgrades.GetProgress(node.UpgradeType).Level;
            RevealChildren(id);
        }

        public bool Synchronize()
        {
            bool changed = IncrementalProgress.Migrate(state);
            foreach (var node in definitions.Values)
            {
                int legacyLevel = upgrades.GetProgress(node.UpgradeType).Level;
                if (!state.upgradeNodeLevels.TryGetValue(node.Id, out int saved) || saved != legacyLevel)
                {
                    state.upgradeNodeLevels[node.Id] = legacyLevel;
                    changed = true;
                }
                if (node.RequiredNodeIds.Count == 0)
                    changed |= Reveal(node.Id);
                if (legacyLevel > 0)
                {
                    changed |= Reveal(node.Id);
                    changed |= RevealChildren(node.Id);
                }
            }
            return changed;
        }

        private bool PrerequisitesMet(UpgradeNodeDefinition node) => node.RequiredNodeIds.All(id =>
            definitions.TryGetValue(id, out var required) &&
            upgrades.GetProgress(required.UpgradeType).Level > 0);

        private bool RevealChildren(string purchasedId)
        {
            bool changed = false;
            foreach (var child in definitions.Values.Where(node => node.RequiredNodeIds.Contains(purchasedId)))
                changed |= Reveal(child.Id);
            return changed;
        }

        private bool Reveal(string id)
        {
            if (state.revealedUpgradeNodes.Contains(id)) return false;
            state.revealedUpgradeNodes.Add(id);
            return true;
        }
    }
}
