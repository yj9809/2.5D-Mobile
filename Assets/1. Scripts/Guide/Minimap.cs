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
    [SerializeField] private GameObject miniMapPanel;
    [SerializeField] private Image playerIcon;
    [SerializeField] private Transform player;
    
    private void Start()
    {
        miniMapPanel.SetActive(isMiniMapOn);

        if (miniMapOnButton != null)
            miniMapOnButton.onClick.AddListener(ActivateMiniMap);
        if (miniMapOffButton != null)
            miniMapOffButton.onClick.AddListener(DeactivateMiniMap);

        player = GameManager.Instance.P.transform;

        GameObject iconObject = GameObject.Find("playerIcon");
        if (iconObject != null)
            playerIcon = iconObject.GetComponent<Image>();
    }

    private void Update()
    {
        if (player != null && playerIcon != null)
        {
            Vector3 playerPosition = player.position;
            RectTransform iconRectTransform = playerIcon.rectTransform;
            iconRectTransform.position = new Vector3(playerPosition.x, playerPosition.y + 5, playerPosition.z);
        }
    }

    public void ActivateMiniMap()
    {
        isMiniMapOn = true;
        miniMapPanel.SetActive(true);
    }

    public void DeactivateMiniMap()
    {
        isMiniMapOn = false;
        miniMapPanel.SetActive(false);
    }
}
