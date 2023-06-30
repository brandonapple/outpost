using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningFenceController : WeaponControllerBase
{
    public static LightningFenceController instance;
    public LineRenderer lightningLine;

    public float rangeOutSide;
    public float rangeInSide;
    public float lightningLength;
   
    private void Awake()
    {
        instance = this;
    }
    public override void Start()
    {
        base.Start();
        UpdateLightningLine();
    }

    private void OnEnable()
    {
        UpdateLightningLine();
    }
    public void UpdateLightningLine()
    {
        lightningLine.positionCount = 40;
        float radius = WeaponDataManager.instance.gunRangeValueCurrent - lightningLength;
        for (int i = 0; i < 40; i++)
        {
            Vector3 normalPos = new Vector3(Mathf.Cos((float)i / 39 * Mathf.PI * 2), 0, Mathf.Sin((float)i / 39 * Mathf.PI * 2));
            lightningLine.SetPosition(i,normalPos * radius - transform.position );
        }
    }

    public override void LoadData()
    {
        attackSpeed = DataManager.instance.getCurrentValueByString("lightningSpeed");
        damage = DataManager.instance.getCurrentValueByString("lightningDamage");
        rangeOutSide = DataManager.instance.getCurrentValueByString("gunRange");
        lightningLength = DataManager.instance.getCurrentValueByString("lightningLength");
        rangeInSide = rangeOutSide - lightningLength;

        FixedData();
        FixedElectricDamage();
    }
    public override void Fire()
    {
        LightningFenceBullet lightningFenceBullet;//= Instantiate(Resources.Load<LightningFenceBullet>("Bullets/lightningFenceBullet"));
        lightningFenceBullet = GameObjectPoolManager.instance.lightningBulletPool.Get(transform.position, 1).GetComponent<LightningFenceBullet>();
        lightningFenceBullet.transform.position = transform.position;
        lightningFenceBullet.damage = damage;
        lightningFenceBullet.rangeInside = rangeInSide;
        lightningFenceBullet.rangeOutSide = rangeOutSide;

        
    }
  
 
}
