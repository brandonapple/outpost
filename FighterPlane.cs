using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class FighterPlane : MonoBehaviour
{


    public float timer;
    bool working;
    SpriteRenderer planeSpriteRenderer;
    private void Awake()
    {
        planeSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        working = false;
        FadeIn();
    }
    private void Start()
    {
        if (transform.position.x > 0)
        {
            planeSpriteRenderer.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    private void FixedUpdate()
    {
        if (!working) return;
        timer += Time.deltaTime * 8f;
        if (timer>=1)
        {
            if (MonsterManager.cloestMonster(transform.position) == null)
            {
                return;
            }
            timer = 0;
            Fire();
        }
    }

    void Fire()
    {
        //Bullet bullet = Instantiate(Resources.Load<Bullet>("Bullets/gunBullet"));
        Bullet bullet = GameObjectPoolManager.instance.gunBulletPool.Get(transform.position, 2).GetComponent<Bullet>();
        bullet.bulletFromPlane = true;


        bullet.transform.position = transform.position;
        Monster cloestMonster = MonsterManager.cloestMonster(transform.position);
        bullet.targetMonster = cloestMonster;
    }

    public void FadeIn()
    {
        planeSpriteRenderer.transform.localPosition = new Vector3(0, 5, 0);
       

        planeSpriteRenderer.color = new Color(0, 0, 0, 0);

        planeSpriteRenderer.transform.DOLocalMoveY(0, 1);
        planeSpriteRenderer.DOColor(Color.white, 1);

        AudioManager.PlayClip("fighterPlane");


        StartCoroutine(BeginWork());
        IEnumerator BeginWork()
        {
            yield return new WaitForSeconds(1);
            working = true;
        }

        Invoke(nameof(FadeOut), 6);
    }
    void FadeOut()
    {
        working = false;
        planeSpriteRenderer.transform.DOLocalMoveY(5, 1);
        planeSpriteRenderer.DOColor(new Color(0,0,0,0), 1);
        Destroy(gameObject, 1);
    }
}
