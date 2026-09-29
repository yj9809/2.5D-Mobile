using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

// Keeps the production scene intact while testing its serialized gameplay
// objects against the compact layout's stage-zero positions.
public static class CompactFactoryPlaytestBuilder
{
    public const string SceneName = "CompactFactory_Playtest";
    public const string ScenePath = "Assets/2. Scene/CompactFactory_Playtest.unity";
    public const string EditorPrefKey = "Churub.CompactFactory.UsePlaytest";
    const string GamePath = "Assets/2. Scene/Game.unity";
    const string LayoutPath = "Assets/2. Scene/CompactFactory_Layout.unity";
    const string WorkIconPath = "Assets/6. 2D Sprite/타이틀 화면/츄릅.png";
    const string SalesPalletPrefabPath =
        "Assets/3. Prefab/Worktable/Pallet/Plallet Mint.prefab";
    static readonly Vector3 SupplySpawnPosition = new Vector3(-3.5f, .49f, 6.45f);
    static readonly Vector3 SupplyBeltAdapterPosition = new Vector3(-3f, 0f, 4.1375756f);
    static readonly Vector3 SupplyBeltAdapterScale = new Vector3(1f, 1f, .40414142f);
    static readonly Vector3 StoreCounterPosition = new Vector3(1.4f, 0f, -4.44f);
    static readonly Vector3 SalesPalletPosition = new Vector3(1.55f, 0f, -3.25f);
    static readonly Vector3 SalesPalletScale = Vector3.one * .65f;
    static readonly Vector3 SalesWorkPointPosition = new Vector3(1.55f, .1f, -3.25f);
    static readonly Vector3 SalesStockPilePosition = new Vector3(1.55f, .164f, -3.25f);
    static readonly Vector3 CustomerQueuePosition = new Vector3(1.4f, .02f, -5.63f);
    static readonly Vector3 CustomerEntryPosition = new Vector3(7.4f, 0f, -6.25f);
    static readonly Vector3 CustomerExitPosition = new Vector3(-7.4f, 0f, -6.25f);
    static readonly Vector3 ManualInputTrayPosition = new Vector3(-.944f, .928f, 2.4f);
    static readonly Vector3 ManualOutputTrayPosition = new Vector3(.944f, .872f, 2.4f);
    static readonly Vector3 ManualInputMarkerPosition = new Vector3(-1.75f, .02f, 1.45f);
    static readonly Vector3 ManualOutputMarkerPosition = new Vector3(1.75f, .02f, 1.45f);
    static readonly Vector3 ManualWorkMarkerPosition = new Vector3(0f, .02f, .85f);
    static readonly Vector3 ManualInputPointPosition = new Vector3(-1.75f, .1f, 1.45f);
    static readonly Vector3 ManualOutputPointPosition = new Vector3(1.75f, .1f, 1.45f);
    static readonly Vector3 ManualWorkPointPosition = new Vector3(0f, .1f, .85f);
    const float CustomerSidewalkLength = 16f;
    const float ManualPointFootprint = .85f;
    const float SalesPointFootprint = 1.5f;
    static readonly Vector3 SalesPalletBlockerSize = new Vector3(1.1f, .9f, 1.1f);
    static readonly Vector3 SalesPalletBlockerCenter = new Vector3(0f, .286f, 0f);

    [MenuItem("Tools/Churub/Compact Factory/Build Stage 0 Playtest")]
    public static void Build()
    {
        if (!File.Exists(LayoutPath)) throw new InvalidOperationException("Layout scene missing.");
        if (File.Exists(ScenePath)) throw new InvalidOperationException("Playtest exists. Preserve edits before rebuilding.");
        string gameHash = Hash(GamePath), layoutHash = Hash(LayoutPath);
        var game = EditorSceneManager.OpenScene(GamePath, OpenSceneMode.Single);
        if (!EditorSceneManager.SaveScene(game, ScenePath, true))
            throw new InvalidOperationException("Cannot copy Game scene.");
        var scene = EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single);
        var sourceRoots = scene.GetRootGameObjects();
        var layout = EditorSceneManager.OpenScene(LayoutPath, OpenSceneMode.Additive);
        foreach (var root in layout.GetRootGameObjects())
        {
            SceneManager.MoveGameObjectToScene(root, scene);
            if (root.name == "Layout Camera") root.SetActive(false);
        }
        EditorSceneManager.CloseScene(layout, true);
        SceneManager.SetActiveScene(scene);
        foreach (var root in sourceRoots)
        {
            if (root.name == "Factory" || root.name == "_Store")
            {
                if (root.name == "Factory") root.SetActive(false);
                else
                {
                    root.transform.position += new Vector3(100, 0, 100);
                    foreach (var renderer in root.GetComponentsInChildren<Renderer>(true))
                        renderer.enabled = false;
                }
            }
            else if (root.name == "[ Player ]")
                root.transform.position = new Vector3(-1.5f, 0, 0);
            else if (!new[] {"Work Actions", "Main Camera", "Canvas", "EventSystem", "Guide",
                     "AudioManager", "InterstitialAdExample", "RewardedAdsButton"}.Contains(root.name))
                root.SetActive(false);
        }
        HideScaleReference(scene);
        Place(At(Root(scene, "_Store").transform, "Common GameObjects"),
            typeof(StoreInteraction), new Vector3(1.4f, .1f, -4.45f));
        InstallStageZero(scene);
        EditorSceneManager.SaveScene(scene, ScenePath);
        AddToBuildSettings();
        EditorPrefs.SetBool(EditorPrefKey, true);
        Validate();
        if (Hash(GamePath) != gameHash || Hash(LayoutPath) != layoutHash)
            throw new InvalidOperationException("Source scene changed during build.");
        Debug.Log("COMPACT_FACTORY_PLAYTEST_READY");
    }

    [MenuItem("Tools/Churub/Compact Factory/Use Stage 0 Playtest In Editor")]
    public static void UsePlaytest()
    {
        if (!File.Exists(ScenePath)) throw new InvalidOperationException("Build playtest first.");
        AddToBuildSettings();
        EditorPrefs.SetBool(EditorPrefKey, true);
    }

    [MenuItem("Tools/Churub/Compact Factory/Use Original Game In Editor")]
    public static void UseOriginal() => EditorPrefs.SetBool(EditorPrefKey, false);

    [MenuItem("Tools/Churub/Compact Factory/Upgrade Existing Playtest Visuals")]
    public static void UpgradeExistingPlaytest()
    {
        var scene = EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single);
        if (scene.GetRootGameObjects().Any(o => o.name == "Stage 0 Gameplay"))
            throw new InvalidOperationException("Stage 0 Gameplay already exists. Preserve hand edits.");
        Root(scene, "Factory").SetActive(false);
        InstallStageZero(scene);
        EditorSceneManager.SaveScene(scene, ScenePath);
        Validate();
        Debug.Log("COMPACT_FACTORY_VISUAL_GAMEPLAY_READY");
    }

    [MenuItem("Tools/Churub/Compact Factory/Install Incremental UX References")]
    public static void InstallIncrementalUxReferences()
    {
        var scene = EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single);
        var workPoints = scene.GetRootGameObjects().SelectMany(root =>
            root.GetComponentsInChildren<WorkPoint>(true)).ToArray();
        foreach (var point in workPoints)
        {
            OnboardingTargetType? type = TargetType(point);
            if (!type.HasValue) continue;
            var marker = point.GetComponent<OnboardingTargetMarker>();
            if (marker == null) marker = point.gameObject.AddComponent<OnboardingTargetMarker>();
            marker.Configure(type.Value);
            EditorUtility.SetDirty(marker);
        }

        var ui = scene.GetRootGameObjects().SelectMany(root =>
            root.GetComponentsInChildren<UIManager>(true)).SingleOrDefault();
        Require(ui != null, "UIManager missing from compact playtest");
        if (ui.GetComponent<UpgradeGraphView>() == null)
            ui.gameObject.AddComponent<UpgradeGraphView>();
        ConfigureEmployeeNavigationSurface(scene);

        EditorSceneManager.MarkSceneDirty(scene);
        EditorSceneManager.SaveScene(scene, ScenePath);
        ValidateIncrementalUx(scene);
        Debug.Log("COMPACT_FACTORY_INCREMENTAL_UX_REFERENCES_READY");
    }

    private static OnboardingTargetType? TargetType(WorkPoint point)
    {
        if (point.Action is CompactManualProcessAction)
            return OnboardingTargetType.ManualWork;
        if (!(point.Action is ItemTransfer transfer)) return null;
        if (transfer.Endpoint is CompactSupplyStation) return OnboardingTargetType.SupplyPickup;
        if (transfer.Endpoint is CompactManualStation) return OnboardingTargetType.ManualInput;
        if (transfer.Endpoint is CompactManualOutput) return OnboardingTargetType.ManualOutput;
        if (transfer.Endpoint is CompactSalesCounter) return OnboardingTargetType.SalesPallet;
        return null;
    }

    private static void ValidateIncrementalUx(Scene scene)
    {
        var markers = scene.GetRootGameObjects().SelectMany(root =>
            root.GetComponentsInChildren<OnboardingTargetMarker>(true)).ToArray();
        foreach (OnboardingTargetType type in Enum.GetValues(typeof(OnboardingTargetType)))
            Require(markers.Count(marker => marker.TargetType == type) == 1,
                "Onboarding target missing or duplicated: " + type);
        var graphViews = scene.GetRootGameObjects().SelectMany(root =>
            root.GetComponentsInChildren<UpgradeGraphView>(true)).ToArray();
        Require(graphViews.Length == 1, "Upgrade graph view must exist exactly once");
    }

    public static void RenderVisualProof()
    {
        var scene = EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single);
        var gameplay = Root(scene, "Stage 0 Gameplay").transform;
        var supply = At(gameplay, "Container Supply/Pickup Tray");
        var input = At(gameplay, "Manual Processing/Ingredient Board");
        var output = At(gameplay, "Manual Processing/Finished Product Placement");
        var sale = At(gameplay, "Front Sales Display/Sales Stock Pile");
        var salmon = AssetDatabase.LoadAssetAtPath<GameObject>(
            "Assets/3. Prefab/Churu/Salmon/Salmon.prefab");
        var product = AssetDatabase.LoadAssetAtPath<GameObject>(
            "Assets/3. Prefab/Churu/Churub_Package_Defalt.prefab");
        Require(salmon != null && product != null, "Display prefabs missing");
        UnityEngine.Object.Instantiate(salmon, supply).transform.localPosition = Vector3.zero;
        UnityEngine.Object.Instantiate(salmon, input).transform.localPosition = Vector3.zero;
        UnityEngine.Object.Instantiate(product, output).transform.localPosition =
            new Vector3(-.23f, .04f, -.16f);
        for (int i = 0; i < 12; i++)
        {
            var preview = UnityEngine.Object.Instantiate(product, sale).transform;
            preview.localPosition = new Vector3((i % 3 - 1) * .23f,
                .04f + i / 6 * .08f, i % 6 / 3 == 0 ? -.16f : .16f);
        }

        var cameraObject = Root(scene, "Layout Camera");
        cameraObject.SetActive(true);
        var camera = cameraObject.GetComponent<Camera>();
        camera.orthographicSize = 7f;
        camera.transform.position = new Vector3(-9, 11, -12);
        camera.transform.LookAt(new Vector3(0, 0, -.7f));
        var texture = new RenderTexture(1600, 1200, 24);
        var previous = RenderTexture.active;
        camera.targetTexture = texture;
        camera.Render();
        RenderTexture.active = texture;
        var pixels = new Texture2D(1600, 1200, TextureFormat.RGB24, false);
        pixels.ReadPixels(new Rect(0, 0, 1600, 1200), 0, 0);
        pixels.Apply();
        Directory.CreateDirectory("Logs/CompactFactory");
        File.WriteAllBytes("Logs/CompactFactory/playtest-model-proof.png", pixels.EncodeToPNG());
        camera.targetTexture = null;
        RenderTexture.active = previous;
        UnityEngine.Object.DestroyImmediate(pixels);
        texture.Release();
        UnityEngine.Object.DestroyImmediate(texture);
        Debug.Log("COMPACT_FACTORY_MODEL_PROOF_READY");
    }

    [MenuItem("Tools/Churub/Compact Factory/Hide Scale Reference In Playtest")]
    public static void HideScaleReferenceInPlaytest()
    {
        var scene = EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single);
        HideScaleReference(scene);
        EditorSceneManager.SaveScene(scene, ScenePath);
        Validate();
    }

    [MenuItem("Tools/Churub/Compact Factory/Validate Stage 0 Playtest")]
    public static void Validate()
    {
        var scene = EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single);
        ValidateScene(scene);
    }

    internal static void ValidateScene(Scene scene)
    {
        Require(Root(scene, "[ Player ]").GetComponent<Player>() != null, "Player missing");
        var playerAnimator = Root(scene, "[ Player ]").GetComponent<Animator>();
        Require(playerAnimator != null && playerAnimator.layerCount > 1 &&
            playerAnimator.GetLayerName(1) == "BoxPackaging Layer",
            "Player packaging animation layer missing");
        Require(Root(scene, "Canvas").GetComponentInChildren<UIManager>(true) != null, "UI missing");
        ValidateGuide(scene);
        ValidateIncrementalUx(scene);
        Require(Root(scene, "_Store").GetComponent<Store>() != null, "Store missing");
        Require(!Root(scene, "Factory").activeSelf, "Offscreen factory must be disabled");
        Require(!Root(scene, "_Store").GetComponent<Store>().enabled, "Passive income must be disabled");
        var gameplay = Root(scene, "Stage 0 Gameplay");
        var supply = gameplay.GetComponentInChildren<CompactSupplyStation>(true);
        var manual = gameplay.GetComponentInChildren<CompactManualStation>(true);
        var sales = gameplay.GetComponentInChildren<CompactSalesCounter>(true);
        Require(supply != null && manual != null && sales != null, "Stage 0 stations missing");
        var customerSpawner = Root(scene, "_Store").GetComponentInChildren<SpawnPoint>(true);
        Require(customerSpawner != null && customerSpawner.gameObject.activeInHierarchy,
            "Customer spawner missing or inactive");
        var routeMarkers = scene.GetRootGameObjects().SelectMany(root =>
            root.GetComponentsInChildren<Transform>(true)).ToArray();
        Require(routeMarkers.Any(t => t.name == "Future Customer Entry" && t.gameObject.activeInHierarchy),
            "Customer entry missing");
        Require(routeMarkers.Any(t => t.name == "Customer Queue" && t.gameObject.activeInHierarchy),
            "Customer queue missing");
        Require(routeMarkers.Any(t => t.name == "Future Customer Exit" && t.gameObject.activeInHierarchy),
            "Customer exit missing");
        var sidewalk = routeMarkers.FirstOrDefault(t => t.name == "Customer Sidewalk" &&
            t.gameObject.activeInHierarchy);
        Require(sidewalk != null && sidewalk.GetComponent<MeshFilter>() != null &&
            sidewalk.GetComponent<NavMeshSurface>() != null,
            "Customer sidewalk NavMeshSurface missing");
        var surface = sidewalk.GetComponent<NavMeshSurface>();
        Require(surface.collectObjects == CollectObjects.Children &&
            surface.layerMask.value == 1 << sidewalk.gameObject.layer &&
            surface.useGeometry == NavMeshCollectGeometry.PhysicsColliders &&
            surface.agentTypeID == 0, "Customer NavMeshSurface settings are invalid");
        Require(Mathf.Abs(sidewalk.localScale.x - CustomerSidewalkLength) < .001f,
            "Customer sidewalk must extend beyond the play area");
        Require((routeMarkers.First(t => t.name == "Future Customer Entry").position -
            CustomerEntryPosition).sqrMagnitude < .001f, "Customer entry is too close");
        Require((routeMarkers.First(t => t.name == "Future Customer Exit").position -
            CustomerExitPosition).sqrMagnitude < .001f, "Customer exit is too close");
        Require(ReferencePrefab(supply, "ingredientPrefab", ItemType.Ingredient),
            "Visible ingredient prefab missing");
        Require(supply.StockCapacity >= 50, "Supply stock capacity must be at least 50");
        Require(ReferencePrefab(manual, "productPrefab", ItemType.Churu),
            "Visible product prefab missing");
        var workIcon = new SerializedObject(manual).FindProperty("workIcon")
            .objectReferenceValue as Sprite;
        Require(workIcon != null && AssetDatabase.GetAssetPath(workIcon) == WorkIconPath,
            "Manual processing product icon missing");
        var salesData = new SerializedObject(sales);
        var stockAnchor = salesData.FindProperty("stockAnchor").objectReferenceValue as Transform;
        Require(stockAnchor != null &&
            (stockAnchor.position - SalesStockPilePosition).sqrMagnitude < .001f,
            "Sales stock pile misplaced");
        var palletBlocker = stockAnchor.GetComponent<BoxCollider>();
        Require(palletBlocker != null && palletBlocker.enabled && !palletBlocker.isTrigger &&
            (palletBlocker.size - SalesPalletBlockerSize).sqrMagnitude < .001f &&
            (palletBlocker.center - SalesPalletBlockerCenter).sqrMagnitude < .001f,
            "Sales stock pallet blocker is missing");
        Require(sales.StockCapacity >= 100, "Sales stock capacity must be at least 100");
        Require((At(gameplay.transform, "Container Supply/Pickup Tray").position -
            new Vector3(-3.5f, .11f, 3.29f)).sqrMagnitude < .001f,
            "Supply pickup tray misplaced");
        Require((At(gameplay.transform, "Container Supply/Container Feed").position -
            SupplySpawnPosition).sqrMagnitude < .001f, "Supply feed spawn misplaced");
        Require((At(gameplay.transform, "Manual Processing/Ingredient Board").position -
            ManualInputTrayPosition).sqrMagnitude < .001f,
            "Processing input tray misplaced");
        Require((At(gameplay.transform, "Manual Processing/Finished Product Placement").position -
            ManualOutputTrayPosition).sqrMagnitude < .001f,
            "Processing output placement misplaced");
        var starter = Root(scene, "01 Starter Workshop - 10 x 10 m").transform;
        var employeeSurface = starter.GetComponent<NavMeshSurface>();
        Require(employeeSurface != null &&
            employeeSurface.collectObjects == CollectObjects.Children &&
            employeeSurface.layerMask.value == ~0 &&
            employeeSurface.useGeometry == NavMeshCollectGeometry.PhysicsColliders &&
            employeeSurface.agentTypeID == 0,
            "Employee NavMeshSurface settings are invalid");
        var salesCounterVisual = At(starter, "Starter Sales Counter");
        Require((salesCounterVisual.position - StoreCounterPosition).sqrMagnitude < .001f,
            "Sales counter misplaced");
        var salesCounterCollider = salesCounterVisual.GetComponent<MeshCollider>();
        Require(salesCounterVisual.gameObject.layer == 0 && salesCounterCollider != null &&
            salesCounterCollider.enabled && !salesCounterCollider.isTrigger &&
            salesCounterCollider.sharedMesh == salesCounterVisual.GetComponent<MeshFilter>()?.sharedMesh,
            "Sales counter collision is missing");
        Require(!At(starter, "Stocking Interaction").gameObject.activeSelf,
            "Legacy sales stocking marker must be hidden");
        var salesPallet = At(starter, "Sales Stock Pallet");
        Require((salesPallet.position - SalesPalletPosition).sqrMagnitude < .001f &&
            (salesPallet.localScale - SalesPalletScale).sqrMagnitude < .001f,
            "Sales stock pallet misplaced");
        var palletPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(SalesPalletPrefabPath);
        Require(palletPrefab != null &&
            PrefabUtility.GetCorrespondingObjectFromOriginalSource(salesPallet.gameObject) ==
                palletPrefab,
            "Sales stock pallet prefab is disconnected");
        Require((At(starter, "Customer Queue").position - CustomerQueuePosition)
            .sqrMagnitude < .001f, "Customer queue misplaced");
        var feederAdapter = At(starter,
            "Salmon Container Supply/Short Feeder Length Adapter");
        Require((feederAdapter.localPosition - SupplyBeltAdapterPosition).sqrMagnitude < .001f &&
            (feederAdapter.localScale - SupplyBeltAdapterScale).sqrMagnitude < .001f,
            "Container feed belt length adapter misplaced");
        var feederBounds = At(feederAdapter, "Short Container Feeder")
            .GetComponent<Renderer>().bounds;
        Require(Mathf.Abs(feederBounds.size.z - 3.2f) < .02f &&
            feederBounds.min.z < SupplySpawnPosition.z &&
            feederBounds.max.z > SupplySpawnPosition.z &&
            Mathf.Abs(feederBounds.max.y + .01f - SupplySpawnPosition.y) < .02f,
            "Supply spawn must sit on the extended conveyor");
        var benchVisual = At(starter, "Manual Processing Station");
        Require((benchVisual.localScale - Vector3.one * .8f).sqrMagnitude < .001f,
            "Manual processing station scale must be 0.8");
        var inputTrayParts = benchVisual.Cast<Transform>().Where(t =>
            t.name.StartsWith("InputTray_", StringComparison.Ordinal)).ToArray();
        Require(inputTrayParts.Length == 5 && inputTrayParts.All(t => t.localPosition.x < 0f),
            "Salmon tray visuals must be placed on the input side");
        var outputTrayParts = benchVisual.Cast<Transform>().Where(t =>
            t.name.StartsWith("OutputTray_", StringComparison.Ordinal)).ToArray();
        Require(outputTrayParts.Length == 5 && outputTrayParts.All(t => !t.gameObject.activeSelf),
            "Finished product tray visuals must be hidden");
        Require((At(starter, "Processing Interaction").position -
            ManualInputMarkerPosition).sqrMagnitude < .001f,
            "Processing input marker misplaced");
        Require((At(starter, "Finished Product Interaction").position -
            ManualOutputMarkerPosition).sqrMagnitude < .001f,
            "Processing output marker misplaced");
        Require((At(starter, "Processing Work Interaction").position -
            ManualWorkMarkerPosition).sqrMagnitude < .001f,
            "Processing work marker misplaced");
        Require(!Root(scene, "Layout Camera").activeSelf, "Second camera active");
        Require(!At(Root(scene, "01 Starter Workshop - 10 x 10 m").transform,
            "Player Scale Reference").gameObject.activeSelf, "Scale reference must be hidden");
        Require(!At(Root(scene, "Growth Layout").transform,
            "Stage 1 - First Automation").gameObject.activeSelf, "Stage 1 is active");
        Check(scene, "Stage 0 Gameplay", typeof(CompactSupplyStation),
            new Vector3(-3.5f, .1f, 2));
        Check(scene, "Stage 0 Gameplay", typeof(CompactManualStation),
            ManualInputPointPosition, ManualPointFootprint);
        Check(scene, "Stage 0 Gameplay", typeof(CompactManualOutput),
            ManualOutputPointPosition, ManualPointFootprint);
        var processPoint = gameplay.GetComponentsInChildren<WorkPoint>(true).FirstOrDefault(p =>
            p.gameObject.activeInHierarchy &&
            (p.transform.position - ManualWorkPointPosition).sqrMagnitude < .01f);
        Require(processPoint != null && processPoint.Action is CompactManualProcessAction process &&
            process.Station == manual &&
            processPoint.GetComponents<BoxCollider>().Any(c => c.enabled && c.isTrigger &&
                Mathf.Abs(c.size.x - ManualPointFootprint) < .001f &&
                Mathf.Abs(c.size.z - ManualPointFootprint) < .001f),
            "Manual processing WorkPoint missing or disconnected");
        Check(scene, "Stage 0 Gameplay", typeof(CompactSalesCounter),
            SalesWorkPointPosition, SalesPointFootprint);
        Check(scene, "_Store/Common GameObjects", typeof(StoreInteraction),
            new Vector3(1.4f, .1f, -4.45f));
        foreach (var root in scene.GetRootGameObjects())
            foreach (var t in root.GetComponentsInChildren<Transform>(true))
                Require(GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(t.gameObject) == 0,
                    "Missing script: " + t.name);
        Debug.Log("COMPACT_FACTORY_PLAYTEST_VALIDATION_PASS");
    }

    static void ValidateGuide(Scene scene)
    {
        var guideObject = Root(scene, "Guide");
        Require(guideObject.activeSelf, "Guide root must be active");
        var guide = guideObject.GetComponent<Guide>();
        Require(guide != null && guide.enabled, "Guide component missing or disabled");

        var serializedGuide = new SerializedObject(guide);
        foreach (var field in new[] { "guidePrefab", "guideButton", "guideUI", "guideLine" })
            Require(serializedGuide.FindProperty(field)?.objectReferenceValue != null,
                "Guide reference missing: " + field);

        var targets = serializedGuide.FindProperty("targets");
        Require(targets != null && targets.isArray && targets.arraySize >= 15,
            "Guide expansion target list is incomplete");
        var unlocks = guideObject.GetComponentsInChildren<UnlockManager>(true);
        foreach (UnlockType type in Enum.GetValues(typeof(UnlockType)))
            Require(unlocks.Count(unlock => unlock.Type == type) == 1,
                "Guide unlock target missing or duplicated: " + type);
        var ui = Root(scene, "Canvas").GetComponentInChildren<UIManager>(true);
        var serializedUi = new SerializedObject(ui);
        Require(serializedUi.FindProperty("upgradePanel")?.objectReferenceValue != null,
            "Upgrade panel is disconnected");
        Require(serializedUi.FindProperty("upgradeCostText")?.arraySize >= 6,
            "Upgrade buttons are disconnected");

        var gameplay = Root(scene, "Stage 0 Gameplay");
        var supply = gameplay.GetComponentInChildren<CompactSupplyStation>(true);
        var manual = gameplay.GetComponentInChildren<CompactManualStation>(true);
        var sales = gameplay.GetComponentInChildren<CompactSalesCounter>(true);
        var workPoints = scene.GetRootGameObjects().SelectMany(root =>
            root.GetComponentsInChildren<WorkPoint>(true)).ToArray();

        Require(workPoints.Any(point => point.Action is ItemTransfer transfer &&
            transfer.Endpoint == supply), "Guide step 0 is disconnected from compact supply");
        Require(workPoints.Any(point => point.Action is ItemTransfer transfer &&
            transfer.Endpoint == manual), "Guide step 1 is disconnected from manual input");
        Require(workPoints.Any(point => point.Action is CompactManualProcessAction process &&
            process.Station == manual), "Guide step 2 is disconnected from manual processing");
        Require(workPoints.Any(point => point.Action is ItemTransfer transfer &&
            transfer.Endpoint is CompactManualOutput output && output.Station == manual),
            "Guide step 3 is disconnected from manual output");
        Require(workPoints.Any(point => point.Action is ItemTransfer transfer &&
            transfer.Endpoint == sales), "Guide steps 4 and 5 are disconnected from sales stock");
    }

    static void Check(Scene scene, string path, Type endpointType, Vector3 expected,
        float? footprint = null)
    {
        var slash = path.IndexOf('/');
        var parent = slash < 0 ? Root(scene, path).transform
            : At(Root(scene, path.Substring(0, slash)).transform, path.Substring(slash + 1));
        var point = parent.GetComponentsInChildren<WorkPoint>(true)
            .FirstOrDefault(p => p.gameObject.activeInHierarchy &&
                (p.transform.position - expected).sqrMagnitude < .01f);
        Require(point != null, "WorkPoint missing: " + path);
        Require(point.GetComponents<Collider>().Any(c => c.enabled && c.isTrigger),
            "Trigger collider missing: " + path);
        if (footprint.HasValue)
            Require(point.GetComponents<BoxCollider>().Any(c => c.enabled && c.isTrigger &&
                Mathf.Abs(c.size.x - footprint.Value) < .001f &&
                Mathf.Abs(c.size.z - footprint.Value) < .001f),
                "Trigger footprint mismatch: " + path);
        Require(endpointType == typeof(StoreInteraction)
            ? point.Action is StoreInteraction
            : point.Action is ItemTransfer transfer && transfer.Endpoint != null &&
              endpointType.IsInstanceOfType(transfer.Endpoint), "Action endpoint missing: " + path);
    }

    static void Place(Transform parent, Type endpointType, Vector3 position)
    {
        var point = parent.GetComponentsInChildren<WorkPoint>(true).FirstOrDefault(p =>
            (p.Action is ItemTransfer transfer && transfer.Endpoint != null &&
             endpointType.IsInstanceOfType(transfer.Endpoint)) ||
            (endpointType == typeof(StoreInteraction) && p.Action is StoreInteraction));
        if (point == null) throw new InvalidOperationException("WorkPoint missing: " + parent.name);
        point.transform.position = position;
        point.gameObject.SetActive(true);
        foreach (var collider in point.GetComponents<Collider>()) collider.enabled = true;
    }

    static void InstallStageZero(Scene scene)
    {
        var ui = Root(scene, "Canvas").GetComponentInChildren<UIManager>(true);
        Require(ui != null, "UIManager missing");
        if (ui.GetComponent<UpgradeGraphView>() == null)
            ui.gameObject.AddComponent<UpgradeGraphView>();
        var starter = Root(scene, "01 Starter Workshop - 10 x 10 m").transform;
        ConfigureEmployeeNavigationSurface(scene);
        var salesCounterVisual = At(starter, "Starter Sales Counter");
        salesCounterVisual.position = StoreCounterPosition;
        salesCounterVisual.gameObject.layer = 0;
        var salesCounterCollider = salesCounterVisual.GetComponent<MeshCollider>();
        if (salesCounterCollider == null)
            salesCounterCollider = salesCounterVisual.gameObject.AddComponent<MeshCollider>();
        salesCounterCollider.sharedMesh = salesCounterVisual.GetComponent<MeshFilter>().sharedMesh;
        salesCounterCollider.convex = false;
        salesCounterCollider.isTrigger = false;
        salesCounterCollider.enabled = true;
        At(starter, "Stocking Interaction").gameObject.SetActive(false);
        var palletPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(SalesPalletPrefabPath);
        Require(palletPrefab != null, "Sales stock pallet prefab missing");
        var salesPallet = PrefabUtility.InstantiatePrefab(palletPrefab, starter) as GameObject;
        Require(salesPallet != null, "Cannot instantiate sales stock pallet");
        salesPallet.name = "Sales Stock Pallet";
        salesPallet.transform.position = SalesPalletPosition;
        salesPallet.transform.localScale = SalesPalletScale;
        At(starter, "Customer Queue").position = CustomerQueuePosition;
        var salesFrontage = At(starter, "Exterior Sales Frontage");
        At(salesFrontage, "Future Customer Entry").position = CustomerEntryPosition;
        At(salesFrontage, "Future Customer Exit").position = CustomerExitPosition;
        At(salesFrontage, "Customer Sidewalk").localScale =
            new Vector3(CustomerSidewalkLength, .24f, 2.5f);
        var inputMarker = At(starter, "Processing Interaction");
        inputMarker.position = ManualInputMarkerPosition;
        var outputMarker = UnityEngine.Object.Instantiate(inputMarker.gameObject, starter);
        outputMarker.name = "Finished Product Interaction";
        outputMarker.transform.position = ManualOutputMarkerPosition;
        var processMarker = UnityEngine.Object.Instantiate(inputMarker.gameObject, starter);
        processMarker.name = "Processing Work Interaction";
        processMarker.transform.position = ManualWorkMarkerPosition;
        var benchVisual = At(starter, "Manual Processing Station");
        benchVisual.localScale = Vector3.one * .8f;
        foreach (Transform part in benchVisual)
        {
            if (part.name.StartsWith("InputTray_", StringComparison.Ordinal))
            {
                var localPosition = part.localPosition;
                localPosition.x = -Mathf.Abs(localPosition.x);
                part.localPosition = localPosition;
            }
            else if (part.name.StartsWith("OutputTray_", StringComparison.Ordinal))
                part.gameObject.SetActive(false);
        }

        var feederAdapter = At(starter,
            "Salmon Container Supply/Short Feeder Length Adapter");
        feederAdapter.localPosition = SupplyBeltAdapterPosition;
        feederAdapter.localScale = SupplyBeltAdapterScale;

        var sidewalk = scene.GetRootGameObjects().SelectMany(root =>
            root.GetComponentsInChildren<Transform>(true))
            .FirstOrDefault(t => t.name == "Customer Sidewalk");
        Require(sidewalk != null && sidewalk.GetComponent<MeshFilter>() != null,
            "Customer sidewalk mesh missing");
        var customerSurface = sidewalk.GetComponent<NavMeshSurface>();
        if (customerSurface == null)
            customerSurface = sidewalk.gameObject.AddComponent<NavMeshSurface>();
        customerSurface.collectObjects = CollectObjects.Children;
        customerSurface.layerMask = 1 << sidewalk.gameObject.layer;
        customerSurface.useGeometry = NavMeshCollectGeometry.PhysicsColliders;
        customerSurface.agentTypeID = 0;

        var factory = Root(scene, "Factory");
        var ingredientSource = At(factory.transform, "Container/IngredientSpawn")
            .GetComponent<IngredientMaker>();
        var productSource = At(factory.transform,
            "Machine/ChuruConveyerBelt Obj 1/ChuruStorage").GetComponent<BoxStorage>();
        var ingredientPrefab = new SerializedObject(ingredientSource)
            .FindProperty("objPrefab").objectReferenceValue as GameObject;
        var productPrefab = new SerializedObject(productSource)
            .FindProperty("churu").objectReferenceValue as GameObject;
        var workIcon = AssetDatabase.LoadAssetAtPath<Sprite>(WorkIconPath);
        Require(ingredientPrefab != null && ingredientPrefab.GetComponent<Item>()?.Type == ItemType.Ingredient,
            "Ingredient prefab missing or invalid");
        Require(productPrefab != null && productPrefab.GetComponent<Item>()?.Type == ItemType.Churu,
            "Product prefab missing or invalid");
        Require(workIcon != null, "Manual processing product icon missing");

        var store = Root(scene, "_Store").GetComponent<Store>();
        store.enabled = false; // Stage zero sales now depend on visible stocked products.
        var group = new GameObject("Stage 0 Gameplay");
        if (group.scene != scene) SceneManager.MoveGameObjectToScene(group, scene);

        var supplyObject = Child(group.transform, "Container Supply");
        var supply = supplyObject.gameObject.AddComponent<CompactSupplyStation>();
        Assign(supply, "ingredientPrefab", ingredientPrefab);
        Assign(supply, "spawnPoint", Child(supplyObject, "Container Feed", SupplySpawnPosition));
        Assign(supply, "pickupTray", Child(supplyObject, "Pickup Tray", new Vector3(-3.5f, .11f, 3.29f)));

        var benchObject = Child(group.transform, "Manual Processing");
        var manual = benchObject.gameObject.AddComponent<CompactManualStation>();
        Assign(manual, "productPrefab", productPrefab);
        Assign(manual, "workIcon", workIcon);
        Assign(manual, "inputTray", Child(benchObject, "Ingredient Board", ManualInputTrayPosition));
        Assign(manual, "outputTray", Child(benchObject, "Finished Product Placement", ManualOutputTrayPosition));
        var output = benchObject.gameObject.AddComponent<CompactManualOutput>();
        Assign(output, "station", manual);

        var counterObject = Child(group.transform, "Front Sales Display");
        var counter = counterObject.gameObject.AddComponent<CompactSalesCounter>();
        var stockAnchor = Child(counterObject, "Sales Stock Pile", SalesStockPilePosition);
        var palletBlocker = stockAnchor.gameObject.AddComponent<BoxCollider>();
        palletBlocker.isTrigger = false;
        palletBlocker.center = SalesPalletBlockerCenter;
        palletBlocker.size = SalesPalletBlockerSize;
        var serialized = new SerializedObject(counter);
        serialized.FindProperty("stockAnchor").objectReferenceValue = stockAnchor;
        serialized.FindProperty("capacity").intValue = 100;
        serialized.ApplyModifiedPropertiesWithoutUndo();

        Trigger(group.transform, "Take Salmon", new Vector3(-3.5f, .1f, 2), supply);
        Trigger(group.transform, "Place Salmon", ManualInputPointPosition, manual,
            ManualPointFootprint);
        ProcessTrigger(group.transform, ManualWorkPointPosition, manual,
            ManualPointFootprint);
        Trigger(group.transform, "Take Churu", ManualOutputPointPosition, output,
            ManualPointFootprint);
        Trigger(group.transform, "Stock Sales Display", SalesWorkPointPosition, counter,
            SalesPointFootprint);
    }

    private static void ConfigureEmployeeNavigationSurface(Scene scene)
    {
        var starter = Root(scene, "01 Starter Workshop - 10 x 10 m");
        var surface = starter.GetComponent<NavMeshSurface>();
        if (surface == null)
            surface = starter.AddComponent<NavMeshSurface>();
        surface.collectObjects = CollectObjects.Children;
        surface.layerMask = ~0;
        surface.useGeometry = NavMeshCollectGeometry.PhysicsColliders;
        surface.agentTypeID = 0;
        EditorUtility.SetDirty(surface);
    }

    static Transform Child(Transform parent, string name, Vector3? worldPosition = null)
    {
        var child = new GameObject(name).transform;
        child.SetParent(parent, false);
        if (worldPosition.HasValue) child.position = worldPosition.Value;
        return child;
    }

    static void Trigger(Transform parent, string name, Vector3 position, MonoBehaviour endpoint,
        float footprint = 1.1f)
    {
        var target = Child(parent, name, position).gameObject;
        var collider = target.AddComponent<BoxCollider>();
        collider.isTrigger = true;
        collider.center = new Vector3(0, .9f, 0);
        collider.size = new Vector3(footprint, 1.8f, footprint);
        var action = target.AddComponent<ItemTransfer>();
        Assign(action, "endpoint", endpoint);
        var point = target.AddComponent<WorkPoint>();
        Assign(point, "action", action);
        OnboardingTargetType? targetType = endpoint is CompactSupplyStation
            ? OnboardingTargetType.SupplyPickup
            : endpoint is CompactManualStation
                ? OnboardingTargetType.ManualInput
                : endpoint is CompactManualOutput
                    ? OnboardingTargetType.ManualOutput
                    : endpoint is CompactSalesCounter
                        ? OnboardingTargetType.SalesPallet
                        : (OnboardingTargetType?)null;
        if (targetType.HasValue)
            target.AddComponent<OnboardingTargetMarker>().Configure(targetType.Value);
    }

    static void ProcessTrigger(Transform parent, Vector3 position, CompactManualStation station,
        float footprint = 1.1f)
    {
        var target = Child(parent, "Process Salmon", position).gameObject;
        var collider = target.AddComponent<BoxCollider>();
        collider.isTrigger = true;
        collider.center = new Vector3(0, .9f, 0);
        collider.size = new Vector3(footprint, 1.8f, footprint);
        var action = target.AddComponent<CompactManualProcessAction>();
        Assign(action, "station", station);
        var point = target.AddComponent<WorkPoint>();
        Assign(point, "action", action);
        target.AddComponent<OnboardingTargetMarker>().Configure(OnboardingTargetType.ManualWork);
    }

    static void Assign(UnityEngine.Object target, string field, UnityEngine.Object value)
    {
        var serialized = new SerializedObject(target);
        var property = serialized.FindProperty(field);
        if (property == null) throw new InvalidOperationException("Field missing: " + field);
        property.objectReferenceValue = value;
        serialized.ApplyModifiedPropertiesWithoutUndo();
    }

    static void HideScaleReference(Scene scene) =>
        At(Root(scene, "01 Starter Workshop - 10 x 10 m").transform,
            "Player Scale Reference").gameObject.SetActive(false);

    static Transform At(Transform root, string path) =>
        root.Find(path) ?? throw new InvalidOperationException("Missing: " + root.name + "/" + path);
    static GameObject Root(Scene scene, string name) =>
        scene.GetRootGameObjects().FirstOrDefault(o => o.name == name)
        ?? throw new InvalidOperationException("Missing root: " + name);
    static void Require(bool ok, string message)
    {
        if (!ok) throw new InvalidOperationException(message);
    }
    static bool ReferencePrefab(UnityEngine.Object component, string field, ItemType type)
    {
        var prefab = new SerializedObject(component).FindProperty(field)?.objectReferenceValue as GameObject;
        return prefab != null && prefab.GetComponent<Item>()?.Type == type
            && prefab.GetComponentInChildren<Renderer>(true) != null;
    }
    static void AddToBuildSettings()
    {
        var scenes = EditorBuildSettings.scenes.ToList();
        if (scenes.All(s => s.path != ScenePath))
            scenes.Add(new EditorBuildSettingsScene(ScenePath, true));
        EditorBuildSettings.scenes = scenes.ToArray();
    }
    static string Hash(string path)
    {
        using (var sha = SHA256.Create())
            return Convert.ToBase64String(sha.ComputeHash(File.ReadAllBytes(path)));
    }
}
