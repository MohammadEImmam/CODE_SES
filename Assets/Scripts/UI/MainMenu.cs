using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public enum ButtonCode
    {
        Start,
        Options,
        Quit,
        Return,
    }
    public enum PanelCode
    {
        BaseMenu,
        Options,
    }

    [System.Serializable]
    public struct ButtonRef
    {
        public ButtonCode code;
        public Button button;
    }
    public List<Pair<ButtonCode, Button>> buttons = new List<Pair<ButtonCode, Button>>();
    public List<Pair<PanelCode, GameObject>> panels = new List<Pair<PanelCode, GameObject>>();
    public Slider audioVolumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var x in buttons)
        {
            x.value.onClick.AddListener(() => HandleButtonClick(x.key));
        }

        audioVolumeSlider.onValueChanged.AddListener(x => AudioListener.volume = x);
    }

    void HandleButtonClick(ButtonCode buttonCode)
    {
        switch (buttonCode)
        {
            case ButtonCode.Start:
                SceneManager.LoadScene(1);
                break;
            case ButtonCode.Options:
                SetPanelEnabled(PanelCode.Options);
                break;
            case ButtonCode.Quit:
#if UNITY_EDITOR
                // Application.Quit() does not work in the editor so
                // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
                UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
                break;
            case ButtonCode.Return:
                SetPanelEnabled(PanelCode.BaseMenu);
                break;


        }
    }

    void SetPanelEnabled(PanelCode p)
    {
        foreach (var x in panels)
        {
            x.value.SetActive(x.key == p);
        }
    }
}
