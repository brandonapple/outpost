using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushRoomSpawnerController : WeaponBase
{
    public float timer;
    public override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        timer = 2 - .1f;
    }
    private void FixedUpdate()
    {
        if (RelicManager.instance == null) return;
        if (RelicManager.instance.mushroomBombCount == 0) return;

        weaponCDIcon.SetCDCircle(timer / 2);
        if (timer<2)
        {
            float speed = Time.deltaTime;
            speed *= RelicManager.instance.mushroomSpawnerSpeed;
            speed *= RelicManager.instance.hourGlassCDSpeedMultiplier;
            timer += speed;
        }
        if (timer>2)
        {
            timer = 0;
            SpawnMushRoomBomb();
        }
    }

    void SpawnMushRoomBomb()
    {
        MushRoomBomb mushRoomBomb = Instantiate(Resources.Load<MushRoomBomb>("Bullets/mushRoomBomb"));
        Vector3 pos = new Vector3(Random.value - .5f, 0, Random.value - .5f).normalized;
        pos *= Random.Range(.8f, 1.05f) * WeaponDataManager.instance.gunRangeValueCurrent;
        mushRoomBomb.transform.position = pos;
    }
}

