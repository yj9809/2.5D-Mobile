using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    [SerializeField] private Button optionButton;
    [SerializeField] private GameObject blurPanel;

    // Start is called before the first frame update
    void Start()
    {
        blurPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowOption()
    {
        blurPanel.SetActive(true);
    }

    public void CloseOption()
    {
        blurPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
