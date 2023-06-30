using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Monster : MonoBehaviour
{
    public float lifeCurrent;
    public float attackTimer;
    [Space(20)]
   
    public int lifeMax = 3;
    public float speed = 1;
    public float scalingSpeed = 10f;
    public float scalingAmplitude = .1f;
    float scalingOffset;
    float monsterHeight;
   
    public Sprite level1Sprite;
    public Sprite level2Sprite;

    [Space(20)]
    public float characterSpeed = 1f;
    public float attackSpeed = 1;
    public float attackDamage = .3f;
    public float attackRange = .75f;
    public float monsterValue = 1;
    public Color bodyColor;
    public Color bloodColor;

    public bool avaliable;
    public bool stuned ;
    public bool poisoned;
    public bool burned;
    public bool paralyzed;
    public bool dead;
    public bool hitted;
    public bool slowed;
    public bool knocked;
    public bool confused;
    public bool timeFreezon;

    [Space(10)]
    public SpriteRenderer monsterSpriteRenderer;
    public LifeSlider lifeSlider;
    public GameObject burnedEffect;
    public GameObject paralyzedEffect;

    [Space(20)]
    public bool energyBallMarked;
    public GameObject energyBallMarkEffect;

    public bool freezed;
    public float freezingTimer;
    public GameObject freezonEffect;


    [Space(10)]
    public GameObject currentTargetUnit;
    

   
    public virtual void Awake()
    {
        GetSprite();
        FixLifeAndSprite();
        SetLifeSlider();
    }
    public virtual void Start()
    {
        scalingOffset = Random.value;
        avaliable = true;
        characterSpeed = 1;
        attackTimer = Random.value * .5f;

        CheckMonsterSpriteRendererRotation();
        GetCloestTargetUnit();
    }
    public virtual void GetSprite()
    {
        monsterSpriteRenderer = GetComponentsInChildren<SpriteRenderer>()[0];
        level1Sprite = monsterSpriteRenderer.sprite;
        monsterHeight = monsterSpriteRenderer.sprite.texture.height * monsterSpriteRenderer.transform.localScale.y * .01f;
        CheckMonsterSpriteRendererRotation();

    }
    public virtual void CheckMonsterSpriteRendererRotation()
    {
        if (transform.position.x >= 0)
        {
            monsterSpriteRenderer.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            monsterSpriteRenderer.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    public virtual void BackToBase()
    {
        if (transform.position.x>=0)
        {
            monsterSpriteRenderer.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            monsterSpriteRenderer.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    public virtual void FixLifeAndSprite()
    {
        MonsterBodyColor monsterBodyColorComponent = GetComponent<MonsterBodyColor>();
        switch (MonsterManager.instance.roundIndex)
        {
            case 1:
                bodyColor = Color.white;
                bloodColor = monsterBodyColorComponent.levelOneColor;
                break;
            case 2:
                lifeMax = 55 + (int)(lifeMax * 8f);
                if (level2Sprite)
                {
                    monsterSpriteRenderer.sprite = level2Sprite;
                    monsterSpriteRenderer.transform.localScale *= level1Sprite.texture.width / level2Sprite.texture.width;
                    monsterSpriteRenderer.transform.localScale *= 1.25f;
                    bodyColor = monsterBodyColorComponent.levelTwoColor;
                    bloodColor = monsterBodyColorComponent.levelTwoColor;
                }
                break;


            case 3:
                lifeMax = 400 + (int)(lifeMax * 100);
                bodyColor = monsterBodyColorComponent.levelTwoColor;
                monsterSpriteRenderer.transform.localScale *= 1.3f;
                bloodColor = monsterBodyColorComponent.levelOneColor;

                break;
            case 4:
                lifeMax = 2000 + (int)(lifeMax * 2000);
                bodyColor = monsterBodyColorComponent.levelTwoColor;
                monsterSpriteRenderer.transform.localScale *= 1.45f;
                bodyColor = monsterBodyColorComponent.levelTwoColor;

                break;
            case 5:
                lifeMax = 40000 + (int)(lifeMax * 40000);
                if (level2Sprite)
                {
                    monsterSpriteRenderer.sprite = level2Sprite;
                    monsterSpriteRenderer.transform.localScale *= level1Sprite.texture.width / level2Sprite.texture.width;
                    monsterSpriteRenderer.transform.localScale *= 1.55f;
                    bodyColor = GetComponent<MonsterBodyColor>().levelTwoColor;
                    bloodColor = monsterBodyColorComponent.levelTwoColor;
                }
                bodyColor = monsterBodyColorComponent.levelTwoColor;
                monsterSpriteRenderer.transform.localScale *= 1.6f;
                break;
            case 6:
                lifeMax = 400000 + (int)(lifeMax * 400000);
                bodyColor = new Color(.2f, .2f, .2f, 1);
                monsterSpriteRenderer.transform.localScale *= 2f;
                break;
            case 7:
                lifeMax = (40000 + (int)(lifeMax * 400000)) * 20;
                bodyColor = new Color(.1f, .1f, .1f, 1);
                monsterSpriteRenderer.transform.localScale *= 3;
                break;
        }

        //monsterSpriteRenderer.color = bodyColor;

    }
    public virtual void SetLifeSlider()
    {
        lifeCurrent = lifeMax;
        lifeSlider = GameObjectPoolManager.instance.lifeSliderPool.Get(transform.position + Vector3.up * monsterHeight, 10000).GetComponent<LifeSlider>();
        lifeSlider.transform.position = transform.position + Vector3.up * monsterHeight;
        lifeSlider.GetSepataters(lifeMax);
        lifeSlider.UpdataValue(lifePercent);
    }
   
    public virtual void FixedUpdate()
    {
        if (GameManager.instance.thisGameState == GameManager.GameState.gameOver) return;

        if (Time.frameCount % 10 == 0) { GetCloestTargetUnit(); }
        if (currentTargetUnit == null) return;

        if (freezed) return;
        if (knocked) return;
        if (timeFreezon) return;
        if (stuned) return;
        if (paralyzed) return;

        MeleeBehaviour();
        UpdateLifeSliderPos();
    }

    public Vector3 dirToBase
    {
        get
        {
            return (Base.instance.transform.position - transform.position).normalized;
        }
    }
    public Vector3 dirToTargetUnit
    {
        get
        {
            return (currentTargetUnit.transform.position - transform.position).normalized;
        }
    }
    public float distanceToBase
    {
        get
        {
            return Vector3.Distance(transform.position, Base.instance.transform.position);
        }
    }
    public float distanceToTargetUnit
    {
        get
        {
            return Vector3.Distance(transform.position, currentTargetUnit.transform.position);
        }
    }
    public float lifePercent
    {
        get
        {
            if (lifeMax <= 1) lifeMax = 1;
            return (float)lifeCurrent / lifeMax;
        }
    }


    
    public void MonsterScaling()
    {
        transform.localScale = new Vector3(1 + Mathf.Cos(Time.time * scalingSpeed * characterSpeed + scalingOffset * Mathf.PI * 2) * scalingAmplitude,
            1 + Mathf.Sin(Time.time * scalingSpeed * characterSpeed + scalingOffset * Mathf.PI * 2) * scalingAmplitude, 1);
    }

    public virtual void GetCloestTargetUnit()
    {
        if (confused)
        {
            List<Monster> monsters = new List<Monster>(MonsterManager.instance.monsters);
            monsters.Remove(this);

            if (monsters.Count > 0)
            {
                Monster cloestMonster = monsters[0];
                foreach (Monster monster in monsters)
                {
                    if (Vector3.Distance(transform.position, monster.transform.position) <
                        Vector3.Distance(transform.position, cloestMonster.transform.position))
                    {
                        cloestMonster = monster;
                    }
                }
                currentTargetUnit = cloestMonster.gameObject;
            }
            else
            {
                currentTargetUnit = null;
            }

        }
        else
        {
            BaseTargetUnit[] units = FindObjectsOfType<BaseTargetUnit>();
            if (units.Length == 0)
            {
                currentTargetUnit = null;
            }
            else
            {
                BaseTargetUnit cloestUnit = units[0];
                foreach (BaseTargetUnit unit in units)
                {
                    if (Vector3.Distance(transform.position, unit.transform.position)
                        < Vector3.Distance(transform.position, cloestUnit.transform.position))
                    {
                        cloestUnit = unit;
                    }
                }
                currentTargetUnit = cloestUnit.gameObject;
            }
        }


       
      
    }
    public virtual void MeleeBehaviour()
    {
        if (attackRange == 0) attackRange = .75f;

        float attackRangeTemp = attackRange;
        if (currentTargetUnit.GetComponent<Base>())
        {
            attackRangeTemp = attackRange + 1;
        }

        if (distanceToTargetUnit < attackRangeTemp)
        {
            MeleeAttackment();
        }
        else
        {
            MoveToBase();
        }
      
    }
    public virtual void MoveToBase()
    {
        MonsterScaling();
        transform.position += dirToTargetUnit * Time.deltaTime * getMoveSpeed;
    }
    public virtual void MeleeAttackment()
    {
        float speedTem = Time.deltaTime * attackSpeed;
        if (paralyzed) speedTem *= .1f;
        attackTimer += speedTem;
        if (attackTimer > 1.25f)
        {
            attackTimer = 0;
            StartCoroutine(Attack());
        }

        IEnumerator Attack()
        {
            if (transform.position.x > currentTargetUnit.transform.position.x)
            {
                monsterSpriteRenderer.transform.DOLocalRotate(new Vector3(0, 0, -30), .1f);
                yield return new WaitForSeconds(.2f);
                monsterSpriteRenderer.transform.DOLocalRotate(new Vector3(0, 0, 30), .1f);
                yield return new WaitForSeconds(.1f);
                AttackPoint();
                yield return new WaitForSeconds(.2f);
                monsterSpriteRenderer.transform.DOLocalRotate(new Vector3(0, 0, 0), .1f);
                yield return new WaitForSeconds(.2f);
            }
            else
            {
                monsterSpriteRenderer.transform.DOLocalRotate(new Vector3(0, 180, -30), .1f);
                yield return new WaitForSeconds(.2f);
                monsterSpriteRenderer.transform.DOLocalRotate(new Vector3(0, 180, 30), .1f);
                yield return new WaitForSeconds(.1f);
                AttackPoint();
                yield return new WaitForSeconds(.2f);
                monsterSpriteRenderer.transform.DOLocalRotate(new Vector3(0, 180, 0), .1f);
                yield return new WaitForSeconds(.2f);
            }
           // AttackPoint();
        }
    }
   
    public virtual void AttackPoint()
    {
        if (currentTargetUnit != null)
        {
            if (currentTargetUnit.GetComponent<Base>())
            {
                Base.instance.Hitted(attackDamage, transform.position);
                Hitted(RelicManager.instance.bronzeScalesDamageBackValue * lifeMax);
            }
            else if (currentTargetUnit.GetComponent<DollTarget>())
            {
                currentTargetUnit.GetComponent<DollTarget>().Hitted(transform.position);
            }
            else if (currentTargetUnit.GetComponent<Monster>())
            {
                float attackMonsterDamage = lifeMax * .1f;
                if (attackMonsterDamage < 1) attackMonsterDamage = 1;
                currentTargetUnit.GetComponent<Monster>().Hitted(attackMonsterDamage);
            }
        }
    }

    public void UpdateLifeSliderPos()
    {
        lifeSlider.transform.position = transform.position + Vector3.up * monsterHeight;
    }
    public virtual void Dead()
    {
        if (burnedEffect != null)
        {
            burnedEffect.GetComponent<GameObjectPoolInfo>().RemoveFast();
            burnedEffect.transform.parent = GameObjectPoolManager.instance.gameObject.transform;
        }
        if (freezonEffect)
        {
            freezonEffect.GetComponent<GameObjectPoolInfo>().RemoveFast();
            freezonEffect.transform.parent = GameObjectPoolManager.instance.gameObject.transform;
        }

        Destroy(gameObject);

        GameObject bodyPiece = GameObjectPoolManager.instance.monsterBodyExplosionEffectPool.Get(transform.position, 1.5f);
        bodyPiece.GetComponent<DeadBodyPieces>().SetColor(bloodColor);

        GameObjectPoolManager.instance.monsterBodyDeadBloodMarkEffectPool.Get(transform.position, 1.5f)
            .GetComponent<DeadBodyBloodMark>().SetColor(bloodColor);

        monsterValue *= RelicManager.instance.monsterValueMulplier;
        MonsterManager.instance.MonsterDead(transform.position, monsterValue);

       
        AudioManager.PlayClips("monsterDeadBody",.5f);
        dead = true;

        if (RelicManager.instance.bodyBombCount>0)
        {
            float f = Random.value;
            if (f < RelicManager.instance.bodyBombChance)
            {
                DeadBodyBomb deadBodyBomb = GameObjectPoolManager.instance.deadBodyBombPool.Get(transform.position, 3).GetComponent<DeadBodyBomb>();
                deadBodyBomb.damageValue = lifeMax * .3f;

            }
        }


        
        AchievementManager.instance.ReachAchievement(SteamManager.AchievementType.firstBlood);

        if (BodyCollectorDevice.instance)
        {
            if (BodyCollectorDevice.instance.active)
            {
                BodyPieceValue bodyPieceValue = Instantiate(Resources.Load<BodyPieceValue>("Prefab/bodyPieceValue"));
                bodyPieceValue.transform.position = transform.position;
            }
        }

        if (FastRepairDevice.instance)
        {
            FastRepairDevice.instance.MonsterDead();
        }

        GameManager.instance.ValuePlusOne("monsterDeadCount");

    }

    public virtual void Hitted(float damage)
    {
      //  damage *= 1000;

        if (dead) return;
        if (damage == 0) return;

        //if (RelicManager.instance.gunpowderCount>0)
        //{
        //    float f = Random.value;
        //    if(f < RelicManager.instance.gunpowderCritChance)
        //    {
        //        damage *= 2;
        //        GameObjectPoolManager.instance.critEffectPool.Get(transform.position, 1);
        //    }
        //}


        float critChance = 0;
        if (RelicManager.instance.gunpowderCount>0)
        {
            critChance += RelicManager.instance.gunpowderCritChance;
        }
        if (TalentManager.instance.critChanceUnlocked)
        {
            critChance += .3f;
        }
        if (critChance>0)
        {
            float f = Random.value;
            if (f< critChance)
            {
                damage *= 2;
                GameObjectPoolManager.instance.critEffectPool.Get(transform.position, 1);
            }
        }




        if (RelicManager.instance.telescopeCount>0)
        {
            if (distanceToBase >= GunRangeManager.instance.rangeRadius - 1)
            {
                damage *= RelicManager.instance.telescopeDamageMultiplier;
            }
        }
        if (RelicManager.instance.hammerCount>0)
        {
            if (lifePercent==1)
            {
                damage *= RelicManager.instance.hammerDamageMultiplier;
                Stuned();
            }
        }
        if (RelicManager.instance.steelPlateCount>0)
        {
            if (Base.instance.lifePercent==1)
            {
                damage *= RelicManager.instance.steelPlateDamageMultiplier;
                
            }
        }
        if (RelicManager.instance.partnerCount>0)
        {
            damage *= WeaponsManager.instance.partnerWeaponDamageMultiplier;
        }
        if (RelicManager.instance.loneWolfCount>0)
        {
            damage *= WeaponsManager.instance.loneWolfWeaponDamageMultiplier;
        }
        if (RelicManager.instance.showCaseCount>0)
        {
            damage *= RelicManager.instance.showCaseRelicsCountDamageMultiplier;
        }

        if (TalentManager.instance.riskForDamageUnlocked)
        {
            damage *= 2;
        }
        if (TalentManager.instance.moneyForDamageUnlocked)
        {
            damage *= CoinsManager.instance.moneyForDamageMultiplier;
        }

        damage *= Random.Range(.9f, 1.1f);
        damage = Mathf.Clamp(damage, 0, lifeMax);
        lifeCurrent -= damage;
        lifeCurrent = Mathf.Clamp(lifeCurrent, 0, lifeMax);

        if (RelicManager.instance.anchorCount>0)
        {
            if (lifePercent <= RelicManager.instance.anchorKillPercentValue)
            {
                lifeCurrent = 0;
                GameObject anchorKillEffect = GameObjectPoolManager.instance.anchorKillEffectPool.Get(transform.position, 1);
            }
        }

        if (lifeCurrent <= 0)
        {
            lifeCurrent = 0;
            Dead();
        }

        float lifePercentTem = lifePercent;
        lifePercentTem = Mathf.Clamp(lifePercent, 0, 1);
        lifeSlider.UpdataValue(lifePercentTem);

        if (OptionsManager.damageValueTextOn)
        {
            // DamageValueText damageValueText = Instantiate(Resources.Load<DamageValueText>("Prefab/DamageValueText"));
            GameObject damageValueText = GameObjectPoolManager.instance.damageValueTextPool.Get(transform.position + Vector3.up * .2f, 1);
            damageValueText.transform.position = transform.position + Vector3.up * .2f;
            damageValueText.transform.position += transform.position.normalized * 1;
            damageValueText.GetComponent<DamageValueText>().SetText(damage);
        }

        StartCoroutine(HittedWhite());
        IEnumerator HittedWhite()
        {
            monsterSpriteRenderer.material = GetComponent<MonsterBodyColor>().monsterHittedMat;
            hitted = true;
            yield return new WaitForSeconds(.15f);

            monsterSpriteRenderer.material = GetComponent<MonsterBodyColor>().monsterNormalMat;
            hitted = false;
        }
    }
    public virtual void HittedByBullet(float damage,int gunIndex)
    {
        Hitted(damage);
        if (dead)
        {
            if (RelicManager.instance.aiLearningCount>0)
            {
                if (gunIndex == 0)
                {
                    RelicManager.instance.gunBulletAddValue += RelicManager.instance.aiLearningDamageAddValue*.05f;
                }
                else if (gunIndex == 1)
                {
                    RelicManager.instance.sniperBulletAddValue += RelicManager.instance.aiLearningDamageAddValue*.2f;
                }
                else if (gunIndex ==2)
                {
                    RelicManager.instance.gatlingBulletAddValue += RelicManager.instance.aiLearningDamageAddValue * 50;
                }
                WeaponsManager.instance.UpdateGunControllerAILearningBuffs();
            }
        }

    }
    public virtual void HittedByElectric(float damage)
    {
        Hitted(damage);
        Slowed();

        if (RelicManager.instance.superChargerCount>0)
        {
            float f = Random.value;
            if(f < RelicManager.instance.superChargerChance)
            {
                Paralyzed();
            }
        }
    }

    public void Slowed()
    {
        StopCoroutine(SlowedIE());
        StartCoroutine(SlowedIE());
        IEnumerator SlowedIE()
        {
            slowed = true;
            yield return new WaitForSeconds(.5f);
            slowed = false;
        }
    }
    public void Stuned()
    {
        StopCoroutine(StunedIE());
        StartCoroutine(StunedIE());
        IEnumerator StunedIE()
        {
            stuned = true;
            GameObjectPoolManager.instance.stunedEffectPool.Get(transform.position, 1);
            yield return new WaitForSeconds(1.5f);
            stuned = false;
        }
    }
    public void Posioned()
    {
        poisoned = true;
        StopCoroutine(PoisedOverIE());
        StartCoroutine(PoisedOverIE());
        IEnumerator PoisedOverIE()
        {
            yield return new WaitForSeconds(1.5f);
            poisoned = false;
        }
    }
    public void Burned()
    {
        if (burnedEffect == null)
        {
            burnedEffect = GameObjectPoolManager.instance.monsterBurnedEffectPool.Get(transform.position, 1);
            burnedEffect.transform.parent = transform;
            burnedEffect.transform.localPosition = Vector3.zero;
        }
        Slowed();

    }
    public void Paralyzed()
    {
        paralyzed = true;
        if (paralyzedEffect==null)
        {
            paralyzedEffect = GameObjectPoolManager.instance.paralyzedEffectPool
                .Get(transform.position+Vector3.up*.5f, 3.5f);
        }
        monsterSpriteRenderer.material = GetComponent<MonsterBodyColor>().monsterHittedMat;

        StopCoroutine(ParalyzedOverIE());
        StartCoroutine(ParalyzedOverIE());
        IEnumerator ParalyzedOverIE()
        {
            yield return new WaitForSeconds(3.5f);
            paralyzed = false;
            paralyzedEffect = null;
            monsterSpriteRenderer.material = GetComponent<MonsterBodyColor>().monsterNormalMat;
        }

    }

    public void Knocked()
    {
        knocked = true;
        StopCoroutine(KnockedOverIE());
        StartCoroutine(KnockedOverIE());

        IEnumerator KnockedOverIE()
        {
            monsterSpriteRenderer.transform.DOLocalMoveY(1, 0.5f);
            yield return new WaitForSeconds(0.5f);
            monsterSpriteRenderer.transform.DOLocalMoveY(0, 0.5f);

            yield return new WaitForSeconds(2.25f);
            knocked = false;
        }
    }

    public void Confused()
    {
        confused = true;
        GetCloestTargetUnit();
        monsterSpriteRenderer.color = ColorManager.instance.monsterConfusedColor;

        StartCoroutine(ConfusedOverIE());
        IEnumerator ConfusedOverIE()
        {
            yield return new WaitForSeconds(5);
            confused = false;
            GetCloestTargetUnit();
            monsterSpriteRenderer.color = new Color(1, 1, 1);
        }
    }


    public void SimpleHittedEffect()
    {
        monsterSpriteRenderer.color = Color.black;

        StopCoroutine(ColorBackIE());
        StartCoroutine(ColorBackIE());
        IEnumerator ColorBackIE()
        {
            yield return new WaitForSeconds(.2f);
            monsterSpriteRenderer.color = Color.white;
        }
    }

    public void GetEnergyBallMark()
    {
        energyBallMarked = true;
        energyBallMarkEffect = Instantiate(Resources.Load<GameObject>("Effect/energyBallMark"));
        energyBallMarkEffect.transform.parent = transform;
        energyBallMarkEffect.transform.localPosition = Vector3.zero;

        AudioManager.PlayClip("energyBallMark");
        Hitted(1f);
    }
    public void EnergyBallInvoke()
    {
        energyBallMarked = false;
        Destroy(energyBallMarkEffect.gameObject);
        AudioManager.PlayClip("energyBallInvoke");
    }

    public void AddFreezeValue(float value)
    {
        freezingTimer += value;

        float freezonTime =3;
        if (FreezingAirLuncher.instance)
        {
            freezonTime = FreezingAirLuncher.instance.freezingAirFreezonDuration;
        }

        if (freezingTimer>= 1 && !freezed)
        {
            freezonEffect = GameObjectPoolManager.instance.freezonEffectPool.Get(transform.position, freezonTime);
            freezed = true;

            StopCoroutine(FreezingOver());
            StartCoroutine(FreezingOver());

            AudioManager.PlayClip("ice");
        }
       
        IEnumerator FreezingOver()
        {
            yield return new WaitForSeconds(freezonTime);
            freezingTimer = 0;
            freezed = false;
            if (freezonEffect)
            {
                freezonEffect.GetComponent<GameObjectPoolInfo>().RemoveFast();
                freezonEffect = null;
            }
        }
    }

    public float getMoveSpeed
    {
        get
        {
            float speedTem= .5f * speed * characterSpeed;

            if (poisoned) speedTem *= .1f;
            if (hitted) speedTem *= .45f;
            if (slowed) speedTem *= .75f;
            if (freezingTimer > 0) speedTem *= .5f;

            return speedTem;
        }
    }


    public void TimeFreezon()
    {
        monsterSpriteRenderer.material = GetComponent<MonsterBodyColor>().monsterTimeFreezonMat;
        timeFreezon = true;
    }
    public void TimeFreezonOver()
    {
        monsterSpriteRenderer.material = GetComponent<MonsterBodyColor>().monsterNormalMat;
        timeFreezon = false;
    }

    public virtual void Escape()
    {
        Destroy(gameObject);
        if (lifeSlider)
        {
            lifeSlider.GetComponent<GameObjectPoolInfo>().RemoveFast();
        }

        if (burnedEffect)
        {
            burnedEffect.transform.parent = GameObjectPoolManager.instance.gameObject.transform;
            burnedEffect.GetComponent<GameObjectPoolInfo>().RemoveFast();
        }
    }
}
