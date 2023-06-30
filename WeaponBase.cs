using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    public WeaponCDIcon weaponCDIcon;

    public virtual void Awake()
    {
        weaponCDIcon = Instantiate(Resources.Load<WeaponCDIcon>("Prefab/WeaponCDPercentCircleIcon"),transform);
        weaponCDIcon.transform.position = transform.position;
        weaponCDIcon.SetCDCircle(0);
    }
}
