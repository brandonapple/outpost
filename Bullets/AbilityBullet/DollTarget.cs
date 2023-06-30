using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DollTarget : BaseTargetUnit
{
    public float lifeCurrent;
    public float lifeMax;

    public SpriteRenderer dollTargetSpriteRenderer;
    public LifeSlider lifeSlider;

    public LifeSlider lastTimeSlider;

    public float lastTime;
    public float lastTimeMax;
    private void Start()
    {
        lifeMax = 10;
        lifeCurrent = lifeMax;

        MonsterManager.instance.AllMonstersCheckTarget();
        AudioManager.PlayClip("dollTarget");


        lifeSlider = GameObjectPoolManager.instance.lifeSliderPool
            .Get(transform.position +Vector3.up,Mathf.Infinity).GetComponent<LifeSlider>();
        lifeSlider.SetColor(Color.green);
        RenderLifeSlider();

        lastTimeMax = 15;
        lastTime = lastTimeMax;


        lastTimeSlider = GameObjectPoolManager.instance.lifeSliderPool
            .Get(transform.position + Vector3.up * .85f, Mathf.Infinity).GetComponent<LifeSlider>();
        lastTimeSlider.SetColor(Color.yellow);
        
    }
    public override void Hitted(Vector3 damageFromPos)
    {
        base.Hitted();
        lifeCurrent--;
        if (lifeCurrent<=0)
        {
            Dead();
        }

        dollTargetSpriteRenderer.transform.rotation = Quaternion.Euler(0, 0, 0);
        dollTargetSpriteRenderer.transform.DOShakeRotation(.25f, 40, 30, 20, true);

        AudioManager.PlayClips("dollTargetHitted");
        RenderLifeSlider();

        Vector3 hittedEffectPosOffset = (damageFromPos - transform.position).normalized  * Random.Range(.2f,.3f);

        GameObjectPoolManager.instance.dollTargetHittedEffectPool.Get
            (transform.position+Vector3.up * .35f + hittedEffectPosOffset, 2);
    }

    public void Dead()
    {
        Destroy(gameObject);
        MonsterManager.instance.AllMonstersCheckTarget();

        lifeSlider.GetComponent<GameObjectPoolInfo>().RemoveFast();
        lastTimeSlider.GetComponent<GameObjectPoolInfo>().RemoveFast();
    }

    public void RenderLifeSlider()
    {
        lifeSlider.UpdataValue((float)lifeCurrent / lifeMax);
    }

    public void RenderTimeSlider()
    {
        lastTimeSlider.UpdataValue(lastTime / lastTimeMax);

        if (lastTime<=0)
        {
            Dead();
        }
    }

    private void FixedUpdate()
    {
        lastTime -= Time.deltaTime;
        RenderTimeSlider();
    }
}
