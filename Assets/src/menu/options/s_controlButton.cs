using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_controlButton : s_button
{
    public string keyName;
    public KeyCode code;

    public override void OnStart()
    {
        base.OnStart();
    }

    private void Update()
    {
        code = s_globals.GetKeyPref(keyName);
        txt.text = "" + keyName + "-" + code.ToString();
    }

    public override void OnButtonClicked()
    {
        s_menuhandler.GetInstance().GetMenu<s_optionsKeyMenu>("KeyControl").buttonCtrl = this;
    }
}
