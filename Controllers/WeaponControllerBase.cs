using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControllerBase : MonoBehaviour
{
    public float timer;
    public float interval;

    [Space(10)]
    public float damage;
    public float attackSpeed;
    public float radius;
    public float duration;
    public float triggerAgainChance;
    public int bulletCount;
    public int catapultTime;
    public Vector3 faceDir;
    public Vector3 targetDir;
    public float rotateSpeed;

    [Space(20)]
    public GameObject firePoint;
    public Monster targetMonster;


    public BuffVisualization thisBuffVisualization;
    public GameObject buffVisualizationPoint;
    public WeaponFaceDirVisualization weaponFaceDirVisualization;

    public virtual void Start()
    {
        attackSpeed = 1;
        interval = 1;
        LoadData();
        SpawnWeaponBuff();
    }
    public virtual void LoadData()
    {

    }

    public virtual void FixedData()
    {
        if (!RelicManager.instance) return;


        radius *= RelicManager.instance.rangeAmplifierMutiplier;
        attackSpeed *= RelicManager.instance.hourGlassCDSpeedMultiplier;
        duration *= RelicManager.instance.gasolineBurnTimeMultiplier;
    }

    public virtual void FixedBulletDamage()
    {
        damage *= RelicManager.instance.goldenBulletDamageMultiplier;
    }
    public virtual void FixedElectricDamage()
    {
        damage *= RelicManager.instance.electricDamageMultiplier;
    }

    public virtual void Update()
    {
       
    }

    public virtual void FixedUpdate()
    {
        AttackCloestMonster();
    }


    public virtual void SpawnWeaponBuff()
    {
        thisBuffVisualization = Instantiate(Resources.Load<BuffVisualization>("Prefab/buffVisualization"), transform);
        if (buffVisualizationPoint)
        {
         thisBuffVisualization.transform.position = buffVisualizationPoint.transform.position;

        }
    }
    
    public virtual void Fire()
    {

    } 

    public virtual void AttackCloestMonster()
    {
        targetMonster = MonsterManager.cloestMonster(transform.position);
        if (targetMonster == null) return;

        timer += Time.deltaTime * attackSpeed;
        if (timer > interval)
        {
            timer = 0;
            Fire();
        }
    }

    public virtual void RenderFaceDir(Vector3 faceDir)
    {
        if (weaponFaceDirVisualization==null)
        {
            weaponFaceDirVisualization = Instantiate(Resources.Load<WeaponFaceDirVisualization>("Prefab/weaponFaceDirRoot"));
            weaponFaceDirVisualization.transform.parent = transform;
            weaponFaceDirVisualization.transform.localPosition = Vector3.zero;
        }
        weaponFaceDirVisualization.RenderDir(faceDir);
    }

    public void RotateToCloestMonster()
    {
        if (!targetMonster) return;

        targetDir = targetMonster.transform.position - transform.position;
        targetDir = targetDir.normalized;
        targetDir = (targetDir + Vector3.down * .25f).normalized;

        if (Vector3.Angle(targetDir, faceDir)>10)
        {
            Vector3 norDir = Vector3.Cross(faceDir, Vector3.up);
            if (Vector3.Dot(norDir, targetDir) > 0)
            {
                faceDir += norDir * .005f * rotateSpeed;
            }
            else
            {
                faceDir += norDir * -.005f * rotateSpeed;
            }


            faceDir = faceDir.normalized;
        }
    }
}
