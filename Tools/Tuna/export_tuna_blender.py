"""Run after build_tuna_blender.py. Export only the selected fish, no studio."""
import bpy
from pathlib import Path
from mathutils import Vector
import json, shutil

ROOT=Path(r'C:\Workspace\2.5D-Mobile')
STUDIO=Path(r'C:\Workspace\TunaStudio')
OUT=ROOT/'Assets/3. Prefab/Churu/Tuna'
bpy.ops.wm.open_mainfile(filepath=str(STUDIO/'Tuna.blend'))
for material in bpy.data.materials:
    if material.use_nodes:
        for node in material.node_tree.nodes:
            if node.type=='TEX_IMAGE' and node.image and not node.image.has_data:
                node.image=bpy.data.images.load(str(STUDIO/'Tuna_BaseColor.png'),check_existing=False)
                node.image.pack()
bpy.ops.wm.save_as_mainfile(filepath=str(STUDIO/'Tuna.blend'))
parts=list(bpy.context.selected_objects)
assert parts and all(p.type in {'MESH','CURVE'} for p in parts)
bpy.context.view_layer.objects.active=parts[0]
bpy.ops.object.convert(target='MESH')
bpy.ops.object.join()
fish=bpy.context.object;fish.name='Tuna_Rounded'
bpy.ops.object.transform_apply(location=False,rotation=True,scale=True)
bounds=[fish.matrix_world @ Vector(c) for c in fish.bound_box]
low=Vector(tuple(min(v[i] for v in bounds) for i in range(3)))
high=Vector(tuple(max(v[i] for v in bounds) for i in range(3)))
factor=.74/(high.x-low.x)
for v in fish.data.vertices:
    world=fish.matrix_world@v.co
    v.co=(world-Vector(((low.x+high.x)/2,(low.y+high.y)/2,low.z)))*factor
fish.location=(0,0,0);fish.rotation_euler=(0,0,0);fish.scale=(1,1,1)
bpy.ops.object.mode_set(mode='EDIT')
bpy.ops.mesh.select_all(action='SELECT')
bpy.ops.mesh.normals_make_consistent(inside=False)
bpy.ops.object.mode_set(mode='OBJECT')
fish.data.calc_loop_triangles()
report={'vertices':len(fish.data.vertices),'triangles':len(fish.data.loop_triangles),
        'length_m':.74,'materials':len(fish.data.materials),'rigged':False}
for material in fish.data.materials:
    if material and material.use_nodes:
        for node in material.node_tree.nodes:
            if node.type=='TEX_IMAGE' and node.image:
                image=node.image
                image.filepath_raw=str(OUT/'Tuna_BaseColor.png');image.save()
bpy.ops.export_scene.fbx(filepath=str(OUT/'Tuna_Rounded.fbx'),use_selection=True,
    object_types={'MESH'},axis_forward='-Z',axis_up='Y',bake_anim=False,
    add_leaf_bones=False,path_mode='COPY',embed_textures=False)
bpy.ops.export_scene.gltf(filepath=str(STUDIO/'Tuna_Rounded.glb'),use_selection=True,
    export_format='GLB',export_animations=False)
(STUDIO/'mesh_report.json').write_text(json.dumps(report,indent=2))
shutil.copy2(STUDIO/'Tuna_Blender_Preview.png',ROOT/'docs/previews/Tuna_Blender_Preview.png')
print('EXPORTED',report)
