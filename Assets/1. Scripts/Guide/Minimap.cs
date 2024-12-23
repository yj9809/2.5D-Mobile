using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    [SerializeField] private bool isMiniMapOn = false;
    [SerializeField] private Button miniMapOnButton;
    [SerializeField] private Button miniMapOffButton;
    [SerializeField] private GameObject miniMapUI;
    [SerializeField] private Sprite[] mapBackgroundSprites;
    [SerializeField] private Image mapBackgroundImage;

    private readonly Vector2 mapSize = new Vector2(1000f, 1000f);
    private readonly Vector2 worldSize = new Vector2(39f, 46f);

    [SerializeField] private Transform player;
    [SerializeField] private RectTransform playerIcon;

    [SerializeField, ReadOnly] private int currentMapIndex = 0;

    private void Start()
    {
        miniMapUI.SetActive(isMiniMapOn);

        if (miniMapOnButton != null)
            miniMapOnButton.onClick.AddListener(ActivateMiniMap);
        if (miniMapOffButton != null)
            miniMapOffButton.onClick.AddListener(DeactivateMiniMap);

        player = GameManager.Instance.P.transform;

        UpdateMapBackground(0);
    }

    private void Update()
    {
        if (isMiniMapOn)
        {
            UpdatePlayerPosition();
        }
    }

    public void ActivateMiniMap()
    {
        isMiniMapOn = true;
        miniMapUI.SetActive(true);
    }

    public void DeactivateMiniMap()
    {
        isMiniMapOn = false;
        miniMapUI.SetActive(false);
    }

    private void UpdatePlayerPosition()
    {
        if (player != null && playerIcon != null)
        {
            Vector3 worldPosition = player.position;

            float normalizedX = (worldPosition.x / worldSize.x);
            float normalizedZ = (worldPosition.z / worldSize.y);

            float minimapX = normalizedX * mapSize.x;
            float minimapY = normalizedZ * mapSize.y;

            playerIcon.anchoredPosition = new Vector2(minimapX, minimapY);
        }
        else
        {
            Debug.LogWarning("플레이어 스프라이트나 플레이어 아이콘이 설정되지 않았습니다.");
        }
    }

    public void UpdateMapBackground(int index)
    {
        if (index < 0 || index >= mapBackgroundSprites.Length)
        {
            Debug.LogWarning("잘못된 배경 인덱스입니다.");
            return;
        }

        currentMapIndex = index;

        if (mapBackgroundImage != null)
        {
            mapBackgroundImage.sprite = mapBackgroundSprites[index];
        }
        else
        {
            Debug.LogWarning("미니맵 배경 이미지가 설정되지 않았습니다.");
        }
    }
}
