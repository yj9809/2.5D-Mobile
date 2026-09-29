# 참치 모델 초안

- 모델: `Assets/3. Prefab/Churu/Tuna/Tuna.obj`
- 재질: 같은 폴더의 `Tuna.mtl` (OBJ와 함께 보관)
- 실제 메시 미리보기: `docs/previews/Tuna_Model.png`
- 재생성: `python Tools/Tuna/generate_tuna.py` (numpy, Pillow 필요)

Y축이 위쪽이며 길이 0.74m, 바닥 기준 피벗으로 제작했다. 정적 모델이며
리깅과 애니메이션은 없다. UV와 재질 그룹을 포함하며 단색 재질을 사용한다.
시안의 형태를 단순화한 초안으로, Blender에서 OBJ를 불러와 추가 편집할 수 있다.

Unity 임포트 후 `Tools > Churub > Create Tuna Prefab`을 실행하면
Item(Ingredient), Rigidbody, BoxCollider를 포함한 별도 Tuna.prefab을 만든다.
이미 Prefab이 있으면 덮어쓰지 않고 선택한다. 기존 연어와 생산 흐름은 변경하지 않는다.

Unity에서 OBJ 방향, MTL 색상, Collider와 적재 간격을 확인해야 한다.
이 모델은 기존 연어 조각보다 높으므로 같은 수량을 쌓으면 높이가 더 높아진다.
실제 생산 투입 및 풀 등록은 모델 검토 후 별도로 진행한다.
