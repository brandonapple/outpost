using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckLuncher : WeaponControllerBase
{
    public float movingSpeed;

    public List<TruckBullet> truckBulletList;

    private void Awake()
    {
        truckBulletList = new List<TruckBullet>();
    }
    public override void Start()
    {
        base.Start();
     //   LoadData();
    }
    public override void LoadData()
    {
        movingSpeed = WeaponDataManager.instance.GetCurrentValueByString("truckSpeed");
        bulletCount = (int)WeaponDataManager.instance.GetCurrentValueByString("truckCount");
        damage = WeaponDataManager.instance.GetCurrentValueByString("truckDamage");
        radius = WeaponDataManager.instance.GetCurrentValueByString("truckRadius");

        movingSpeed *= RelicManager.instance.hourGlassCDSpeedMultiplier;

        UpdateTruckData();
        FixedData();
    }

    public void UpdateTruckData()
    {
        if (bulletCount>truckBulletList.Count)
        {
            int additionalCount = bulletCount - truckBulletList.Count;
            for (int i = 0; i < additionalCount; i++)
            {
                TruckBullet truck = Instantiate(Resources.Load<TruckBullet>("Bullets/truck"));
                truckBulletList.Add(truck);
                truck.transform.parent = transform;
            }
            SetTruckPos();
            ResetTruckSpeed();
        }

        foreach (TruckBullet truckBullet in truckBulletList)
        {
            truckBullet.damage = damage;
            truckBullet.speed = movingSpeed;
            truckBullet.damageRadius = radius;
        }
       
    }


    public void SetTruckPos()
    {
        for (int i = 0; i < truckBulletList.Count; i++)
        {
            float angle =((float)i / truckBulletList.Count) *( Mathf.PI*2);
            truckBulletList[i].transform.position = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * 3;

        }
    }
    public void ResetTruckSpeed()
    {
        for (int i = 0; i < truckBulletList.Count; i++)
        {
            truckBulletList[i].ResetSpeed();
        }
    }

    public void EmptyTruckList()
    {
        foreach (TruckBullet truck in truckBulletList)
        {
            if (truck.gameObject)
            {
                Destroy(truck.gameObject);
            }
        }
        truckBulletList = new List<TruckBullet>();
    }
}
