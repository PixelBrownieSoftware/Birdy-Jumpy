using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_optionsKeyMenu : s_menucontroler
{
    public s_controlButton buttonCtrl;
    
    private void OnGUI()
    {
        if (buttonCtrl != null) {

            Event e = Event.current;
            if (e.isKey || e.shift)
            {
                if (e.shift)
                {
                    s_globals.SetButtonKeyPref(buttonCtrl.keyName, KeyCode.LeftShift);
                    buttonCtrl = null;
                }
                else if (e.isKey)
                {
                    s_globals.SetButtonKeyPref(buttonCtrl.keyName, e.keyCode);
                    buttonCtrl = null;
                }
            }
        }
    }
}
