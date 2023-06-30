using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPoolManager : MonoBehaviour
{
    public static GameObjectPoolManager instance;

    Dictionary<string, BaseGameObjectPool> m_poolDic;
    Transform m_parentTrans;

    [Space(20)]
    public GameObject lightningHitEffectPrefab;
    public BaseGameObjectPool lightningHitEffectPool;

    public GameObject gunBulletPrefab;
    public BaseGameObjectPool gunBulletPool;

    public GameObject bulletHitEffectPrefab;
    public BaseGameObjectPool bulletHitEffectPool;

    public GameObject coinPrefab;
    public BaseGameObjectPool coinPool;

    public GameObject diamondPrefab;
    public BaseGameObjectPool diamondPool;

    public GameObject missilePrefab;
    public BaseGameObjectPool missilePool;

    public GameObject missileExplosionEffectPrefab;
    public BaseGameObjectPool missileExplosionEffectPool;

    public GameObject lifeSliderPrefab;
    public BaseGameObjectPool lifeSliderPool;

    public GameObject laserBulletPrefab;
    public BaseGameObjectPool laserBulletPool;

    public GameObject teslaTowerHitEffectPrefab;
    public BaseGameObjectPool teslaTowerHitEffectPool;

    public GameObject baseHittedEffectPrefab;
    public BaseGameObjectPool baseHittedEffectPool;


    public GameObject monsterBodyExplosionEffectPrefab;
    public BaseGameObjectPool monsterBodyExplosionEffectPool;

    public GameObject monsterBodyDeadBloodMarkEffectPrefab;
    public BaseGameObjectPool monsterBodyDeadBloodMarkEffectPool;

    public GameObject lifeSliderSeparaterPrefab;
    public BaseGameObjectPool lifeSliderSeparaterPool;

    public GameObject getDiamondValueTextPrefab;
    public BaseGameObjectPool getDiamondValueTextPool;

    public GameObject monsterBulletPrefab;
    public BaseGameObjectPool monsterBulletPool;

    public GameObject paralyzedEffectPrefab;
    public BaseGameObjectPool paralyzedEffectPool;
    

    [Space(10)]
    public GameObject lightningBulletPrefab;
    public BaseGameObjectPool lightningBulletPool;

    public GameObject lightningChainBulletPrefab;
    public BaseGameObjectPool lightningChainBulletPool;

    public GameObject flameBulletPrefab;
    public BaseGameObjectPool flameBulletPool;

    public GameObject monsterBurnedEffectPrefab;
    public BaseGameObjectPool monsterBurnedEffectPool;

    public GameObject sniperBulletPrefab;
    public BaseGameObjectPool sniperBulletPool;

    public GameObject sniperBulletHitEffectPrefab;
    public BaseGameObjectPool sniperBulletHitEffectPool;

    public GameObject shurikenBPrefab;
    public BaseGameObjectPool shurikenBPool;

    public GameObject shurikenBHitEffectPrefab;
    public BaseGameObjectPool shurikenBHitEffectPool;

    public GameObject damageValueTextPrefab;
    public BaseGameObjectPool damageValueTextPool;

    public GameObject deadBodyBombPrefab;
    public BaseGameObjectPool deadBodyBombPool;


    [Header("-----effect-----")]
    public GameObject anchorKillEffectPrefab;
    public BaseGameObjectPool anchorKillEffectPool;

    public GameObject stunedEffectPrefab;
    public BaseGameObjectPool stunedEffectPool;

    public GameObject critEffectPrefab;
    public BaseGameObjectPool critEffectPool;

    [Header("-----weapon group c -----")]
    public GameObject freezonEffectPrefab;
    public BaseGameObjectPool freezonEffectPool;

    public GameObject ballLightningPrefab;
    public BaseGameObjectPool ballLightningPool;

    public GameObject ballLightningLinePrefab;
    public BaseGameObjectPool ballLightningLinePool;

    public GameObject gatlingBulletPrefab;
    public BaseGameObjectPool gatlingBulletPool;

    public GameObject gatlingMuzzleFlashPrefab;
    public BaseGameObjectPool gatlingMuzzleFlashPool;

    [Header("-----talent-----")]
    public GameObject dollTargetHittedEffectPrefab;
    public BaseGameObjectPool dollTargetHittedEffectPool;

    public GameObject diamondMeteoriteExplosionEffectPrefab;
    public BaseGameObjectPool diamondMeteoriteExplosionEffectPool;

    public GameObject goldMeteoriteExplosionEffectPrefab;
    public BaseGameObjectPool goldMeteoriteExplosionEffectPool;

    public GameObject explosionShadowEffectPrefab;
    public BaseGameObjectPool explosionShadowEffectPool;
   
    private void Awake()
    {
        instance = this;
        m_poolDic = new Dictionary<string, BaseGameObjectPool>();
        m_parentTrans = gameObject.transform;

        lightningHitEffectPool = instance.CreatGameObjectPool<BaseGameObjectPool>("lightningHitEffectPool");
        lightningHitEffectPool.prefab = lightningHitEffectPrefab;


        gunBulletPool = instance.CreatGameObjectPool<BaseGameObjectPool>("gunBulletPool");
        gunBulletPool.prefab = gunBulletPrefab;

        bulletHitEffectPool = instance.CreatGameObjectPool<BaseGameObjectPool>("bulletHitEffectPool");
        bulletHitEffectPool.prefab = bulletHitEffectPrefab;

        coinPool = instance.CreatGameObjectPool<BaseGameObjectPool>("coinPool");
        coinPool.prefab = coinPrefab;

        diamondPool = instance.CreatGameObjectPool<BaseGameObjectPool>("diamondPool");
        diamondPool.prefab = diamondPrefab;

        missilePool = instance.CreatGameObjectPool<BaseGameObjectPool>("missilePool");
        missilePool.prefab = missilePrefab;

        missileExplosionEffectPool = instance.CreatGameObjectPool<BaseGameObjectPool>("missileExplosionEffectPool");
        missileExplosionEffectPool.prefab = missileExplosionEffectPrefab;


        lifeSliderPool = instance.CreatGameObjectPool<BaseGameObjectPool>("lifeSliderPool");
        lifeSliderPool.prefab = lifeSliderPrefab;

        laserBulletPool = instance.CreatGameObjectPool<BaseGameObjectPool>("laserBulletPool");
        laserBulletPool.prefab = laserBulletPrefab;

        teslaTowerHitEffectPool = instance.CreatGameObjectPool<BaseGameObjectPool>("teslaTowerHitEffectPool");
        teslaTowerHitEffectPool.prefab = teslaTowerHitEffectPrefab;

        baseHittedEffectPool = instance.CreatGameObjectPool<BaseGameObjectPool>("baseHittedEffectPool");
        baseHittedEffectPool.prefab = baseHittedEffectPrefab;

        monsterBodyExplosionEffectPool = instance.CreatGameObjectPool<BaseGameObjectPool>("monsterBodyExplosionEffectPool");
        monsterBodyExplosionEffectPool.prefab = monsterBodyExplosionEffectPrefab;

        monsterBodyDeadBloodMarkEffectPool = instance.CreatGameObjectPool<BaseGameObjectPool>("monsterBodyDeadBloodMarkEffectPool");
        monsterBodyDeadBloodMarkEffectPool.prefab = monsterBodyDeadBloodMarkEffectPrefab;

        lifeSliderSeparaterPool = instance.CreatGameObjectPool<BaseGameObjectPool>("lifeSliderSeparaterPool");
        lifeSliderSeparaterPool.prefab = lifeSliderSeparaterPrefab;


        getDiamondValueTextPool = instance.CreatGameObjectPool<BaseGameObjectPool>("getDiamondValueTextPool");
        getDiamondValueTextPool.prefab = getDiamondValueTextPrefab;

        monsterBulletPool = instance.CreatGameObjectPool<BaseGameObjectPool>("monsterBulletPool");
        monsterBulletPool.prefab = monsterBulletPrefab;

        lightningBulletPool = instance.CreatGameObjectPool<BaseGameObjectPool>("lightningBulletPool");
        lightningBulletPool.prefab = lightningBulletPrefab;

        lightningChainBulletPool = instance.CreatGameObjectPool<BaseGameObjectPool>("lightningChainBulletPool");
        lightningChainBulletPool.prefab = lightningChainBulletPrefab;

        flameBulletPool = instance.CreatGameObjectPool<BaseGameObjectPool>("flameBulletPool");
        flameBulletPool.prefab = flameBulletPrefab;

        monsterBurnedEffectPool = instance.CreatGameObjectPool<BaseGameObjectPool>("monsterBurnEffectPool");
        monsterBurnedEffectPool.prefab = monsterBurnedEffectPrefab;

        sniperBulletPool = instance.CreatGameObjectPool<BaseGameObjectPool>("sniperBulletPool");
        sniperBulletPool.prefab = sniperBulletPrefab;

        sniperBulletHitEffectPool = instance.CreatGameObjectPool<BaseGameObjectPool>("sniperBulletHitEffectPool");
        sniperBulletHitEffectPool.prefab = sniperBulletHitEffectPrefab;

        shurikenBPool = instance.CreatGameObjectPool<BaseGameObjectPool>("shurikenBPool");
        shurikenBPool.prefab = shurikenBPrefab;

        shurikenBHitEffectPool = instance.CreatGameObjectPool<BaseGameObjectPool>("shurikenBHitEffectPool");
        shurikenBHitEffectPool.prefab = shurikenBHitEffectPrefab;

        damageValueTextPool = instance.CreatGameObjectPool<BaseGameObjectPool>("damageValueTextPool");
        damageValueTextPool.prefab = damageValueTextPrefab;

        deadBodyBombPool = instance.CreatGameObjectPool<BaseGameObjectPool>("deadBodyBombPool");
        deadBodyBombPool.prefab = deadBodyBombPrefab;

        anchorKillEffectPool = instance.CreatGameObjectPool<BaseGameObjectPool>("anchorKillPool");
        anchorKillEffectPool.prefab = anchorKillEffectPrefab;

        stunedEffectPool = instance.CreatGameObjectPool<BaseGameObjectPool>("stunedEffectPool");
        stunedEffectPool.prefab = stunedEffectPrefab;

        critEffectPool = instance.CreatGameObjectPool<BaseGameObjectPool>("critEffectPool");
        critEffectPool.prefab = critEffectPrefab;

        freezonEffectPool = instance.CreatGameObjectPool<BaseGameObjectPool>("freezonEffectPool");
        freezonEffectPool.prefab = freezonEffectPrefab;

        ballLightningPool = instance.CreatGameObjectPool<BaseGameObjectPool>("ballLightningPool");
        ballLightningPool.prefab = ballLightningPrefab;

        ballLightningLinePool = instance.CreatGameObjectPool<BaseGameObjectPool>("ballLightningLinePool");
        ballLightningLinePool.prefab = ballLightningLinePrefab;

        gatlingBulletPool = instance.CreatGameObjectPool<BaseGameObjectPool>("gatlingBulletPool");
        gatlingBulletPool.prefab = gatlingBulletPrefab;

        gatlingMuzzleFlashPool = instance.CreatGameObjectPool<BaseGameObjectPool>("gatlingMuzzleFlashPool");
        gatlingMuzzleFlashPool.prefab = gatlingMuzzleFlashPrefab;

        paralyzedEffectPool = instance.CreatGameObjectPool<BaseGameObjectPool>("paralyzedEffectPool");
        paralyzedEffectPool.prefab = paralyzedEffectPrefab;

        dollTargetHittedEffectPool = instance.CreatGameObjectPool<BaseGameObjectPool>("dollTargetHittedEffectPool");
        dollTargetHittedEffectPool.prefab = dollTargetHittedEffectPrefab;

        diamondMeteoriteExplosionEffectPool = instance.CreatGameObjectPool<BaseGameObjectPool>("diamondMeteoriteExplosionEffectPool");
        diamondMeteoriteExplosionEffectPool.prefab = diamondMeteoriteExplosionEffectPrefab;

        goldMeteoriteExplosionEffectPool = instance.CreatGameObjectPool<BaseGameObjectPool>("goldMeteoriteExplosionEffectPool");
        goldMeteoriteExplosionEffectPool.prefab = goldMeteoriteExplosionEffectPrefab;

        explosionShadowEffectPool = instance.CreatGameObjectPool<BaseGameObjectPool>("explosionShadowEffectPool");
        explosionShadowEffectPool.prefab = explosionShadowEffectPrefab;
    }
  
    public T CreatGameObjectPool<T>(string poolName) where T : BaseGameObjectPool, new()
    {
        if (m_poolDic.ContainsKey(poolName))
        {
            return (T)m_poolDic[poolName];
        }

        GameObject obj = new GameObject(poolName);
        obj.transform.SetParent(m_parentTrans);
        T pool = new T();
        pool.Init(poolName, obj.transform);
        m_poolDic.Add(poolName, pool);
        return pool;
    }
    public GameObject GetGameObject(string poolName,Vector3 postion,float lifeTime)
    {
        if (m_poolDic.ContainsKey(poolName))
        {
            return m_poolDic[poolName].Get(postion, lifeTime);
        }
        return null;
    }
    public void RemoveGameObject(string poolName,GameObject go)
    {
        if (m_poolDic.ContainsKey(poolName))
        {
            m_poolDic[poolName].Remove(go);
        }
    }

    public void Destroy()
    {
        m_poolDic.Clear();
        GameObject.Destroy(m_parentTrans);
    }
}
