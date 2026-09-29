using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

// Authoring-only layout stages. No save state, purchases or production are simulated.
public static class FactoryGrowthLayout
{
    const string ScenePath="Assets/2. Scene/CompactFactory_Layout.unity";
    const string MaterialPath="Assets/Art/CompactFactoryLayout";
    const string GrowthName="Growth Layout";
    static Material floor,wall,trim,grass,road,paint,concrete;
    public static bool IsInstalled => SceneManager.GetActiveScene().path==ScenePath && FindRoot(GrowthName)!=null;
    static GameObject FindRoot(string name) => SceneManager.GetActiveScene().GetRootGameObjects().FirstOrDefault(o=>o.name==name);
    static Transform Child(Transform root,string name) => root.Find(name) ?? throw new InvalidOperationException("Missing layout object: "+name);
    static Material Mat(string name,Color color)
    {
        string path=MaterialPath+"/"+name+".mat";
        var m=AssetDatabase.LoadAssetAtPath<Material>(path);
        if(m==null) { m=new Material(Shader.Find("Standard")); AssetDatabase.CreateAsset(m,path); }
        m.color=color; m.SetFloat("_Glossiness",.1f); EditorUtility.SetDirty(m); return m;
    }
    static GameObject Group(string name,Transform parent=null)
    {
        var o=new GameObject(name); if(parent!=null)o.transform.SetParent(parent,false); return o;
    }
    static GameObject Block(string name,Transform parent,Vector3 center,Vector3 size,Material material)
    {
        var o=GameObject.CreatePrimitive(PrimitiveType.Cube); o.name=name; o.transform.SetParent(parent,false);
        o.transform.position=center; o.transform.localScale=size; o.GetComponent<Renderer>().sharedMaterial=material; return o;
    }
    static Bounds BoundsOf(GameObject o)
    {
        var rs=o.GetComponentsInChildren<Renderer>(true).Where(r=>!(r is ParticleSystemRenderer)).ToArray();
        if(rs.Length==0)throw new InvalidOperationException("No render mesh: "+o.name);
        var b=rs[0].bounds; foreach(var r in rs.Skip(1))b.Encapsulate(r.bounds); return b;
    }
    static void Place(GameObject o,Vector3 groundCenter)
    {
        var b=BoundsOf(o); o.transform.position+=groundCenter-new Vector3(b.center.x,b.min.y,b.center.z);
    }
    static GameObject MeshCopy(Transform source,Transform parent,string name)
    {
        var mf=source.GetComponent<MeshFilter>(); var mr=source.GetComponent<MeshRenderer>();
        if(mf==null || mr==null)throw new InvalidOperationException("Mesh missing: "+source.name);
        var o=Group(name,parent); o.transform.rotation=source.rotation; o.transform.localScale=source.lossyScale;
        o.AddComponent<MeshFilter>().sharedMesh=mf.sharedMesh;
        o.AddComponent<MeshRenderer>().sharedMaterials=mr.sharedMaterials; return o;
    }
    static GameObject VisualCopy(Transform source,Transform parent,string name,float scale=1)
    {
        var o=UnityEngine.Object.Instantiate(source.gameObject,parent); o.name=name; o.SetActive(true);
        foreach(var b in o.GetComponentsInChildren<MonoBehaviour>(true)) UnityEngine.Object.DestroyImmediate(b);
        foreach(var b in o.GetComponentsInChildren<Rigidbody>(true)) UnityEngine.Object.DestroyImmediate(b);
        foreach(var b in o.GetComponentsInChildren<UnityEngine.AI.NavMeshAgent>(true)) UnityEngine.Object.DestroyImmediate(b);
        foreach(var a in o.GetComponentsInChildren<Animator>(true)) a.enabled=false;
        o.transform.localScale*=scale; return o;
    }
    static GameObject Machine(Transform mesh,Transform parent,string name,Vector3 position)
    {
        var group=Group(name,parent);
        var body=MeshCopy(mesh,group.transform,"Processor Body");
        // Reuse the existing machine's core without its 13m conveyor assembly.
        var b=BoundsOf(body); float fit=Mathf.Min(1,2.8f/b.size.x,2.2f/b.size.z);
        body.transform.localScale*=fit; Place(body,position);
        Block("Machine Footprint",group.transform,position+new Vector3(0,.012f,0),new Vector3(3.8f,.024f,2.6f),trim);
        Block("Collection Marker",group.transform,position+new Vector3(0,.025f,-1.9f),new Vector3(.75f,.04f,.75f),paint);
        return group;
    }
    static void Fence(Transform parent,string name,float x,float z,float length,bool alongX)
    {
        Block(name,parent,new Vector3(x,.4f,z),alongX?new Vector3(length,.8f,.10f):new Vector3(.10f,.8f,length),trim);
        int posts=Mathf.CeilToInt(length/2);
        for(int i=0;i<=posts;i++)
            Block(name+" Post",parent,new Vector3(x+(alongX?-length/2+length*i/posts:0),.5f,z+(!alongX?-length/2+length*i/posts:0)),new Vector3(.17f,1,.17f),wall);
    }
    [MenuItem("Tools/Churub/Compact Factory/Build Final Growth Layout")]
    public static void Build()
    {
        var layout=EditorSceneManager.OpenScene(ScenePath);
        if(FindRoot(GrowthName)!=null)throw new InvalidOperationException("Final growth layout already exists; preserve hand edits instead of rebuilding.");
        string gameHash=Hash("Assets/2. Scene/Game.unity");
        var source=EditorSceneManager.OpenScene("Assets/2. Scene/Game.unity",OpenSceneMode.Additive);
        var roots=source.GetRootGameObjects();
        var factory=roots.First(o=>o.name=="Factory").transform;
        var machine=Child(factory,"Machine/ChuruConveyerBelt Obj 1").GetComponentsInChildren<MeshFilter>(true)
            .First(m=>m.name=="ChangePlace").transform;
        var truck=roots.First(o=>o.name=="Truck").transform;
        var buildings=roots.First(o=>o.name=="Buillding").transform;
        var tree=roots.First(o=>o.name=="Trees").transform.GetChild(0);
        SceneManager.SetActiveScene(layout);
        floor=AssetDatabase.LoadAssetAtPath<Material>(MaterialPath+"/Floor.mat");
        wall=AssetDatabase.LoadAssetAtPath<Material>(MaterialPath+"/Wall.mat");
        trim=AssetDatabase.LoadAssetAtPath<Material>(MaterialPath+"/Trim.mat");
        grass=Mat("Background Grass",new Color(.53f,.65f,.48f));
        road=Mat("Background Asphalt",new Color(.30f,.36f,.38f));
        paint=Mat("Route Marking",new Color(.91f,.73f,.39f));
        concrete=Mat("Service Yard",new Color(.64f,.68f,.65f));
        var starter=FindRoot("01 Starter Workshop - 10 x 10 m");
        foreach(var root in layout.GetRootGameObjects())
            if(root.name.StartsWith("03 ") || root.name.StartsWith("04 "))UnityEngine.Object.DestroyImmediate(root);
        var growth=Group(GrowthName);
        var auto=Group("Stage 1 - First Automation",growth.transform);
        Machine(machine,auto.transform,"Automatic Station 1",new Vector3(0,0,2.4f));
        for(int i=0;i<2;i++)
        {
            var wing=Group(i==0?"Stage 2 - Second Line":"Stage 3 - Third Line",growth.transform);
            float x=8+i*6;
            Block("Production Floor",wing.transform,new Vector3(x,-.12f,0),new Vector3(6,.24f,10),floor);
            Block("Front Workshop Wall",wing.transform,new Vector3(x,.32f,-5),new Vector3(6,.64f,.18f),trim);
            Machine(machine,wing.transform,"Automatic Station "+(i+2),new Vector3(x,0,2.4f));
            // Two marked work aisles, with more than 2m between adjacent machine footprints.
            for(int j=0;j<5;j++)Block("Aisle Dash",wing.transform,new Vector3(x-2+j,.012f,-1.5f),new Vector3(.45f,.024f,.06f),paint);
        }
        var shipping=Group("Stage 2 - Packing and Dispatch",growth.transform);
        Block("Packing Floor",shipping.transform,new Vector3(11,-.12f,9),new Vector3(12,.24f,8),floor);
        Block("Back Workshop Wall",shipping.transform,new Vector3(11,.95f,13),new Vector3(12,1.9f,.18f),wall);
        var table=MeshCopy(Child(factory,"Box Packaging/Packaging_Worktable"),shipping.transform,"Box Packing Station");
        Place(table,new Vector3(7.5f,0,8.5f));
        Block("Packing Interaction",shipping.transform,new Vector3(9,.03f,8.5f),new Vector3(.75f,.04f,.75f),paint);
        Block("Dispatch Apron",shipping.transform,new Vector3(18.5f,-.12f,9),new Vector3(3,.24f,8),concrete);
        var vehicle=MeshCopy(truck,shipping.transform,"Delivery Truck"); vehicle.transform.Rotate(0,180,0,Space.World); Place(vehicle,new Vector3(14,0,10));
        Block("Truck Load Marker",shipping.transform,new Vector3(11.5f,.03f,10),new Vector3(.8f,.04f,.8f),paint);
        var pallet=VisualCopy(Child(factory,"Box Packaging/Plallet Mint"),shipping.transform,"Dispatch Pallet"); Place(pallet,new Vector3(10,0,11.7f));
        var env=Group("Permanent Background",growth.transform);
        Block("District Ground",env.transform,new Vector3(7,-.38f,3),new Vector3(64,.24f,54),grass);
        Block("Customer Street",env.transform,new Vector3(7,-.13f,-10.5f),new Vector3(50,.2f,6),road);
        Block("Public Sidewalk West",env.transform,new Vector3(-10,-.12f,-6.25f),new Vector3(10,.24f,2.5f),wall);
        Block("Public Sidewalk East",env.transform,new Vector3(13.5f,-.12f,-6.25f),new Vector3(17,.24f,2.5f),wall);
        Block("Service Road",env.transform,new Vector3(23,-.13f,4),new Vector3(6,.2f,23),road);
        Block("Rear Truck Approach",env.transform,new Vector3(20,-.13f,10),new Vector3(6,.2f,4),road);
        for(int i=-6;i<=10;i++)Block("Street Center Dash",env.transform,new Vector3(i*2.5f,.005f,-10.5f),new Vector3(1.2f,.012f,.10f),wall);
        for(int i=0;i<8;i++)Block("Service Center Dash",env.transform,new Vector3(23,.005f,-5+i*2.5f),new Vector3(.10f,.012f,1.2f),wall);
        var reserved=Group("Reserved Land - until purchased",growth.transform);
        Block("Future Plot",reserved.transform,new Vector3(11,-.23f,4),new Vector3(12,.10f,18),concrete);
        Fence(reserved.transform,"East Plot Fence",17,4,18,false);
        Fence(reserved.transform,"Rear Plot Fence",11,13,12,true);
        var lastPlot=Group("Third Line Plot - until purchased",growth.transform);
        Block("Third Line Ground",lastPlot.transform,new Vector3(14,-.23f,0),new Vector3(6,.10f,10),concrete);
        Fence(lastPlot.transform,"Third Line Boundary",11,.0f,10,false);
        var endWall=Group("Expanded East Boundary",growth.transform);
        Block("East Workshop Wall",endWall.transform,new Vector3(17,.95f,1),new Vector3(.18f,1.9f,12),wall);
        // Background buildings stay outside ALL production, container and dispatch footprints.
        var house=VisualCopy(Child(buildings,"Building_House_02"),env.transform,"West Neighbor House");Place(house,new Vector3(-12,0,2));
        var mart=VisualCopy(Child(buildings,"Building_Mart"),env.transform,"West Neighbor Shop");Place(mart,new Vector3(-12,0,-4));
        var rear=VisualCopy(Child(buildings,"Building_House_01"),env.transform,"Rear Neighbor House");Place(rear,new Vector3(7,0,20));
        var far=VisualCopy(Child(buildings,"Building_House_03"),env.transform,"East Neighbor House");Place(far,new Vector3(31,0,6));
        foreach(var p in new[]{new Vector3(-8,0,6),new Vector3(-8,0,-2),new Vector3(-3,0,16),new Vector3(17,0,18),new Vector3(29,0,-4)})
        { var o=VisualCopy(tree,env.transform,"Background Tree",.55f);Place(o,p); }
        foreach(var p in new[]{new Vector3(-5.8f,0,-6.5f),new Vector3(7,0,-6.5f),new Vector3(19,0,-6.5f)})
        {var o=VisualCopy(Child(buildings,"Environment_Lamp"),env.transform,"Street Lamp",.65f);Place(o,p);}
        // Production expansion is accessed through the right shutter; frontend entrance never moves.
        EditorSceneManager.CloseScene(source,true);
        SetStage(0);
        EditorSceneManager.SaveScene(layout,ScenePath);
        RenderStages();
        Validate();
        if(gameHash!=Hash("Assets/2. Scene/Game.unity"))throw new InvalidOperationException("Original Game scene changed.");
        Debug.Log("FACTORY_GROWTH_LAYOUT_COMPLETE");
    }
    static string Hash(string path)
    { using(var hash=System.Security.Cryptography.SHA256.Create())return Convert.ToBase64String(hash.ComputeHash(File.ReadAllBytes(path))); }
    [MenuItem("Tools/Churub/Compact Factory/Stage 0 - Manual Shop")]
    public static void Stage0()=>SetStage(0);
    [MenuItem("Tools/Churub/Compact Factory/Stage 1 - First Automation")]
    public static void Stage1()=>SetStage(1);
    [MenuItem("Tools/Churub/Compact Factory/Stage 2 - Second Line and Dispatch")]
    public static void Stage2()=>SetStage(2);
    [MenuItem("Tools/Churub/Compact Factory/Stage 3 - Three Lines")]
    public static void Stage3()=>SetStage(3);
    public static void SetStage(int stage)
    {
        if(!IsInstalled || stage<0 || stage>3)throw new InvalidOperationException("Open final CompactFactory_Layout scene; stages are 0-3.");
        var growth=FindRoot(GrowthName).transform;
        Child(growth,"Stage 1 - First Automation").gameObject.SetActive(stage>=1);
        Child(growth,"Stage 2 - Second Line").gameObject.SetActive(stage>=2);
        Child(growth,"Stage 2 - Packing and Dispatch").gameObject.SetActive(stage>=2);
        Child(growth,"Stage 3 - Third Line").gameObject.SetActive(stage>=3);
        Child(growth,"Reserved Land - until purchased").gameObject.SetActive(stage<2);
        Child(growth,"Third Line Plot - until purchased").gameObject.SetActive(stage==2);
        Child(growth,"Expanded East Boundary").gameObject.SetActive(stage>=2);
        var starter=FindRoot("01 Starter Workshop - 10 x 10 m").transform;
        Child(starter,"Manual Processing Station").gameObject.SetActive(stage==0);
        Child(starter,"Processing Interaction").gameObject.SetActive(stage==0);
        FindRoot("02 Expansion Gate - closed at start").SetActive(stage<2);
        var camera=FindRoot("Layout Camera").GetComponent<Camera>();
        var focus=stage<2?new Vector3(0,0,1):new Vector3(7,0,4);
        camera.orthographicSize=stage<2?11.5f:21;
        camera.transform.position=focus+new Vector3(-17,24,-23);
        camera.transform.LookAt(focus);
        EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
    }
    static void Render(Camera camera,string path)
    {
        var rt=new RenderTexture(1800,1350,24){antiAliasing=4};var old=RenderTexture.active;
        var tex=new Texture2D(1800,1350,TextureFormat.RGB24,false);
        try { camera.targetTexture=rt;camera.Render();RenderTexture.active=rt;tex.ReadPixels(new Rect(0,0,1800,1350),0,0);tex.Apply();File.WriteAllBytes(path,tex.EncodeToPNG()); }
        finally {camera.targetTexture=null;RenderTexture.active=old;UnityEngine.Object.DestroyImmediate(tex);rt.Release();UnityEngine.Object.DestroyImmediate(rt);}
    }
    public static void RenderStages()
    {
        string[] names={"Starter","Automation","TwoLines","Expanded"};
        for(int stage=0;stage<4;stage++) {SetStage(stage);Render(FindRoot("Layout Camera").GetComponent<Camera>(),"docs/previews/CompactFactory_"+names[stage]+".png");}
        SetStage(3);
        var camera=FindRoot("Layout Camera").GetComponent<Camera>();
        camera.transform.position=new Vector3(7,40,4);camera.transform.rotation=Quaternion.Euler(90,0,0);camera.orthographicSize=21;
        Render(camera,"docs/previews/CompactFactory_Masterplan.png");
        SetStage(0);EditorSceneManager.SaveScene(SceneManager.GetActiveScene(),ScenePath);AssetDatabase.SaveAssets();
    }
    public static void AlignDispatchAndVerify()
    {
        EditorSceneManager.OpenScene(ScenePath);
        var source=EditorSceneManager.OpenScene("Assets/2. Scene/Game.unity",OpenSceneMode.Additive);
        var original=source.GetRootGameObjects().First(o=>o.name=="Truck");
        var rotation=Quaternion.AngleAxis(180,Vector3.up)*original.transform.rotation;
        EditorSceneManager.CloseScene(source,true);
        var vehicle=Child(FindRoot(GrowthName).transform,"Stage 2 - Packing and Dispatch/Delivery Truck").gameObject;
        vehicle.transform.rotation=rotation; Place(vehicle,new Vector3(14,0,10));
        RenderStages(); Validate();
        Debug.Log("FACTORY_GROWTH_FINAL_VERIFIED");
    }
    public static void Validate()
    {
        if(!IsInstalled)throw new InvalidOperationException("Final layout not open.");
        var report=new List<string>();
        for(int stage=0;stage<4;stage++)
        {
            SetStage(stage);
            int automatic=FindRoot(GrowthName).GetComponentsInChildren<Transform>().Count(t=>t.name.StartsWith("Automatic Station "));
            if(automatic!=stage)throw new InvalidOperationException("Unexpected machine count at stage "+stage+": "+automatic);
            bool manual=FindRoot("01 Starter Workshop - 10 x 10 m").transform.Find("Manual Processing Station").gameObject.activeSelf;
            if(manual!=(stage==0))throw new InvalidOperationException("Manual/automatic replacement mismatch.");
            report.Add("Stage "+stage+": "+automatic+" automatic stations; manual="+manual);
        }
        foreach(var root in SceneManager.GetActiveScene().GetRootGameObjects())
        foreach(var t in root.GetComponentsInChildren<Transform>(true))
        {
            if(GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(t.gameObject)>0)throw new InvalidOperationException("Missing script: "+t.name);
            var mf=t.GetComponent<MeshFilter>();if(mf!=null && mf.sharedMesh==null)throw new InvalidOperationException("Missing mesh: "+t.name);
            var r=t.GetComponent<Renderer>();if(r!=null && r.sharedMaterials.Any(m=>m==null))throw new InvalidOperationException("Missing material: "+t.name);
        }
        var truck=GameObject.Find("Delivery Truck");var b=BoundsOf(truck);
        if(b.min.z<5)throw new InvalidOperationException("Truck intrudes into front customer zone.");
        var env=Child(FindRoot(GrowthName).transform,"Permanent Background");
        foreach(Transform t in env)
        {
            if(!(t.name.Contains("Neighbor") || t.name=="Background Tree"))continue;
            var bb=BoundsOf(t.gameObject);
            bool overlap=bb.max.x>-5 && bb.min.x<17 && bb.max.z>-5 && bb.min.z<13;
            if(overlap)throw new InvalidOperationException("Background overlaps expansion plot: "+t.name);
        }
        report.Add("PASS: meshes/materials/scripts resolved; truck behind factory; scenery outside reserved footprint.");
        report.Add("Visual layout only: no gameplay, navigation or economy validation.");
        Directory.CreateDirectory("Logs/CompactFactory");
        File.WriteAllLines("Logs/CompactFactory/growth-validation.txt",report);
        SetStage(0);EditorSceneManager.SaveScene(SceneManager.GetActiveScene(),ScenePath);
    }
}
