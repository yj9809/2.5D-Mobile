"""Blender 4.5: build and render a rounded tuna; source scene stays outside Unity."""
import bpy, math, os
from mathutils import Vector
from pathlib import Path

ROOT=Path(r'C:\Workspace\2.5D-Mobile')
STUDIO=Path(r'C:\Workspace\TunaStudio')
STUDIO.mkdir(exist_ok=True)
bpy.ops.object.select_all(action='SELECT')
bpy.ops.object.delete(use_global=False)
parts=[]

def mat(name, color, rough=.4, metal=0):
    m=bpy.data.materials.new(name);m.diffuse_color=(*color,1);m.use_nodes=True
    b=m.node_tree.nodes.get('Principled BSDF')
    b.inputs['Base Color'].default_value=(*color,1)
    b.inputs['Roughness'].default_value=rough;b.inputs['Metallic'].default_value=metal
    return m
navy=mat('Tuna_Fin',(.055,.09,.19),.38)
gold=mat('Tuna_Finlets',(.58,.40,.12),.42)
black=mat('Tuna_Eyes',(.012,.009,.007),.16)
rim=mat('Tuna_Eye_Rim',(.24,.24,.23),.36,.12)
white=mat('Tuna_Eye_Glint',(.98,.95,.83),.22)
mouthmat=mat('Tuna_Mouth',(.075,.055,.041),.55)
bodymat=mat('Tuna_Body',(.3,.4,.55),.4,.08)

# UV image paints continuous height bands instead of face-assigned stripes.
palette=[(-1,(.84,.80,.69)),(-.45,(.76,.79,.78)),(-.08,(.53,.64,.73)),
         (.12,(.39,.53,.68)),(.28,(.23,.37,.57)),(.52,(.09,.17,.34)),(1,(.035,.075,.19))]
def color(h):
    for (a,c),(b,d) in zip(palette,palette[1:]):
        if a<=h<=b:
            t=(h-a)/(b-a);t=t*t*(3-2*t)
            return tuple(c[k]*(1-t)+d[k]*t for k in range(3))
    return palette[-1][1] if h>1 else palette[0][1]
size=512
img=bpy.data.images.new('Tuna_BaseColor',width=size,height=size,alpha=True)
pixels=[]
for j in range(size):
    a=math.pi*(j+.5)/size
    for i in range(size):
        b=2*math.pi*(i+.5)/size
        c=color(math.sin(a)*math.cos(b))
        pixels.extend((*c,1))
img.pixels.foreach_set(pixels)
img.filepath_raw=str(STUDIO/'Tuna_BaseColor.png');img.file_format='PNG';img.save();img.pack()
tex=bodymat.node_tree.nodes.new('ShaderNodeTexImage');tex.image=img
bodymat.node_tree.links.new(tex.outputs['Color'],bodymat.node_tree.nodes.get('Principled BSDF').inputs['Base Color'])

verts=[];uvs=[];faces=[]
N,R=64,40
for j in range(R+1):
    a=math.pi*j/R
    x=-.12+1.22*math.cos(a)
    taper=1-.30*max(math.cos(a),0)
    for i in range(N+1):
        b=2*math.pi*i/N
        verts.append((x,.56*math.sin(a)*math.sin(b)*taper,
                      .70+.70*math.sin(a)*math.cos(b)*taper))
        uvs.append((i/N,j/R))
for j in range(R):
    for i in range(N):
        q=j*(N+1)+i
        # Parameterization uses Y=sin, Z=cos: reverse winding from Y-up source.
        if j>0:faces.append((q,q+1,q+N+1))
        if j<R-1:faces.append((q+1,q+N+2,q+N+1))
mesh=bpy.data.meshes.new('TunaBodyMesh');mesh.from_pydata(verts,[],faces);mesh.update()
uv=mesh.uv_layers.new(name='UVMap')
for p in mesh.polygons:
    p.use_smooth=True
    for li in p.loop_indices:uv.data[li].uv=uvs[mesh.loops[li].vertex_index]
body=bpy.data.objects.new('Tuna_Body',mesh);bpy.context.collection.objects.link(body)
body.data.materials.append(bodymat);parts.append(body)

def sphere(name,loc,scale,material,seg=32,rings=20):
    bpy.ops.mesh.primitive_uv_sphere_add(segments=seg,ring_count=rings,location=loc)
    o=bpy.context.object;o.name=name;o.scale=scale
    bpy.ops.object.transform_apply(location=False,rotation=False,scale=True)
    o.data.materials.append(material)
    for p in o.data.polygons:p.use_smooth=True
    parts.append(o);return o

def fin(name,outline,width,material):
    # Rounded solid silhouette, beveled to avoid paper-thin triangles.
    vs=[(x,y+s*width/2,z) for s in [-1,1] for x,y,z in outline]
    n=len(outline)
    fs=[tuple(reversed(range(n))),tuple(range(n,2*n))]
    for i in range(n):k=(i+1)%n;fs.append((i,k,k+n,i+n))
    me=bpy.data.meshes.new(name);me.from_pydata(vs,[],fs);me.update()
    ob=bpy.data.objects.new(name,me);bpy.context.collection.objects.link(ob)
    ob.data.materials.append(material)
    bpy.context.view_layer.objects.active=ob;ob.select_set(True)
    bevel=ob.modifiers.new('Soft edges','BEVEL');bevel.width=.035;bevel.segments=3
    bevel.affect='EDGES'
    for p in me.polygons:p.use_smooth=True
    weighted=ob.modifiers.new('Weighted normals','WEIGHTED_NORMAL')
    parts.append(ob);ob.select_set(False)
    return ob

sphere('Tail_Base',(1.02,0,.70),(.26,.13,.145),navy)
fin('Crescent_Tail',[(1.09,0,.70),(1.25,0,.87),(1.48,0,1.22),(1.65,0,1.34),
    (1.57,0,1.09),(1.43,0,.79),(1.41,0,.70),(1.43,0,.61),(1.57,0,.31),
    (1.65,0,.06),(1.48,0,.18),(1.25,0,.53)],.115,navy)
fin('Dorsal_Fin',[(-.39,0,1.25),(-.15,0,1.51),(.10,0,1.69),(.20,0,1.71),
    (.12,0,1.51),(.14,0,1.28),(.30,0,1.19)],.10,navy)
fin('Second_Dorsal',[(.42,0,1.19),(.65,0,1.36),(.65,0,1.12)],.065,navy)
for side in [-1,1]:
    sphere('Eye_Rim_'+str(side),(-.94,side*.415,.80),(.153,.043,.161),rim)
    sphere('Eye_'+str(side),(-.95,side*.445,.81),(.130,.063,.137),black)
    sphere('Eye_Glint_'+str(side),(-.99,side*.50,.865),(.036,.016,.036),white,20,12)
    fin('Pectoral_'+str(side),[(-.48,side*.57,.82),(-.24,side*.60,.87),
        (.13,side*.60,.79),(.25,side*.59,.75),(.11,side*.60,.69),(-.22,side*.60,.63)],.055,navy)
    fin('Pelvic_'+str(side),[(-.22,side*.22,.12),(.05,side*.29,.035),
        (.15,side*.31,.07),(-.03,side*.25,.24)],.05,navy)
for i in range(4):
    x=.69+i*.09; z=1.08-i*.066
    fin('Finlet_'+str(i),[(x-.05,0,z),(x+.025,0,z+.115),(x+.07,0,z-.02)],.04,gold)

def curve(name,pts,radius,material):
    cu=bpy.data.curves.new(name,'CURVE');cu.dimensions='3D';cu.bevel_depth=radius;cu.bevel_resolution=3
    sp=cu.splines.new('BEZIER');sp.bezier_points.add(len(pts)-1)
    for b,p in zip(sp.bezier_points,pts):b.co=p;b.handle_left_type='AUTO';b.handle_right_type='AUTO'
    ob=bpy.data.objects.new(name,cu);bpy.context.collection.objects.link(ob);ob.data.materials.append(material);parts.append(ob)
curve('Small_Mouth',[(-1.326,-.025,.67),(-1.28,-.18,.60),(-1.17,-.285,.585),(-1.12,-.31,.61)],.010,mouthmat)

# Subtle gill seam follows the body surface on both sides.
for side in [-1,1]:
    pts=[]
    for k in range(9):
        z=1.11-k*.092
        x=-.61+.075*math.sin(k/8*math.pi)
        a=math.acos((x+.12)/1.22)
        y=.56*math.sin(a)*math.sqrt(max(0,1-((z-.70)/(.70*math.sin(a)))**2))
        pts.append((x,side*(y+.006),z))
    curve('Gill_'+str(side),pts,.004,rim)

# Studio scene is not exported with the model.
floor=mat('Studio_Cream',(.88,.85,.77),.8)
bpy.ops.mesh.primitive_plane_add(size=200,location=(0,0,-.035));bpy.context.object.data.materials.append(floor)
def area(name,loc,power,size):
    bpy.ops.object.light_add(type='AREA',location=loc);o=bpy.context.object;o.name=name;o.data.energy=power;o.data.shape='DISK';o.data.size=size;o.rotation_euler=(Vector((0,0,.6))-o.location).to_track_quat('-Z','Y').to_euler()
area('Large_Key',(-3,-4,6),450,5)
area('Soft_Fill',(1,-1,3),110,4)
area('Rim',(2,3,4),300,3)
bpy.ops.object.camera_add(location=(-2.5,-6.5,2.8))
cam=bpy.context.object;cam.rotation_euler=(Vector((.05,0,.83))-cam.location).to_track_quat('-Z','Y').to_euler();cam.data.type='ORTHO';cam.data.ortho_scale=3.9
scene=bpy.context.scene;scene.camera=cam;scene.render.engine='CYCLES';scene.cycles.samples=48
scene.cycles.use_denoising=True
scene.world.use_nodes=True
scene.world.node_tree.nodes['Background'].inputs[0].default_value=(.78,.80,.85,1)
scene.world.node_tree.nodes['Background'].inputs[1].default_value=.55
scene.render.resolution_x=1400;scene.render.resolution_y=1050;scene.render.resolution_percentage=100
scene.view_settings.view_transform='AgX'
scene.render.image_settings.file_format='PNG'
scene.render.filepath=str(STUDIO/'Tuna_Blender_Preview.png')
# Keep selected model pieces separate and editable in the source .blend.
bpy.ops.object.select_all(action='DESELECT')
for p in parts:p.select_set(True)
bpy.context.view_layer.objects.active=body
bpy.ops.wm.save_as_mainfile(filepath=str(STUDIO/'Tuna.blend'))
bpy.ops.render.render(write_still=True)
print('TUNA_RENDER_COMPLETE')
