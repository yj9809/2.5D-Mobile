using UnityEditor;
using UnityEngine;

// Optional import step: leaves all existing production references unchanged.
public static class TunaPrefabBuilder
{
    private const string Folder = "Assets/3. Prefab/Churu/Tuna";

    [MenuItem("Tools/Churub/Create Tuna Prefab")]
    public static void Create()
    {
        string path = Folder + "/Tuna.prefab";
        if (AssetDatabase.LoadAssetAtPath<GameObject>(path) != null)
        {
            Selection.activeObject = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            return;
        }
        AssetDatabase.ImportAsset(Folder + "/Tuna.obj", ImportAssetOptions.ForceSynchronousImport);
        var model = AssetDatabase.LoadAssetAtPath<GameObject>(Folder + "/Tuna.obj");
        if (model == null) throw new System.InvalidOperationException("Tuna OBJ could not be imported.");
        var root = new GameObject("Tuna");
        try
        {
            var visual = Object.Instantiate(model, root.transform);
            visual.name = "Visual";
            // The mesh is modeled in meters with Y up; normalize imported bounds.
            var renderers = visual.GetComponentsInChildren<Renderer>();
            if (renderers.Length == 0) throw new System.InvalidOperationException("Tuna has no renderer.");
            var bounds = renderers[0].bounds;
            foreach (var renderer in renderers) bounds.Encapsulate(renderer.bounds);
            visual.transform.localScale *= .74f / bounds.size.x;
            bounds = renderers[0].bounds;
            foreach (var renderer in renderers) bounds.Encapsulate(renderer.bounds);
            visual.transform.position -= new Vector3(bounds.center.x, bounds.min.y, bounds.center.z);
            var collider = root.AddComponent<BoxCollider>();
            collider.size = bounds.size;
            collider.center = new Vector3(0, bounds.size.y / 2, 0);
            root.AddComponent<Rigidbody>();
            var item = root.AddComponent<Item>();
            var serialized = new SerializedObject(item);
            serialized.FindProperty("type").enumValueIndex = (int)ItemType.Ingredient;
            serialized.ApplyModifiedPropertiesWithoutUndo();
            root.layer = 6;
            root.tag = "Ingredient";
            Selection.activeObject = PrefabUtility.SaveAsPrefabAsset(root, path);
            AssetDatabase.SaveAssets();
        }
        finally { Object.DestroyImmediate(root); }
    }
}
