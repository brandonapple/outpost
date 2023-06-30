using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpgradeContainerWeaponSwitch : MonoBehaviour
{
    public Image trigger;
    public bool weaponOn;
    public UpgradeContainer currentUpgradeContainer;

    public Color weaponOnColor;
    public Color weaponOffColor;
    public void ChangeState()
    {
        if (!weaponOn)
        {
            if (UpgradeRootPanel.instance.GetActiveWeaponsCount()>=5)
            {
                TipPanel.instance.ShowTipSingle("cant avtive weapons more than 5");
                return;
            }
        }
        weaponOn = !weaponOn;
        ShowState();
        currentUpgradeContainer.SwitchWeapon(weaponOn);
    }
    public void ShowState()
    {
        if (weaponOn)
        {
            trigger.color = weaponOnColor;
            trigger.transform.localPosition = new Vector3(20, 0, 0);
        }
        else
        {
            trigger.color = weaponOffColor;
            trigger.transform.localPosition = new Vector3(-20, 0, 0);
        }
    }
}
