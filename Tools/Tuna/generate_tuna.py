"""Generate an editable, static tuna OBJ with UVs, material groups and mesh preview.
Requires Python, numpy and Pillow. Coordinates: Y up, head -X, meters.
"""
from pathlib import Path
import math
import uuid
import numpy as np
from PIL import Image, ImageDraw

ROOT = Path(__file__).resolve().parents[2]
OUT = ROOT / 'Assets/3. Prefab/Churu/Tuna'
OUT.mkdir(parents=True, exist_ok=True)
vertices, uvs, faces = [], [], []
colors = {
    'Navy': (49,  70, 115),
    'Blue': (100, 137, 175), 'Silver': (178, 199, 211),
    'Cream': (231, 228, 209), 'Fin': (49, 68, 104),
    'Gold': (202, 170, 83), 'Eye': (22, 24, 30),
    'EyeRim': (115, 122, 128), 'Highlight': (255, 251, 235),
}

def ellipsoid(center, scale, material, segments=20, rings=12, body=False):
    start = len(vertices)
    for j in range(rings + 1):
        a = math.pi * j / rings
        for i in range(segments + 1):
            b = 2 * math.pi * i / segments
            x = math.cos(a)
            # Longitudinal axis X; taper the tail end more than the face.
            taper = 1 - .26 * max(x, 0) if body else 1
            vertices.append((center[0] + scale[0] * x,
                center[1] + scale[1] * math.sin(a) * math.cos(b) * taper,
                center[2] + scale[2] * math.sin(a) * math.sin(b) * taper))
            uvs.append((i / segments, j / rings))
    for j in range(rings):
        for i in range(segments):
            q = start + j * (segments + 1) + i
            mat = material
            if body:
                h = math.cos(2 * math.pi * (i + .5) / segments)
                mat = 'Navy' if h > .48 else 'Blue' if h > .05 else 'Silver' if h > -.48 else 'Cream'
            if j != 0:
                faces.append(((q, q + segments + 1, q + 1), mat))
            if j != rings - 1:
                faces.append(((q + 1, q + segments + 1, q + segments + 2), mat))

def fin(points, thickness, material='Fin'):
    # Closed beveled-looking prism, useful for short fins and crescent tail.
    n, start = len(points), len(vertices)
    for side in [-1, 1]:
        for x, y, z in points:
            vertices.append((x, y, z + side * thickness / 2))
            uvs.append((x + .5, y))
    # Fan works for triangular fins; crescent uses a center-line polygon without holes.
    for i in range(1, n - 1):
        faces.append(((start, start + i + 1, start + i), material))
        faces.append(((start + n, start + n + i, start + n + i + 1), material))
    for i in range(n):
        k = (i + 1) % n
        faces.extend([((start+i, start+k, start+n+k), material),
                      ((start+i, start+n+k, start+n+i), material)])

ellipsoid((-.035, .235, 0), (.30, .19, .135), 'Blue', 24, 16, True)
ellipsoid((.244, .235, 0), (.085, .048, .048), 'Blue', 12, 8)
fin([(.282,.235,0),(.405,.405,0),(.374,.295,0)], .025)
fin([(.282,.235,0),(.374,.295,0),(.369,.235,0)], .025)
fin([(.282,.235,0),(.369,.235,0),(.374,.175,0)], .025)
fin([(.282,.235,0),(.374,.175,0),(.405,.065,0)], .025)
fin([(-.07,.40,0),(.055,.525,0),(.075,.395,0)], .025)
fin([(.11,.37,0),(.165,.425,0),(.184,.34,0)], .017)
for side in [-1,1]:
    # Eyes face both sides, with small physical highlight for mobile readability.
    ellipsoid((-.233,.273,side*.086),(.043,.045,.019),'EyeRim',16,10)
    ellipsoid((-.235,.275,side*.101),(.035,.036,.018),'Eye',16,10)
    ellipsoid((-.246,.291,side*.115),(.010,.011,.005),'Highlight',10,6)
    fin([(-.105,.238,side*.12),(.077,.185,side*.147),(-.025,.274,side*.127)],.014)
    fin([(-.02,.072,side*.05),(.067,.027,side*.073),(.048,.096,side*.06)],.012)
for i in range(3):
    x=.18+i*.032
    y=.235+.09-i*.018
    fin([(x-.016,y,0),(x+.013,y+.037,0),(x+.023,y-.012,0)],.01,'Gold')
# Tiny closed mouth on the blunt nose, rather than a realistic open jaw.
ellipsoid((-.329,.204,0),(.007,.006,.042),'Eye',12,6)

# Normalize to a 0.74-meter length, bottom at Y=0, matching the salmon footprint.
v=np.array(vertices,dtype=float)
factor=.74/(v[:,0].max()-v[:,0].min())
v*=factor
v[:,0]-=(v[:,0].max()+v[:,0].min())/2
v[:,1]-=v[:,1].min()
vertices=v.tolist()
obj=['# Churub cute tuna - meters, Y up, static model','mtllib Tuna.mtl','o Tuna']
obj += ['v %.7f %.7f %.7f'%tuple(p) for p in vertices]
obj += ['vt %.6f %.6f'%p for p in uvs]
last=None
for ids,mat in faces:
    if mat!=last:
        obj += ['usemtl '+mat,'s off']
        last=mat
    obj.append('f '+' '.join(f'{i+1}/{i+1}' for i in ids))
(OUT/'Tuna.obj').write_text('\n'.join(obj)+'\n',encoding='utf-8')
mtl=[]
for name,c in colors.items():
    mtl += ['newmtl '+name,'Kd '+' '.join(f'{x/255:.6f}' for x in c),
            'Ka 0.1 0.1 0.1','Ks 0.08 0.08 0.08','Ns 20','d 1','illum 2','']
(OUT/'Tuna.mtl').write_text('\n'.join(mtl),encoding='utf-8')

# Software-render the actual exported triangles, not an AI concept image.
W,H=1200,850
img=Image.new('RGB',(W,H),(245,241,230))
draw=ImageDraw.Draw(img)
draw.ellipse((240,610,970,715),fill=(221,216,203))
view=np.array([-.35,.25,-1.0]);view/=np.linalg.norm(view)
right=np.cross(view,[0,1,0]);right/=np.linalg.norm(right)
up=np.cross(right,view)
center=(v.min(axis=0)+v.max(axis=0))/2
p=v-center
xy=np.column_stack((p@right,p@up))
scale=min(1020/np.ptp(xy[:,0]),650/np.ptp(xy[:,1]))
xy=xy*np.array([scale,-scale])+[W/2,H/2]
light=np.array([-.4,.8,-.6]);light/=np.linalg.norm(light)
render=[]
for ids,mat in faces:
    a,b,c=v[list(ids)]
    n=np.cross(b-a,c-a); length=np.linalg.norm(n)
    if length<1e-10: continue
    n/=length
    # Two-sided display makes the preview robust to imported normal conventions.
    shade=.72+.28*abs(float(n@light))
    col=tuple(int(x*shade) for x in colors[mat])
    render.append((float(np.mean(p[list(ids)]@view)),ids,col))
for _,ids,col in sorted(render,key=lambda x:x[0]):
    draw.polygon([tuple(xy[i]) for i in ids],fill=col)
preview=ROOT/'docs/previews/Tuna_Model.png'
preview.parent.mkdir(parents=True,exist_ok=True)
img.save(preview)
print(f'{len(vertices)} vertices, {len(faces)} triangles; bounds: {np.ptp(v,axis=0)}')
print(preview)
