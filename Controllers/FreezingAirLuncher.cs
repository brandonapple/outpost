using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezingAirLuncher : WeaponControllerBase
{
    public static FreezingAirLuncher instance;

    public GameObject freezingAirEffectRoot;
    public bool weaponOn;
    public AudioSource airAudio;
    public float freezingSpeed;
    public float freezingAirFreezonDuration;
   

    public SpriteRenderer weaponSpriteRenderer;
    private void Awake()
    {
        instance = this;
        SwitchOffFreezingAir();
        faceDir = Vector3.forward;
    }
    public override void Start()
    {
        base.Start();
    }
    public override void LoadData()
    {
        freezingSpeed = WeaponDataManager.instance.GetCurrentValueByString("freezingAirFreezingSpeed");
        freezingAirFreezonDuration = WeaponDataManager.instance.GetCurrentValueByString("freezingAirFreezonDuration");
        rotateSpeed = WeaponDataManager.instance.GetCurrentValueByString("freezingAirRotateSpeed");
        damage = WeaponDataManager.instance.GetCurrentValueByString("freezingAirDamage");

        FixedData();

        SwitchOnFreezingAir();
    }
    public void SwitchOnFreezingAir()
    {
        ParticleSystem[] pars = freezingAirEffectRoot.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem par in pars)
        {
            par.Play();
        }
        weaponOn = true;
        airAudio.Play();
       
    }
    public void SwitchOffFreezingAir()
    {
        ParticleSystem[] pars = freezingAirEffectRoot.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem par in pars)
        {
            par.Stop();
        }
        weaponOn = false;
        airAudio.Stop();
       
    }
    public override void Update()
    {
       
    }
    public override void FixedUpdate()
    {
        RenderFaceDir(faceDir);

        targetMonster = MonsterManager.cloestMonster(Base.instance.transform.position );
        if (targetMonster != null && !weaponOn)
        {
            SwitchOnFreezingAir();
        }

        if (weaponOn)
        {
            if (targetMonster == null)
            {
                SwitchOffFreezingAir();
                return;
            }

            if (faceDir.x>0)
            {
                weaponSpriteRenderer.transform.rotation = Quaternion.Euler(0, 0, 0);
                freezingAirEffectRoot.transform.localPosition = new Vector3(0.35f, 0.4f, 0);
            }
            else
            {
                weaponSpriteRenderer.transform.rotation = Quaternion.Euler(0, 180, 0);
                freezingAirEffectRoot.transform.localPosition = new Vector3(-0.35f, 0.4f, 0);
            }

            RotateToCloestMonster();
            freezingAirEffectRoot.transform.rotation = Quaternion.LookRotation(faceDir);

            

            timer += Time.deltaTime;
            if (timer>.25f)
            {
                timer = 0;
                Monster[] monsters = MonsterManager.instance.monsters;
                foreach (Monster monster in monsters)
                {
                    if (Vector3.Distance(transform.position,monster.transform.position)<5&&
                        Vector3.Angle(monster.transform.position - transform.position,faceDir)<20)
                    {
                        monster.Hitted(damage);
                        monster.AddFreezeValue(freezingSpeed);
                        WeaponDamageSettlementManager.instance.FreezingAirDealDamage(damage);
                    }
                }
            }

        }
    }
}
