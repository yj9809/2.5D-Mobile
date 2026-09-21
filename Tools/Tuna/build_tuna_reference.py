"""Reference-led tuna study: tapered body, inset eyes and broad matte facets.
Run in Blender 4.5 background mode. All renders are from the actual mesh.
"""
import bpy, math, random
from pathlib import Path
from mathutils import Vector
from mathutils.geometry import tessellate_polygon

STUDIO=Path(r'C:\Workspace\TunaStudio\Reference')
STUDIO.mkdir(parents=True,exist_ok=True)
bpy.ops.object.select_all(action='SELECT');bpy.ops.object.delete(use_global=False)
parts=[]
random.seed(41)

def material(name,c,rough=.6,metal=0):
    m=bpy.data.materials.new(name);m.use_nodes=True;m.diffuse_color=(*c,1)
    b=m.node_tree.nodes.get('Principled BSDF');b.inputs['Base Color'].default_value=(*c,1)
    b.inputs['Roughness'].default_value=rough;b.inputs['Metallic'].default_value=metal
    return m

navy=material('Tuna_Navy',(.047,.075,.16),.58)
gold=material('Tuna_Gold',(.48,.34,.11),.6)
eye=material('Tuna_Eye',(.009,.006,.004),.19)
rim=material('Tuna_Eye_Rim',(.13,.105,.075),.48)
glint=material('Tuna_Glint',(.95,.93,.86),.2)
seam=material('Tuna_Seam',(.16,.18,.20),.8)
bodymat=material('Tuna_Painted_Body',(.5,.5,.5),.72,.03)

# x, center height, vertical radius, lateral radius: a fish profile, not an ellipsoid.
profile=[(-1.50,.65,.025,.028),(-1.43,.67,.13,.12),(-1.28,.70,.30,.24),
 (-1.08,.74,.45,.36),(-.84,.77,.56,.45),(-.58,.80,.63,.51),
 (-.30,.82,.65,.53),(-.02,.84,.62,.51),(.25,.86,.55,.45),
 (.49,.88,.44,.36),(.72,.90,.33,.27),(.94,.92,.22,.18),
 (1.14,.94,.12,.105),(1.31,.95,.057,.055),(1.38,.95,.040,.039)]

def section(x):
    for a,b in zip(profile,profile[1:]):
        if a[0]<=x<=b[0]:
            t=(x-a[0])/(b[0]-a[0]);return tuple(a[k]*(1-t)+b[k]*t for k in range(1,4))
    return profile[0][1:] if x<profile[0][0] else profile[-1][1:]

def surface_y(x,z):
    center,rz,ry=section(x)
    return ry*math.sqrt(max(0,1-((z-center)/rz)**2))

N=28;vs=[];fs=[]
for j,(x,center,rz,ry) in enumerate(profile):
    for i in range(N):
        angle=2*math.pi*(i+(.24 if j%2 else 0))/N
        vs.append((x,ry*math.sin(angle),center+rz*math.cos(angle)))
for j in range(len(profile)-1):
    for i in range(N):
        a=j*N+i;b=j*N+(i+1)%N;c=(j+1)*N+i;d=(j+1)*N+(i+1)%N
        fs.extend([(a,c,b),(b,c,d)] if (i+j)%2 else [(a,c,d),(a,d,b)])
fs.extend([tuple(reversed(range(N))),tuple((len(profile)-1)*N+i for i in range(N))])
mesh=bpy.data.meshes.new('Reference_Tuna_Body');mesh.from_pydata(vs,[],fs);mesh.update()
body=bpy.data.objects.new('Tuna_Body',mesh);bpy.context.collection.objects.link(body);parts.append(body)
body.data.materials.append(bodymat)

# One texture atlas supplies controlled face colors; no separate material per polygon.
def paint(h):
    stops=[(-1,(.78,.74,.65)),(-.35,(.69,.72,.73)),(.02,(.47,.56,.67)),
           (.30,(.29,.40,.58)),(.44,(.12,.22,.42)),(.70,(.06,.12,.27)),(1,(.04,.08,.18))]
    for (a,c),(b,d) in zip(stops,stops[1:]):
        if a<=h<=b:
            t=(h-a)/(b-a)
            return tuple(c[k]*(1-t)+d[k]*t for k in range(3))
    return stops[0][1] if h<0 else stops[-1][1]

size,tile=512,16;pixels=[1.0]*(size*size*4)
uv=mesh.uv_layers.new(name='UVMap')
for idx,p in enumerate(mesh.polygons):
    center=sum((mesh.vertices[i].co for i in p.vertices),Vector())/len(p.vertices)
    cz,rz,ry=section(center.x)
    h=(center.z-cz)/max(rz,.04)
    c=paint(h);variation=random.uniform(.94,1.05)
    tx=(idx%32)*tile;ty=(idx//32)*tile
    for py in range(ty,ty+tile):
        for px in range(tx,tx+tile):
            grain=random.uniform(.985,1.015)
            k=(py*size+px)*4
            pixels[k:k+4]=[min(1,v*variation*grain) for v in c]+[1]
    corners=[(3,3),(13,3),(8,13)]
    for k,li in enumerate(p.loop_indices):
        a,b=corners[k%3];uv.data[li].uv=((tx+a)/size,(ty+b)/size)
    p.use_smooth=False
image=bpy.data.images.new('Tuna_Reference_BaseColor',width=size,height=size,alpha=True)
image.pixels.foreach_set(pixels);image.filepath_raw=str(STUDIO/'Tuna_Reference_BaseColor.png')
image.file_format='PNG';image.save();image.pack()
tex=bodymat.node_tree.nodes.new('ShaderNodeTexImage');tex.image=image;tex.interpolation='Linear'
bodymat.node_tree.links.new(tex.outputs['Color'],bodymat.node_tree.nodes.get('Principled BSDF').inputs['Base Color'])

def sphere(name,loc,scale,mat,segments=24,rings=14):
    bpy.ops.mesh.primitive_uv_sphere_add(segments=segments,ring_count=rings,location=loc)
    o=bpy.context.object;o.name=name;o.scale=scale
    bpy.ops.object.transform_apply(location=False,rotation=False,scale=True)
    o.data.materials.append(mat)
    for p in o.data.polygons:p.use_smooth=True
    parts.append(o);return o

def fin(name,outline,width,mat,bevel=.012):
    n=len(outline);vertices=[(x,y+s*width/2,z) for s in [-1,1] for x,y,z in outline]
    polygons=[tuple(reversed(range(n))),tuple(range(n,2*n))]
    for i in range(n):k=(i+1)%n;polygons.append((i,k,k+n,i+n))
    me=bpy.data.meshes.new(name);me.from_pydata(vertices,[],polygons);me.update()
    o=bpy.data.objects.new(name,me);bpy.context.collection.objects.link(o);o.data.materials.append(mat)
    if bevel:
        mod=o.modifiers.new('Edge highlights','BEVEL');mod.width=bevel;mod.segments=2
        o.modifiers.new('Weighted normals','WEIGHTED_NORMAL')
    parts.append(o);return o

def leaf(name,outline,side):
    n=len(outline);cen=sum((Vector(p) for p in outline),Vector())/n
    front=cen+Vector((0,side*.048,0));back=cen-Vector((0,side*.016,0))
    vertices=outline+[tuple(front),tuple(back)];polygons=[]
    for i in range(n):
        k=(i+1)%n;polygons.extend([(i,k,n),(k,i,n+1)])
    me=bpy.data.meshes.new(name);me.from_pydata(vertices,[],polygons);me.update()
    ob=bpy.data.objects.new(name,me);bpy.context.collection.objects.link(ob);ob.data.materials.append(navy);parts.append(ob)

fin('Crescent_Tail',[(1.33,0,.95),(1.41,0,1.13),(1.59,0,1.49),(1.80,0,1.63),
 (1.87,0,1.65),(1.82,0,1.53),(1.65,0,1.18),(1.59,0,.96),
 (1.66,0,.73),(1.83,0,.46),(1.86,0,.40),(1.77,0,.40),
 (1.57,0,.55),(1.42,0,.77)],.075,navy,.024)
fin('Dorsal_Fin',[(-.42,0,1.43),(-.20,0,1.69),(.05,0,1.86),(.13,0,1.88),
 (.08,0,1.72),(.055,0,1.55),(.16,0,1.44),(.30,0,1.37)],.072,navy,.018)
fin('Second_Dorsal',[(.35,0,1.36),(.52,0,1.51),(.58,0,1.53),(.57,0,1.30)],.055,navy)
for side in [-1,1]:
    x,z=-1.03,.80;y=surface_y(x,z)
    sphere('Eye_Rim_'+str(side),(x,side*(y+.006),z),(.14,.025,.145),rim)
    sphere('Eye_'+str(side),(x-.005,side*(y+.026),z+.006),(.121,.043,.125),eye)
    sphere('Glint_'+str(side),(x-.034,side*(y+.063),z+.06),(.028,.012,.028),glint,16,10)
    leaf('Pectoral_'+str(side),[(-.45,side*.505,.79),(-.26,side*.55,.92),
        (.04,side*.555,1.035),(.24,side*.54,1.06),(.30,side*.52,1.055),
        (.18,side*.54,.94),(-.10,side*.56,.73),(-.25,side*.55,.65)],side)
    leaf('Pelvic_'+str(side),[(-.50,side*.16,.22),(-.24,side*.26,.10),
         (-.02,side*.32,.075),(.035,side*.33,.09),(-.23,side*.20,.27)],side)
for i in range(5):
    x=.64+i*.13;cz,rz,_=section(x)
    fin('Upper_Finlet_'+str(i),[(x-.045,0,cz+rz-.025),(x+.017,0,cz+rz+.09),
        (x+.055,0,cz+rz-.03)],.03,gold,.009)
for i in range(4):
    x=.73+i*.14;cz,rz,_=section(x)
    fin('Lower_Finlet_'+str(i),[(x-.04,0,cz-rz+.02),(x+.035,0,cz-rz-.07),
        (x+.05,0,cz-rz+.03)],.028,gold,.008)

def line(name,points,radius,mat):
    cu=bpy.data.curves.new(name,'CURVE');cu.dimensions='3D';cu.resolution_u=8
    cu.bevel_depth=radius;cu.bevel_resolution=2
    sp=cu.splines.new('BEZIER');sp.bezier_points.add(len(points)-1)
    for p,co in zip(sp.bezier_points,points):p.co=co;p.handle_left_type='AUTO';p.handle_right_type='AUTO'
    ob=bpy.data.objects.new(name,cu);bpy.context.collection.objects.link(ob);ob.data.materials.append(mat);parts.append(ob)
for side in [-1,1]:
    points=[]
    for k in range(13):
        t=k/12;x=-.68+.12*math.sin(t*math.pi);z=1.22-1.03*t
        points.append((x,side*(surface_y(x,z)+.001),z))
    line('Gill_'+str(side),points,.006,seam)
    points=[]
    for x,z in [(-1.498,.65),(-1.43,.605),(-1.33,.57),(-1.24,.56),(-1.21,.58)]:
        points.append((x,side*(surface_y(x,z)+.004),z))
    line('Mouth_'+str(side),points,.012,seam)

floor=material('Studio',(.92,.88,.78),.9)
bsdf=floor.node_tree.nodes.get('Principled BSDF');bsdf.inputs['Emission Color'].default_value=(.92,.88,.78,1);bsdf.inputs['Emission Strength'].default_value=.25
bpy.ops.mesh.primitive_plane_add(size=200,location=(0,0,.06));bpy.context.object.data.materials.append(floor)
def light(name,loc,power,size):
    bpy.ops.object.light_add(type='AREA',location=loc);ob=bpy.context.object;ob.name=name;ob.data.energy=power;ob.data.shape='DISK';ob.data.size=size
    ob.rotation_euler=(Vector((0,0,.7))-ob.location).to_track_quat('-Z','Y').to_euler()
light('Key',(-3,-4,6),650,4)
light('Fill',(0,-2,3),110,5)
light('Rim',(2,3,5),400,4)
bpy.ops.object.camera_add(location=(-2.0,-7.5,2.75));cam=bpy.context.object
cam.rotation_euler=(Vector((.10,0,.96))-cam.location).to_track_quat('-Z','Y').to_euler()
cam.data.type='ORTHO';cam.data.ortho_scale=3.9
scene=bpy.context.scene;scene.camera=cam;scene.render.engine='CYCLES';scene.cycles.samples=48;scene.cycles.use_denoising=True
scene.world.use_nodes=True;bg=scene.world.node_tree.nodes['Background'];bg.inputs[0].default_value=(.82,.86,.95,1);bg.inputs[1].default_value=.65
scene.view_settings.view_transform='AgX';scene.view_settings.look='AgX - Medium High Contrast'
scene.render.resolution_x=1254;scene.render.resolution_y=1254;scene.render.resolution_percentage=100
scene.render.filepath=str(STUDIO/'Tuna_Reference_Preview.png')
bpy.ops.object.select_all(action='DESELECT')
for o in parts:o.select_set(True)
bpy.context.view_layer.objects.active=body
bpy.ops.wm.save_as_mainfile(filepath=str(STUDIO/'Tuna_Reference.blend'))
bpy.ops.render.render(write_still=True)
print('REFERENCE_RENDER_COMPLETE')
