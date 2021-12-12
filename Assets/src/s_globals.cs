using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class o_state {
    public string name;
    public string scene;
}

[System.Serializable]
public class s_keybindings {
    public string action;
    public KeyCode key;
}

[System.Serializable]
public struct ev_integer
{
    public int integer;
    public string integer_name;
}
[System.Serializable]
public struct dat_globalflags
{
    public dat_globalflags(Dictionary<string, int> Flags)
    {
        this.Flags = Flags;
    }
    public Dictionary<string, int> Flags;
}


public class s_globals : s_singleton<s_globals>
{
    public o_character playerPrefab;
    public o_player currentPlayer;

    public List<s_keybindings> keyBindings;
    public Vector3 spawnPoint;
    public string currentScene;

    public List<ev_integer> GlobalFlagCache = new List<ev_integer>();
    public static Dictionary<string, int> GlobalFlags = new Dictionary<string, int>();
    public static Dictionary<string, KeyCode> arrowKeyConfig = new Dictionary<string, KeyCode>();

    public void LoadScene(string str)
    {
        currentScene = str;
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentScene);
    }

    public void ReloadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentScene);
    }

    private void Awake()
    {
        AddKeys();
        DontDestroyOnLoad(this);
    }

    public void SpawnPlayer() {
        currentPlayer = Instantiate<o_player>(playerPrefab.GetComponent<o_player>(), transform.position, Quaternion.identity);
    }

    public void GameOver() { }

    public void SetNewFlags()
    {
        foreach (ev_integer e in GlobalFlagCache)
        {
            if (!GlobalFlags.ContainsKey(e.integer_name))
                GlobalFlags.Add(e.integer_name, e.integer);
        }
    }
    public void SetSavedFlags(dat_globalflags flgs)
    {
        foreach (KeyValuePair<string, int> e in flgs.Flags)
        {
            if (!GlobalFlags.ContainsKey(e.Key))
                GlobalFlags.Add(e.Key, e.Value);
            else
                GlobalFlags[e.Key] = e.Value;
        }
    }
    public static void SetButtonKeyPref(string buttonName, KeyCode keyCode)
    {
        PlayerPrefs.SetInt(buttonName, (int)keyCode);
    }

    public static void SetButtonKey(string buttonName, KeyCode keyCode)
    {
        if (!arrowKeyConfig.ContainsKey(buttonName))
        {
            arrowKeyConfig.Add(buttonName, keyCode);
            return;
        }
        arrowKeyConfig[buttonName] = keyCode;
    }

    public static void SetGlobalFlag(string flagname, int flag)
    {
        if (!GlobalFlags.ContainsKey(flagname))
        {
            GlobalFlags.Add(flagname, flag);
            return;
        }
        GlobalFlags[flagname] = flag;
    }

    public static int GetGlobalFlag(string flagname)
    {
        if (!GlobalFlags.ContainsKey(flagname))
        {
            return int.MinValue;
        }
        return GlobalFlags[flagname];
    }

    public static KeyCode GetKeyPref(string keyName)
    {
        if (PlayerPrefs.HasKey(keyName))
        {
            return (KeyCode)PlayerPrefs.GetInt(keyName);
        }
        return KeyCode.None;
    }

    public virtual void AddKeys()
    {
        SetButtonKeyPref("left", KeyCode.A);
        SetButtonKeyPref("right", KeyCode.D);
        SetButtonKeyPref("down", KeyCode.S);
        SetButtonKeyPref("up", KeyCode.W);
        SetButtonKeyPref("jump", KeyCode.Space);
        SetButtonKeyPref("camera", KeyCode.LeftShift);
    }


    public void LoadFlag(dat_globalflags flag)
    {
        GlobalFlags = flag.Flags;
    }

}
