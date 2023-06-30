using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;
    public GameObject bulletHitEffect;
    public GameObject deadBodyPieces;
    public GameObject bodyBloodMark;

    public GameObject missileExplosionEffect;
    public GameObject lightningFenceHitEffect;
    public GameObject lightningChainHitEffect;
    public GameObject mushRoomExplosionEffect;
    public GameObject stunedMissileExplosionEffect;
    public GameObject topLaserHitEffect;
    public GameObject shipLandedEffect;
    public GameObject baseHittedEffect;
    public GameObject digOutEffect;
    public GameObject baseExplosionEffect;
    public GameObject monsterBombExplosionEffect;
    public GameObject teslaTowerHitEffect;
    public GameObject monsterBurnedEffect;
    public GameObject sniperMuzzleEffect;
    public GameObject sniperBulletHitEffect;
    public GameObject shurikenBHitEffect;
    public GameObject magneticFieldExpolosionEffecct;
    private void Awake()
    {
        instance = this;
    }
    public void SpawnEffect(string effectName,Vector3 pos,Quaternion qua)
    {
        if (!OptionsManager.effectOn) return;


        GameObject effect = null;
        switch (effectName)
        {
            case "bulletHit":
                GameObjectPoolManager.instance.bulletHitEffectPool.Get(pos, 1);
                break;
            //case "deadBodyPieces":
            //    GameObjectPoolManager.instance.monsterBodyExplosionEffectPool.Get(pos, 1.5f);
            //    break;
            case "bodyBloodMark":
                effect = Instantiate(bodyBloodMark, pos, qua);
                Destroy(effect, 5);
                break;
            case "missileExplosion":
               
                effect = GameObjectPoolManager.instance.missileExplosionEffectPool.Get(pos, 1);
                effect.transform.localScale = Vector3.one * DataManager.instance.getCurrentValueByString("missileRadius") * .5f;
               
                break;
            case "lightningFenceHit":
                GameObjectPoolManager.instance.lightningHitEffectPool.Get(pos+Vector3.up*.5f, 1);
                break;
            case "lightningChainHit":
                effect = Instantiate(lightningChainHitEffect, pos+Vector3.up*.35f, qua);
                effect.transform.localScale = Vector3.one;
                Destroy(effect, 1);
                break;
            case "mushRoomExplosion":
                effect = Instantiate(mushRoomExplosionEffect, pos, qua);
                effect.transform.localScale = Vector3.one * .5f;
                Destroy(effect, 5);
                break;
            case "stunedMissileExplosion":
                effect = Instantiate(stunedMissileExplosionEffect, pos, qua);
                effect.transform.localScale = Vector3.one * .25f;
                Destroy(effect, 5);
                break;
            case "topLaserHitEffect":
                effect = Instantiate(topLaserHitEffect, pos, qua);
                effect.transform.localScale = Vector3.one * .3f;
                Destroy(effect, 3);
                break;
            case "shipLandedEffect":
                effect = Instantiate(shipLandedEffect, pos, qua);
                effect.transform.localScale = Vector3.one;
                Destroy(effect, 5);
                break;
            case "baseHittedEffect":

                GameObjectPoolManager.instance.baseHittedEffectPool.Get(pos, 1);

                break;
            case "digOutEffect":
                effect = Instantiate(digOutEffect, pos, qua);
                effect.transform.localScale = Vector3.one;
                Destroy(effect, 5);
                break;
            case "baseExplosionEffect":
                effect = Instantiate(baseExplosionEffect, pos, qua);
                effect.transform.localScale = Vector3.one;
                Destroy(effect, 5);
                break;
            case "monsterBombExplosionEffect":
                effect = Instantiate(monsterBombExplosionEffect, pos, qua);
                effect.transform.localScale = Vector3.one * .3f;
                Destroy(effect, 5);
                break;
            case "teslaTowerHitEffect":
                GameObjectPoolManager.instance.teslaTowerHitEffectPool.Get(pos, .75f);

                break;
            default:
                break;
        }

        if (effect!=null)
        {
            AudioSource[] audioSources = effect.GetComponentsInChildren<AudioSource>();
            foreach (AudioSource audioSource in audioSources)
            {
                audioSource.volume = OptionsManager.audioVolumeValue;
            }
        }
       
        
    }

}
