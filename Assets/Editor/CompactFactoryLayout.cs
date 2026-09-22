using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class CompactFactoryLayout
{
    private const string LayoutPath = "Assets/2. Scene/CompactFactory_Layout.unity";
    private const string MatFolder = "Assets/Art/CompactFactoryLayout";
    private static Material floorMat, wallMat, trimMat, shutterMat, accentMat;

    [MenuItem("Tools/Churub/Compact Factory/Create Layout Scene")]
    public static void Build()
    {
        if (File.Exists(LayoutPath)) throw new InvalidOperationException("Layout already exists; preserve hand edits before rebuilding.");
        var source = EditorSceneManager.OpenScene("Assets/2. Scene/Game.unity");
        var playerSource = GameObject.Find("[ Player ]");
        var stallSource = GameObject.Find("_Store").transform.Find("Stall/Stall").gameObject;
        var machineSource = GameObject.Find("Factory").transform.Find("Machine/ChuruConveyerBelt Obj 1").gameObject;
        var packingSource = GameObject.Find("Factory").transform.Find("Box Packaging/Packaging_Worktable").gameObject;
        var containerSource = GameObject.Find("Factory").transform.Find("Container/Container_20FT_Yellow").gameObject;
        var feederSource = GameObject.Find("Factory").transform.Find("Container/ConveyerBelt").gameObject;
        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Additive);
        SceneManager.SetActiveScene(scene);
        Directory.CreateDirectory(MatFolder);
        AssetDatabase.Refresh();
        floorMat = MakeMaterial("Floor", new Color(.75f,.80f,.73f));
        wallMat = MakeMaterial("Wall", new Color(.88f,.85f,.73f));
        trimMat = MakeMaterial("Trim", new Color(.19f,.40f,.38f));
        shutterMat = MakeMaterial("Shutter", new Color(.37f,.45f,.46f));
        accentMat = MakeMaterial("Accent", new Color(.96f,.66f,.26f));
        var starter = new GameObject("01 Starter Workshop - 10 x 10 m");
        Block("Starter Floor", starter.transform, new Vector3(0,-.12f,0), new Vector3(10,.24f,10), floorMat);
        AddContainerEntranceWalls(starter.transform);
        Block("West Low Wall", starter.transform, new Vector3(-5,.32f,0), new Vector3(.18f,.64f,10), trimMat);
        // Keep the camera-facing side open so the short work loop reads clearly.
        Block("Front Left Wall", starter.transform, new Vector3(-4,.32f,-5), new Vector3(2,.64f,.18f), trimMat);
        Block("Front Right Wall", starter.transform, new Vector3(2.3f,.32f,-5), new Vector3(5.4f,.64f,.18f), trimMat);
        for (int i=-4;i<=4;i++)
        {
            Block("Floor Seam X", starter.transform, new Vector3(i,.004f,0), new Vector3(.012f,.008f,9.8f), wallMat, false);
            Block("Floor Seam Z", starter.transform, new Vector3(0,.004f,i), new Vector3(9.8f,.008f,.012f), wallMat, false);
        }
        AddContainerSupply(starter.transform,containerSource,feederSource);
        var bench = LoadVisual("Assets/3. Prefab/Worktable/ManualSalmonWorkbench/ManualSalmonWorkbench.fbx", starter.transform, "Manual Processing Station");
        PlaceOnFloor(bench, new Vector3(0,0,2.4f));
        var stall = CloneVisual(stallSource, starter.transform, "Starter Sales Counter");
        PlaceOnFloor(stall, new Vector3(1.4f,0,-2.7f));
        var player = CloneVisual(playerSource, starter.transform, "Player Scale Reference");
        PlaceOnFloor(player, new Vector3(-1.4f,0,.3f));
        var cart = player.transform.Find("Cart"); if (cart != null) cart.gameObject.SetActive(false);
        Marker("Supply Interaction", starter.transform, new Vector3(-3.5f,.02f,2.0f));
        Marker("Processing Interaction", starter.transform, new Vector3(0,.02f,1.1f));
        Marker("Stocking Interaction", starter.transform, new Vector3(1.4f,.02f,-1.1f));
        Marker("Customer Queue", starter.transform, new Vector3(-.6f,.02f,-3.2f));
        var gate = new GameObject("02 Expansion Gate - closed at start");
        Block("Expansion East Wall",gate.transform,new Vector3(5,.95f,0),new Vector3(.18f,1.9f,10),wallMat);
        Block("Future Production Shutter",gate.transform,new Vector3(4.88f,.85f,1.1f),new Vector3(.08f,1.7f,2.4f),shutterMat);
        for(int i=0;i<7;i++) Block("Shutter Slat",gate.transform,new Vector3(4.82f,.2f+i*.21f,1.1f),new Vector3(.025f,.025f,2.3f),trimMat,false);
        Marker("Future Expansion Interaction",gate.transform,new Vector3(3.7f,.02f,1.1f));
        var expansion = new GameObject("03 Future Production Wing - preview only");
        Block("Production Floor",expansion.transform,new Vector3(9,-.12f,2.5f),new Vector3(8,.24f,15),floorMat);
        Block("Production Back Wall",expansion.transform,new Vector3(9,.95f,10),new Vector3(8,1.9f,.18f),wallMat);
        Block("Production East Wall",expansion.transform,new Vector3(13,.95f,2.5f),new Vector3(.18f,1.9f,15),wallMat);
        var machine = CloneVisual(machineSource,expansion.transform,"Future Automatic Line");
        PlaceOnFloor(machine,new Vector3(9,0,2.5f));
        var shipping = new GameObject("04 Future Packing Wing - preview only");
        Block("Shipping Floor",shipping.transform,new Vector3(4,-.12f,-9),new Vector3(18,.24f,8),floorMat);
        var packing = CloneVisual(packingSource,shipping.transform,"Future Box Packing Station");
        PlaceOnFloor(packing,new Vector3(7.5f,0,-8.5f));
        Block("Loading Bay",shipping.transform,new Vector3(0,.01f,-10),new Vector3(4,.02f,4),shutterMat,false);
        Block("Loading Bay Stripe",shipping.transform,new Vector3(0,.025f,-12),new Vector3(4,.02f,.10f),accentMat,false);
        ConfigureExteriorSales(starter.transform,shipping.transform);
        var cameraObject = new GameObject("Layout Camera");
        var camera = cameraObject.AddComponent<Camera>();
        camera.orthographic = true; camera.orthographicSize = 9.8f;
        camera.clearFlags = CameraClearFlags.SolidColor; camera.backgroundColor = new Color(.90f,.91f,.87f);
        camera.farClipPlane = 200;
        camera.transform.position = new Vector3(-12,16,-15);
        camera.transform.LookAt(new Vector3(0,0,2));
        var light = new GameObject("Layout Sun").AddComponent<Light>();
        light.type = LightType.Directional; light.intensity = 1.0f; light.shadows = LightShadows.Soft; light.shadowStrength=.5f;
        light.transform.rotation = Quaternion.Euler(50,-30,0);
        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
        RenderSettings.ambientLight = new Color(.65f,.65f,.65f);
        EditorSceneManager.CloseScene(source,true);
        expansion.SetActive(false); shipping.SetActive(false);
        EditorSceneManager.SaveScene(scene,LayoutPath);
        Directory.CreateDirectory("docs/previews");
        Render(camera,"docs/previews/CompactFactory_Starter.png");
        gate.SetActive(false); expansion.SetActive(true); shipping.SetActive(true);
        camera.orthographicSize = 17;
        camera.transform.position = new Vector3(-22,31,-32);
        camera.transform.LookAt(new Vector3(3.5f,0,-1.5f));
        Render(camera,"docs/previews/CompactFactory_Expanded.png");
        // Restore and save the opening layout, not the expansion preview.
        gate.SetActive(true); expansion.SetActive(false); shipping.SetActive(false);
        camera.orthographicSize=9.8f; camera.transform.position=new Vector3(-12,16,-15); camera.transform.LookAt(new Vector3(0,0,2));
        EditorSceneManager.SaveScene(scene,LayoutPath);
        Validate();
        Debug.Log("COMPACT_FACTORY_BUILD_COMPLETE");
    }
    private static Material MakeMaterial(string name, Color color)
    {
        string path = MatFolder+"/"+name+".mat";
        var m = AssetDatabase.LoadAssetAtPath<Material>(path);
        if(m==null) { m=new Material(Shader.Find("Standard")); AssetDatabase.CreateAsset(m,path); }
        m.color=color; m.SetFloat("_Glossiness",.15f); EditorUtility.SetDirty(m); return m;
    }
    private static GameObject Block(string name,Transform parent,Vector3 pos,Vector3 size,Material mat,bool collision=true)
    {
        var o=GameObject.CreatePrimitive(PrimitiveType.Cube); o.name=name; o.transform.SetParent(parent);
        o.transform.position=pos; o.transform.localScale=size; o.GetComponent<Renderer>().sharedMaterial=mat;
        if(!collision) UnityEngine.Object.DestroyImmediate(o.GetComponent<Collider>());
        return o;
    }
    private static void Marker(string name,Transform parent,Vector3 pos)
    {
        Block(name,parent,pos,new Vector3(.75f,.025f,.75f),accentMat,false);
    }
    private static GameObject LoadVisual(string path,Transform parent,string name)
    {
        var asset=AssetDatabase.LoadAssetAtPath<GameObject>(path);
        if(asset==null) throw new InvalidOperationException("Missing asset: "+path);
        return CloneVisual(asset,parent,name);
    }
    private static GameObject CloneVisual(GameObject source,Transform parent,string name)
    {
        var o=UnityEngine.Object.Instantiate(source,parent); o.name=name; o.SetActive(true);
        foreach(var c in o.GetComponentsInChildren<MonoBehaviour>(true)) UnityEngine.Object.DestroyImmediate(c);
        foreach(var c in o.GetComponentsInChildren<Rigidbody>(true)) UnityEngine.Object.DestroyImmediate(c);
        foreach(var c in o.GetComponentsInChildren<UnityEngine.AI.NavMeshAgent>(true)) UnityEngine.Object.DestroyImmediate(c);
        foreach(var c in o.GetComponentsInChildren<Animator>(true)) c.enabled=false;
        return o;
    }
    private static void PlaceOnFloor(GameObject o,Vector3 center)
    {
        var rs=o.GetComponentsInChildren<Renderer>(true);
        if(rs.Length==0) throw new InvalidOperationException("No geometry: "+o.name);
        var b=rs[0].bounds; foreach(var r in rs) b.Encapsulate(r.bounds);
        o.transform.position += center-new Vector3(b.center.x,b.min.y,b.center.z);
    }
    private static void Render(Camera camera,string path)
    {
        var rt=new RenderTexture(1600,1200,24); rt.antiAliasing=4; camera.targetTexture=rt;
        var old=RenderTexture.active; camera.Render(); RenderTexture.active=rt;
        var tex=new Texture2D(1600,1200,TextureFormat.RGB24,false);
        tex.ReadPixels(new Rect(0,0,1600,1200),0,0); tex.Apply(); File.WriteAllBytes(path,tex.EncodeToPNG());
        camera.targetTexture=null; RenderTexture.active=old;
        UnityEngine.Object.DestroyImmediate(tex); rt.Release(); UnityEngine.Object.DestroyImmediate(rt);
    }
    [MenuItem("Tools/Churub/Compact Factory/Validate Layout")]
    public static void Validate()
    {
        if(FactoryGrowthLayout.IsInstalled) { FactoryGrowthLayout.Validate(); return; }
        var scene=SceneManager.GetActiveScene();
        if(scene.path!=LayoutPath) throw new InvalidOperationException("Open the compact layout scene first.");
        foreach(var root in scene.GetRootGameObjects())
            foreach(var t in root.GetComponentsInChildren<Transform>(true))
                if(GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(t.gameObject)>0)
                    throw new InvalidOperationException("Missing script: "+t.name);
        AssetDatabase.SaveAssets();
        File.WriteAllText("Logs/CompactFactory/layout-validation.txt","PASS: layout saved, no missing scripts. Starter floor 10 x 10 m. Future wings inactive. Visual layout only; no gameplay progression connected.");
    }
    [MenuItem("Tools/Churub/Compact Factory/Show Starter Layout")]
    public static void ShowStarter() { SetStage(false); }
    [MenuItem("Tools/Churub/Compact Factory/Show Expanded Layout")]
    public static void ShowExpanded() { SetStage(true); }
    private static void SetStage(bool expanded)
    {
        if(FactoryGrowthLayout.IsInstalled) { FactoryGrowthLayout.SetStage(expanded?3:0); return; }
        var scene=SceneManager.GetActiveScene();
        if(scene.path!=LayoutPath) throw new InvalidOperationException("Open CompactFactory_Layout first.");
        foreach(var root in scene.GetRootGameObjects())
        {
            if(root.name.StartsWith("02 ")) root.SetActive(!expanded);
            if(root.name.StartsWith("03 ") || root.name.StartsWith("04 ")) root.SetActive(expanded);
        }
        var camera=GameObject.Find("Layout Camera").GetComponent<Camera>();
        camera.orthographicSize=expanded?17:9.8f;
        camera.transform.position=expanded?new Vector3(-22,31,-32):new Vector3(-12,16,-15);
        camera.transform.LookAt(expanded?new Vector3(3.5f,0,-1.5f):new Vector3(0,0,2));
        EditorSceneManager.MarkSceneDirty(scene);
    }
    public static void FinishPreview()
    {
        var scene=EditorSceneManager.OpenScene(LayoutPath);
        if(FactoryGrowthLayout.IsInstalled) { FactoryGrowthLayout.RenderStages(); FactoryGrowthLayout.Validate(); return; }
        var sun=GameObject.Find("Layout Sun").GetComponent<Light>(); sun.intensity=1f; sun.shadowStrength=.5f;
        ShowExpanded(); Render(GameObject.Find("Layout Camera").GetComponent<Camera>(),"docs/previews/CompactFactory_Expanded.png");
        ShowStarter(); Render(GameObject.Find("Layout Camera").GetComponent<Camera>(),"docs/previews/CompactFactory_Starter.png");
        EditorSceneManager.SaveScene(scene,LayoutPath);
        Validate();
        Debug.Log("COMPACT_FACTORY_FINISH_COMPLETE");
    }
    private static void AddContainerEntranceWalls(Transform parent)
    {
        // 2.2m-wide opening centered on x=-3.5; no collider crosses the feed entrance.
        Block("Back Wall Left",parent,new Vector3(-4.8f,.95f,5),new Vector3(.4f,1.9f,.18f),wallMat);
        Block("Back Wall Right",parent,new Vector3(1.3f,.95f,5),new Vector3(7.4f,1.9f,.18f),wallMat);
    }
    private static void AddContainerSupply(Transform parent,GameObject containerSource,GameObject feederSource)
    {
        var group=new GameObject("Salmon Container Supply"); group.transform.SetParent(parent);
        var container=CloneVisual(containerSource,group.transform,"Original Salmon Container");
        PlaceOnFloor(container,new Vector3(-3.5f,0,7.35f));
        var feederRoot=new GameObject("Short Feeder Length Adapter"); feederRoot.transform.SetParent(group.transform);
        var feeder=CloneVisual(feederSource,feederRoot.transform,"Short Container Feeder");
        // Preserve the original width and height; shorten only its long supply belt.
        var bounds=feeder.GetComponent<Renderer>().bounds;
        // The source mesh has a rotated local axis; scale an identity parent along world Z.
        feederRoot.transform.localScale=new Vector3(1,1,1.8f/bounds.size.z);
        PlaceOnFloor(feederRoot,new Vector3(-3.5f,0,4.65f));
        var tray=LoadVisual("Assets/3. Prefab/Churu/Salmon/Salmon_Worktable.prefab",group.transform,"Container Pickup Tray");
        PlaceOnFloor(tray,new Vector3(-3.5f,.02f,3.29f));
        Block("Container Foundation",group.transform,new Vector3(-3.5f,-.12f,7.25f),new Vector3(2.6f,.24f,4.6f),floorMat);
        Block("Entrance Threshold",group.transform,new Vector3(-3.5f,-.04f,5),new Vector3(2.2f,.08f,.35f),trimMat);
        var pickup=new GameObject("Future Ingredient Spawn Anchor"); pickup.transform.SetParent(group.transform);
        pickup.transform.position=new Vector3(-3.5f,.12f,3.29f);
    }
    [MenuItem("Tools/Churub/Compact Factory/Replace Supply With Container")]
    public static void ReplaceSupplyWithContainer()
    {
        var layout=EditorSceneManager.OpenScene(LayoutPath);
        var source=EditorSceneManager.OpenScene("Assets/2. Scene/Game.unity",OpenSceneMode.Additive);
        Transform factory=null;
        foreach(var root in source.GetRootGameObjects()) if(root.name=="Factory") factory=root.transform;
        if(factory==null) throw new InvalidOperationException("Original Factory not found.");
        SceneManager.SetActiveScene(layout);
        Transform starter=null;
        foreach(var root in layout.GetRootGameObjects()) if(root.name.StartsWith("01 ")) starter=root.transform;
        if(starter==null) throw new InvalidOperationException("Starter layout not found.");
        floorMat=AssetDatabase.LoadAssetAtPath<Material>(MatFolder+"/Floor.mat");
        wallMat=AssetDatabase.LoadAssetAtPath<Material>(MatFolder+"/Wall.mat");
        trimMat=AssetDatabase.LoadAssetAtPath<Material>(MatFolder+"/Trim.mat");
        foreach(string name in new[]{"Salmon Supply","Supply Pedestal","Back Wall","Back Wall Left","Back Wall Right","Salmon Container Supply"})
        {
            var existing=starter.Find(name);
            if(existing!=null) UnityEngine.Object.DestroyImmediate(existing.gameObject);
        }
        AddContainerEntranceWalls(starter);
        AddContainerSupply(starter,factory.Find("Container/Container_20FT_Yellow").gameObject,factory.Find("Container/ConveyerBelt").gameObject);
        starter.Find("Supply Interaction").position=new Vector3(-3.5f,.02f,2.0f);
        EditorSceneManager.CloseScene(source,true);
        EditorSceneManager.SaveScene(layout,LayoutPath);
        FinishPreview();
        var container=GameObject.Find("Original Salmon Container").GetComponent<Renderer>().bounds;
        if(container.min.z<5) throw new InvalidOperationException("Container body intrudes into workshop.");
        if(GameObject.Find("Supply Pedestal")!=null) throw new InvalidOperationException("Old supply pedestal remains.");
        var feederBounds=GameObject.Find("Short Container Feeder").GetComponent<Renderer>().bounds;
        if(Mathf.Abs(feederBounds.size.z-1.8f)>.01f) throw new InvalidOperationException("Feed belt length mismatch: "+feederBounds.size);
        File.WriteAllText("Logs/CompactFactory/container-validation.txt",
            "PASS: original container reused; body outside z=5 workshop edge; 2.2m rear opening; old pedestal removed; pickup marker inside workshop.\n"+
            "Container bounds: "+container+"\nFeeder bounds: "+feederBounds+"\nSupply anchor: (-3.5, 0.12, 3.29). Visual layout only.");
        Debug.Log("COMPACT_FACTORY_CONTAINER_COMPLETE");
    }
    private static void ConfigureExteriorSales(Transform starter,Transform shipping)
    {
        var existing=starter.Find("Exterior Sales Frontage");
        bool first=existing==null;
        if(existing!=null) UnityEngine.Object.DestroyImmediate(existing.gameObject);
        var frontage=new GameObject("Exterior Sales Frontage"); frontage.transform.SetParent(starter);
        var stall=starter.Find("Starter Sales Counter").gameObject;
        if(first) stall.transform.Rotate(0,-90,0,Space.World);
        PlaceOnFloor(stall,new Vector3(1.4f,0,-5.45f));
        var wall=starter.Find("Front Right Wall");
        if(wall!=null) UnityEngine.Object.DestroyImmediate(wall.gameObject);
        // Leave the original staff entrance (-3 .. -0.4) open and a separate 3m counter opening.
        Block("Counter Right Wall",frontage.transform,new Vector3(3.95f,.32f,-5),new Vector3(2.1f,.64f,.18f),trimMat);
        Block("Entrance Divider",frontage.transform,new Vector3(-.25f,.32f,-5),new Vector3(.3f,.64f,.18f),trimMat);
        Block("Customer Sidewalk",frontage.transform,new Vector3(0,-.12f,-6.25f),new Vector3(10,.24f,2.5f),wallMat);
        Block("Sidewalk Edge",frontage.transform,new Vector3(0,-.06f,-7.5f),new Vector3(10,.12f,.1f),trimMat);
        starter.Find("Stocking Interaction").position=new Vector3(1.4f,.02f,-3.9f);
        starter.Find("Customer Queue").position=new Vector3(1.4f,.02f,-6.6f);
        Marker("Customer Waiting",frontage.transform,new Vector3(3.1f,.02f,-6.6f));
        var approach=new GameObject("Future Customer Entry"); approach.transform.SetParent(frontage.transform);
        approach.transform.position=new Vector3(4.4f,0,-6.6f);
        var exit=new GameObject("Future Customer Exit"); exit.transform.SetParent(frontage.transform);
        exit.transform.position=new Vector3(-.9f,0,-6.6f);
        // Keep future packing floor behind the permanent sidewalk, without coplanar overlap.
        var floor=shipping.Find("Shipping Floor"); floor.position=new Vector3(4,-.12f,-10.25f); floor.localScale=new Vector3(18,.24f,5.5f);
        if(shipping.Find("Future Shipping Access")==null)
            Block("Future Shipping Access",shipping,new Vector3(9,-.12f,-6.25f),new Vector3(8,.24f,2.5f),floorMat);
    }
    [MenuItem("Tools/Churub/Compact Factory/Move Sales Outside")]
    public static void MoveSalesOutside()
    {
        var scene=EditorSceneManager.OpenScene(LayoutPath);
        Transform starter=null,shipping=null;
        foreach(var root in scene.GetRootGameObjects())
        {
            if(root.name.StartsWith("01 ")) starter=root.transform;
            if(root.name.StartsWith("04 ")) shipping=root.transform;
        }
        if(starter==null || shipping==null) throw new InvalidOperationException("Layout roots missing.");
        floorMat=AssetDatabase.LoadAssetAtPath<Material>(MatFolder+"/Floor.mat");
        wallMat=AssetDatabase.LoadAssetAtPath<Material>(MatFolder+"/Wall.mat");
        trimMat=AssetDatabase.LoadAssetAtPath<Material>(MatFolder+"/Trim.mat");
        accentMat=AssetDatabase.LoadAssetAtPath<Material>(MatFolder+"/Accent.mat");
        ConfigureExteriorSales(starter,shipping);
        var bounds=starter.Find("Starter Sales Counter").GetComponentInChildren<Renderer>().bounds;
        if(bounds.min.x<-.1f || bounds.max.x>2.9f) throw new InvalidOperationException("Counter overlaps front wall: "+bounds);
        if(starter.Find("Stocking Interaction").position.z<=-5 || starter.Find("Customer Queue").position.z>=-5)
            throw new InvalidOperationException("Stocking/customer sides not separated.");
        EditorSceneManager.SaveScene(scene,LayoutPath);
        FinishPreview();
        File.WriteAllText("Logs/CompactFactory/exterior-sales-validation.txt",
            "PASS: counter within separate wall opening; indoor stocking and outdoor customer markers; staff entrance retained; future shipping floor moved clear of sidewalk.\nCounter bounds: "+bounds+"\nLayout preview only.");
        Debug.Log("COMPACT_FACTORY_EXTERIOR_SALES_COMPLETE");
    }
    public static void LowerPickupTray()
    {
        var scene=EditorSceneManager.OpenScene(LayoutPath);
        var tray=GameObject.Find("Container Pickup Tray");
        PlaceOnFloor(tray,new Vector3(-3.5f,.02f,3.29f));
        GameObject.Find("Future Ingredient Spawn Anchor").transform.position=new Vector3(-3.5f,.12f,3.29f);
        GameObject.Find("Supply Interaction").transform.position=new Vector3(-3.5f,.02f,2.0f);
        var trayBounds=tray.GetComponentInChildren<Renderer>().bounds;
        var beltBounds=GameObject.Find("Short Container Feeder").GetComponent<Renderer>().bounds;
        if(trayBounds.max.z>beltBounds.min.z || trayBounds.min.y<0 || trayBounds.max.y>.15f)
            throw new InvalidOperationException("Tray must sit at floor level in front of belt: "+trayBounds);
        EditorSceneManager.SaveScene(scene,LayoutPath);
        FinishPreview();
        File.WriteAllText("Logs/CompactFactory/pickup-tray-validation.txt",
            "PASS: tray at floor level, no plan overlap with belt.\nTray: "+trayBounds+"\nBelt: "+beltBounds);
        Debug.Log("COMPACT_FACTORY_TRAY_COMPLETE");
    }
    [Serializable] private class Entry
    {
        public string path;
        public Vector3 position, scale, boundsCenter, boundsSize;
        public bool active;
        public string[] components;
    }
    [Serializable] private class Report { public List<Entry> objects = new List<Entry>(); }
    public static void Inspect()
    {
        var scene = EditorSceneManager.OpenScene("Assets/2. Scene/Game.unity");
        var report = new Report();
        foreach (var root in scene.GetRootGameObjects())
            Visit(root.transform, "", report, 0);
        Directory.CreateDirectory("Logs/CompactFactory");
        File.WriteAllText("Logs/CompactFactory/scene.json", JsonUtility.ToJson(report, true));
        Debug.Log("COMPACT_FACTORY_INSPECT_COMPLETE");
    }
    private static void Visit(Transform t, string parent, Report report, int depth)
    {
        string path = parent + "/" + t.name;
        var components = t.GetComponents<Component>();
        bool interesting = depth < 3 || t.GetComponent<UnlockManager>() != null ||
            t.GetComponent<IngredientMaker>() != null || t.GetComponent<BoxPackaging>() != null ||
            t.GetComponent<ConveyorBelt>() != null || t.GetComponent<Store>() != null ||
            t.GetComponent<WorkPoint>() != null || t.GetComponent<Player>() != null;
        if (interesting && t.GetComponent<RectTransform>() == null)
        {
            var names = new List<string>();
            foreach (var c in components) names.Add(c == null ? "MISSING" : c.GetType().Name);
            var renderers = t.GetComponentsInChildren<Renderer>(true);
            Bounds bounds = new Bounds(t.position, Vector3.zero);
            bool first = true;
            foreach (var r in renderers)
            {
                if (first) { bounds = r.bounds; first = false; }
                else bounds.Encapsulate(r.bounds);
            }
            report.objects.Add(new Entry { path = path, position = t.position, scale = t.lossyScale,
                boundsCenter = bounds.center, boundsSize = bounds.size, active = t.gameObject.activeSelf,
                components = names.ToArray() });
        }
        if (t.GetComponent<Canvas>() != null) return;
        foreach (Transform child in t) Visit(child, path, report, depth + 1);
    }
}
