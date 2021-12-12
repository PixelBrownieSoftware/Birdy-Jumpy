using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class s_menuhandler : s_singleton<s_menuhandler>
{
    public List<s_menucontroler> menuControllers;
    s_menucontroler lastActiveMenu;

    private void Awake()
    {
        menuControllers = GetComponentsInChildren<s_menucontroler>().ToList();
        menuControllers.ForEach(x => x.gameObject.SetActive(false));
        DontDestroyOnLoad(this);
        SwitchMenu("MainMenu");
    }

    public T GetMenu<T>(string _menu) where T : s_menucontroler
    {
        T menuToSwitchTo = menuControllers.Find(x => x.menuType == _menu).GetComponent<T>();
        return menuToSwitchTo;
    }

    public void SwitchMenu(string _menu) {
        if (lastActiveMenu != null) {
            lastActiveMenu.gameObject.SetActive(false);
        }

        s_menucontroler menuToSwitchTo = menuControllers.Find(x=> x.menuType == _menu);
        if (menuToSwitchTo != null)
        {
            menuToSwitchTo.gameObject.SetActive(true);
            lastActiveMenu = menuToSwitchTo;
        }
    }
}
