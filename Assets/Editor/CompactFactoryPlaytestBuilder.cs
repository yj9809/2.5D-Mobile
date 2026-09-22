using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
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
            else if (!new[] {"Work Actions", "Main Camera", "Canvas", "EventSystem",
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

    public static void RenderVisualProof()
    {
        var scene = EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single);
        var gameplay = Root(scene, "Stage 0 Gameplay").transform;
        var supply = At(gameplay, "Container Supply/Pickup Tray");
        var input = At(gameplay, "Manual Processing/Ingredient Board");
        var output = At(gameplay, "Manual Processing/Finished Product Tray");
        var sale = At(gameplay, "Front Sales Display");
        var salmon = AssetDatabase.LoadAssetAtPath<GameObject>(
            "Assets/3. Prefab/Churu/Salmon/Salmon.prefab");
        var product = AssetDatabase.LoadAssetAtPath<GameObject>(
            "Assets/3. Prefab/Churu/Churub_Package_Defalt.prefab");
        Require(salmon != null && product != null, "Display prefabs missing");
        UnityEngine.Object.Instantiate(salmon, supply).transform.localPosition = Vector3.zero;
        UnityEngine.Object.Instantiate(salmon, input).transform.localPosition = Vector3.zero;
        UnityEngine.Object.Instantiate(product, output).transform.localPosition = Vector3.zero;
        for (int i = 1; i <= 6; i++)
            UnityEngine.Object.Instantiate(product, At(sale, "Display Slot " + i))
                .transform.localPosition = Vector3.zero;

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
        Require(Root(scene, "[ Player ]").GetComponent<Player>() != null, "Player missing");
        Require(Root(scene, "Canvas").GetComponentInChildren<UIManager>(true) != null, "UI missing");
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
        Require(ReferencePrefab(supply, "ingredientPrefab", ItemType.Ingredient),
            "Visible ingredient prefab missing");
        Require(ReferencePrefab(manual, "productPrefab", ItemType.Churu),
            "Visible product prefab missing");
        var slots = new SerializedObject(sales).FindProperty("displaySlots");
        Require(slots.arraySize == 6, "Sales display must have six slots");
        for (int i = 0; i < slots.arraySize; i++)
            Require(slots.GetArrayElementAtIndex(i).objectReferenceValue is Transform,
                "Sales display slot missing: " + i);
        Require(!Root(scene, "Layout Camera").activeSelf, "Second camera active");
        Require(!At(Root(scene, "01 Starter Workshop - 10 x 10 m").transform,
            "Player Scale Reference").gameObject.activeSelf, "Scale reference must be hidden");
        Require(!At(Root(scene, "Growth Layout").transform,
            "Stage 1 - First Automation").gameObject.activeSelf, "Stage 1 is active");
        Check(scene, "Stage 0 Gameplay", typeof(CompactSupplyStation),
            new Vector3(-3.5f, .1f, 2));
        Check(scene, "Stage 0 Gameplay", typeof(CompactManualStation),
            new Vector3(-.9f, .1f, 1.1f));
        Check(scene, "Stage 0 Gameplay", typeof(CompactManualOutput),
            new Vector3(.9f, .1f, 1.1f));
        Check(scene, "Stage 0 Gameplay", typeof(CompactSalesCounter),
            new Vector3(1.4f, .1f, -3.75f));
        Check(scene, "_Store/Common GameObjects", typeof(StoreInteraction),
            new Vector3(1.4f, .1f, -4.45f));
        foreach (var root in scene.GetRootGameObjects())
            foreach (var t in root.GetComponentsInChildren<Transform>(true))
                Require(GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(t.gameObject) == 0,
                    "Missing script: " + t.name);
        Debug.Log("COMPACT_FACTORY_PLAYTEST_VALIDATION_PASS");
    }

    static void Check(Scene scene, string path, Type endpointType, Vector3 expected)
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
        var factory = Root(scene, "Factory");
        var ingredientSource = At(factory.transform, "Container/IngredientSpawn")
            .GetComponent<IngredientMaker>();
        var productSource = At(factory.transform,
            "Machine/ChuruConveyerBelt Obj 1/ChuruStorage").GetComponent<BoxStorage>();
        var ingredientPrefab = new SerializedObject(ingredientSource)
            .FindProperty("objPrefab").objectReferenceValue as GameObject;
        var productPrefab = new SerializedObject(productSource)
            .FindProperty("churu").objectReferenceValue as GameObject;
        Require(ingredientPrefab != null && ingredientPrefab.GetComponent<Item>()?.Type == ItemType.Ingredient,
            "Ingredient prefab missing or invalid");
        Require(productPrefab != null && productPrefab.GetComponent<Item>()?.Type == ItemType.Churu,
            "Product prefab missing or invalid");

        var store = Root(scene, "_Store").GetComponent<Store>();
        store.enabled = false; // Stage zero sales now depend on visible stocked products.
        var group = new GameObject("Stage 0 Gameplay");
        if (group.scene != scene) SceneManager.MoveGameObjectToScene(group, scene);

        var supplyObject = Child(group.transform, "Container Supply");
        var supply = supplyObject.gameObject.AddComponent<CompactSupplyStation>();
        Assign(supply, "ingredientPrefab", ingredientPrefab);
        Assign(supply, "spawnPoint", Child(supplyObject, "Container Feed", new Vector3(-3.5f, .32f, 4.45f)));
        Assign(supply, "pickupTray", Child(supplyObject, "Pickup Tray", new Vector3(-3.5f, .25f, 3.29f)));

        var benchObject = Child(group.transform, "Manual Processing");
        var manual = benchObject.gameObject.AddComponent<CompactManualStation>();
        Assign(manual, "productPrefab", productPrefab);
        Assign(manual, "inputTray", Child(benchObject, "Ingredient Board", new Vector3(-.65f, 1.16f, 2.4f)));
        Assign(manual, "outputTray", Child(benchObject, "Finished Product Tray", new Vector3(.75f, 1.16f, 2.4f)));
        var output = benchObject.gameObject.AddComponent<CompactManualOutput>();
        Assign(output, "station", manual);

        var counterObject = Child(group.transform, "Front Sales Display");
        var counter = counterObject.gameObject.AddComponent<CompactSalesCounter>();
        var slots = new Transform[6];
        for (int row = 0; row < 2; row++)
            for (int column = 0; column < 3; column++)
                slots[row * 3 + column] = Child(counterObject,
                    "Display Slot " + (row * 3 + column + 1),
                    new Vector3(.95f + column * .45f, .88f, -5.16f - row * .32f));
        var serialized = new SerializedObject(counter);
        var display = serialized.FindProperty("displaySlots");
        display.arraySize = slots.Length;
        for (int i = 0; i < slots.Length; i++)
            display.GetArrayElementAtIndex(i).objectReferenceValue = slots[i];
        serialized.ApplyModifiedPropertiesWithoutUndo();

        Trigger(group.transform, "Take Salmon", new Vector3(-3.5f, .1f, 2), supply);
        Trigger(group.transform, "Place Salmon", new Vector3(-.9f, .1f, 1.1f), manual);
        Trigger(group.transform, "Take Churu", new Vector3(.9f, .1f, 1.1f), output);
        Trigger(group.transform, "Stock Sales Display", new Vector3(1.4f, .1f, -3.75f), counter);
    }

    static Transform Child(Transform parent, string name, Vector3? worldPosition = null)
    {
        var child = new GameObject(name).transform;
        child.SetParent(parent, false);
        if (worldPosition.HasValue) child.position = worldPosition.Value;
        return child;
    }

    static void Trigger(Transform parent, string name, Vector3 position, MonoBehaviour endpoint)
    {
        var target = Child(parent, name, position).gameObject;
        var collider = target.AddComponent<BoxCollider>();
        collider.isTrigger = true;
        collider.center = new Vector3(0, .9f, 0);
        collider.size = new Vector3(1.1f, 1.8f, 1.1f);
        var action = target.AddComponent<ItemTransfer>();
        Assign(action, "endpoint", endpoint);
        var point = target.AddComponent<WorkPoint>();
        Assign(point, "action", action);
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
