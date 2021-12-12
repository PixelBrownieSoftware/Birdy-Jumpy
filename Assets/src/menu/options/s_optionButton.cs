using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_optionButton : s_button
{
    public override void OnButtonClicked()
    {
        switch (buttonType)
        {
            case "controls":
                break;

            case "lev":
                break;

            default:
                base.OnButtonClicked();
                break;
        }
    }
}
