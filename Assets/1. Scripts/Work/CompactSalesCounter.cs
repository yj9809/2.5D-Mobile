using System.Collections.Generic;
using Churub.Core;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

// Customers buy only products that the player has placed on the display.
public sealed class CompactSalesCounter : MonoBehaviour, IItemTransferEndpoint, IObjectDataSave
{
    private const int ItemsPerLayer = 6;
    private const int ColumnsPerLayer = 3;

    [SerializeField] private Transform stockAnchor;
    [Min(1)] [SerializeField] private int capacity = 100;
    private ItemBuffer stock;
    private CompactSupplyStation supply;
    private CompactManualStation station;
    private Player player;
    private BaseCost saveState;
    public int StockCount => stock == null ? 0 : stock.Count;
    public int StockCapacity => stock == null ? Mathf.Max(1, capacity) : stock.Capacity;
    public NavMeshSurface EmployeeNavigationSurface { get; private set; }
    public bool NavigationReady { get; private set; }

    private void Awake()
    {
        stock = new ItemBuffer(Mathf.Max(1, capacity), ItemType.Churu);
    }

    private void Start()
    {
        supply = FindObjectOfType<CompactSupplyStation>();
        station = FindObjectOfType<CompactManualStation>();
        player = GameManager.Instance.P;
        saveState = DataManager.Instance.baseCost;
        if (supply == null || station == null || player == null || saveState == null)
        {
            Debug.LogError("Compact save participants are incomplete.", this);
            return;
        }
        RestoreState();
        DataManager.Instance.AddObjStackCountList(this);

        var legacyStoreInteraction = FindObjectOfType<StoreInteraction>();
        if (legacyStoreInteraction != null)
            legacyStoreInteraction.enabled = false;

        var spawner = FindObjectOfType<SpawnPoint>();
        var entry = GameObject.Find("Future Customer Entry");
        var queue = GameObject.Find("Customer Queue");
        var exit = GameObject.Find("Future Customer Exit");
        if (spawner != null) spawner.enabled = false;
        if (spawner == null || entry == null || queue == null || exit == null)
        {
            Debug.LogError("Compact customer route is incomplete.", this);
            return;
        }
        var sidewalk = GameObject.Find("Customer Sidewalk");
        if (sidewalk == null || sidewalk.GetComponent<MeshFilter>() == null)
        {
            Debug.LogError("Compact customer sidewalk is missing.", this);
            return;
        }

        if (!BuildNavigation())
        {
            Debug.LogError("Compact navigation could not be built.", this);
            return;
        }
        spawner.ConfigureCompactSales(this, entry.transform, queue.transform, exit.transform);
    }

    public bool BuildNavigation()
    {
        var workshop = GameObject.Find("01 Starter Workshop - 10 x 10 m");
        var sidewalk = GameObject.Find("Customer Sidewalk");
        if (workshop == null)
        {
            Debug.LogError("Compact employee workshop is missing.", this);
            NavigationReady = false;
            return false;
        }
        if (sidewalk == null || sidewalk.GetComponent<MeshFilter>() == null)
        {
            Debug.LogError("Compact customer sidewalk is missing.", this);
            NavigationReady = false;
            return false;
        }

        EmployeeNavigationSurface = workshop.GetComponent<NavMeshSurface>();
        if (EmployeeNavigationSurface == null)
            EmployeeNavigationSurface = workshop.AddComponent<NavMeshSurface>();
        ConfigureRuntimeSurface(EmployeeNavigationSurface, ~0);
        EmployeeNavigationSurface.agentTypeID = 0;
        EmployeeNavigationSurface.BuildNavMesh();

        var customerSurface = sidewalk.GetComponent<NavMeshSurface>();
        if (customerSurface == null) customerSurface = sidewalk.AddComponent<NavMeshSurface>();
        ConfigureRuntimeSurface(customerSurface, 1 << sidewalk.layer);
        customerSurface.agentTypeID = 0;
        customerSurface.BuildNavMesh();

        NavigationReady = NavMesh.SamplePosition(transform.position, out _, 12f, NavMesh.AllAreas);
        if (!NavigationReady)
            Debug.LogError("Compact employee NavMesh has no walkable area near the factory.", this);
        return NavigationReady;
    }

    internal static void ConfigureRuntimeSurface(NavMeshSurface surface, int layerMask)
    {
        if (surface == null) return;
        surface.collectObjects = CollectObjects.Children;
        surface.layerMask = layerMask;
        // Imported decoration meshes are not guaranteed to be CPU-readable in a Player.
        // Navigation geometry is represented by the scene's non-trigger colliders.
        surface.useGeometry = NavMeshCollectGeometry.PhysicsColliders;
    }

    public void ObjectDataSave()
    {
        if (saveState == null || supply == null || station == null || player == null)
            return;
        var values = saveState.objectData;
        CountEmployeeTransit(out int ingredientTransit, out int productTransit);
        values[GameDataSchema.Objects.CompactSupplyCount] =
            Mathf.Min(supply.StockCapacity, supply.SavedStockCount + ingredientTransit);
        values[GameDataSchema.Objects.CompactInputCount] = station.SavedInputCount;
        values[GameDataSchema.Objects.CompactOutputCount] =
            Mathf.Min(station.OutputCapacity, station.OutputCount + productTransit);
        values[GameDataSchema.Objects.CompactSalesCount] = StockCount;
        var carry = player.Inventory;
        values[GameDataSchema.Objects.CompactCarryCount] = carry.Count;
        values[GameDataSchema.Objects.CompactCarryType] = carry.TryPeek(out var item)
            && item != null ? (int)item.Type : -1;
    }

    private static void CountEmployeeTransit(out int ingredientCount, out int productCount)
    {
        ingredientCount = 0;
        productCount = 0;
        foreach (var employee in FindObjectsByType<Employee>(
            FindObjectsInactive.Exclude, FindObjectsSortMode.None))
        {
            var inventory = employee.Inventory;
            if (inventory.ContainsType(ItemType.Ingredient))
                ingredientCount += inventory.Count;
            else if (inventory.ContainsType(ItemType.Churu))
                productCount += inventory.Count;
        }
    }

    private void RestoreState()
    {
        var values = saveState.objectData;
        supply.RestoreStock(ReadCount(values, GameDataSchema.Objects.CompactSupplyCount));
        station.RestoreState(ReadCount(values, GameDataSchema.Objects.CompactInputCount),
            ReadCount(values, GameDataSchema.Objects.CompactOutputCount), supply.IngredientPrefab);
        RestoreSales(ReadCount(values, GameDataSchema.Objects.CompactSalesCount));
        RestoreCarry(ReadCount(values, GameDataSchema.Objects.CompactCarryCount),
            values.TryGetValue(GameDataSchema.Objects.CompactCarryType, out int type) ? type : -1);
    }

    private static int ReadCount(Dictionary<string, int> values, string key) =>
        values.TryGetValue(key, out int count) ? Mathf.Max(0, count) : 0;

    private void RestoreSales(int count)
    {
        for (int i = 0; i < Mathf.Min(count, stock.Capacity); i++)
        {
            if (stockAnchor == null) break;
            var spawned = PoolingManager.Instance.GetObj(station.ProductPrefab);
            if (spawned != null && spawned.TryGetComponent<Item>(out var item)
                && stock.TryAdd(item))
            {
                PlaceItem(item, i);
                continue;
            }
            PoolingManager.Instance.ReturnObjecte(spawned);
            Debug.LogError("Cannot restore compact sales stock.", this);
            break;
        }
    }

    private void RestoreCarry(int count, int type)
    {
        GameObject prefab = type == (int)ItemType.Ingredient ? supply.IngredientPrefab :
            type == (int)ItemType.Churu ? station.ProductPrefab : null;
        if (prefab == null) return;
        var carry = player.Inventory;
        for (int i = 0; i < Mathf.Min(count, carry.Capacity); i++)
        {
            var spawned = PoolingManager.Instance.GetObj(prefab);
            if (spawned != null && spawned.TryGetComponent<Item>(out var item)
                && ItemTransferUtility.TryCollect(item, carry, player.CarryParent, animate: false))
                continue;
            PoolingManager.Instance.ReturnObjecte(spawned);
            Debug.LogError("Cannot restore compact carried items.", this);
            break;
        }
    }

    public bool TrySellOne()
    {
        if (stock == null || !stock.TryPop(out var item) || item == null)
            return false;

        PoolingManager.Instance.ReturnObjecte(item.gameObject);
        UIManager.Instance.AddGold(BalanceTable.ProductSaleIncome(saveState, player.buffGold));
        CompactTelemetryEvents.Record(CompactTelemetryMetric.ProductSold);
        CompactProgressEvents.Raise(CompactProgressAction.ProductSold);
        if (!saveState.IsUnlocked(BalanceTable.FirstSaleKey))
        {
            saveState.SetUnlocked(BalanceTable.FirstSaleKey, true);
            DataManager.Instance.GameDataUpdate();
        }
        return true;
    }

    public bool TryTransfer(CarrierInventory inventory, Transform carryParent)
    {
        if (inventory == null || stock == null || stock.IsFull)
            return false;

        if (stockAnchor == null) return false;
        int slot = stock.Count;
        if (!inventory.TryMoveTo(stock, out var item))
            return false;

        PlaceItem(item, slot);
        return true;
    }

    private void PlaceItem(Item item, int slot)
    {
        item.transform.DOKill();
        if (item.TryGetComponent<Rigidbody>(out var body)) Destroy(body);
        item.transform.SetParent(stockAnchor, false);
        float bottomOffset = item.TryGetComponent<BoxCollider>(out var box)
            ? box.size.y * .5f - box.center.y : 0f;
        int positionInLayer = slot % ItemsPerLayer;
        int layer = slot / ItemsPerLayer;
        float layerHeight = box != null ? box.size.y : .08f;
        item.transform.localPosition = new Vector3(
            (positionInLayer % ColumnsPerLayer - 1) * .23f,
            bottomOffset + layer * layerHeight,
            positionInLayer / ColumnsPerLayer == 0 ? -.16f : .16f);
        item.transform.localRotation = Quaternion.identity;
        item.transform.localScale = Vector3.one;
    }
}
