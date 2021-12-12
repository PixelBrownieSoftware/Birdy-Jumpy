using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
[RequireComponent(typeof(Button))]
public class s_button : MonoBehaviour
{
    public string buttonType;
    public string sceneStr;

    s_menuhandler canvasManager;
    Button menuButton;
    protected EventTrigger eventTrigger;
    public Text txt;
    public bool isPause = true;

    private void Start()
    {
        menuButton = GetComponent<Button>();
        menuButton.onClick.AddListener(OnButtonClicked);
        canvasManager = s_menuhandler.GetInstance();
    }
    protected virtual void OnHover()
    {

    }

    public virtual void OnStart()
    {

    }


    public virtual void OnButtonClicked()
    {
        switch (buttonType)
        {
            case "play":
                canvasManager.SwitchMenu("none");
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("map1");
                s_globals.GetInstance().SpawnPlayer();
                break;

            case "lev":
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneStr);
                canvasManager.SwitchMenu("none");
                s_globals.GetInstance().SpawnPlayer();
                break;

            default:
                canvasManager.SwitchMenu(buttonType);
                break;
        }
    }

}
