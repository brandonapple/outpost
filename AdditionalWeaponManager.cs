using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalWeaponManager : MonoBehaviour
{
    public static AdditionalWeaponManager instance;

    public StunedMissileLuncher stunedMissileLuncher;
    public EnergyShieldController energyShieldController;
    public MushRoomSpawnerController mushRoomSpawnerController;
    public ShurikenSpawnerController shurikenSpawnerController;
    public TopLaserController topLaserController;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CheckWeapons();
    }
    public void CheckWeapons()
    {
        stunedMissileLuncher.gameObject.SetActive(RelicManager.instance.stunedMissileLuncherCount > 0);
        energyShieldController.gameObject.SetActive(RelicManager.instance.energyShieldCount > 0);
        mushRoomSpawnerController.gameObject.SetActive(RelicManager.instance.mushroomBombCount > 0);
        shurikenSpawnerController.gameObject.SetActive(RelicManager.instance.shurikenCount > 0);
        topLaserController.gameObject.SetActive(RelicManager.instance.topLaserCount > 0);
    }
}
