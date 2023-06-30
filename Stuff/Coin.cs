using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float moveSpeed = 3;
    Vector3 dir;
    Vector3 dirToBase;
    float distanceToBase;
    bool movingOut;
    public int value;

    public GameObject moveTargetGameObject;

    private void Awake()
    {
        SpriteRenderer coinsSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        coinsSpriteRenderer.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0,360));
    }
    private void OnEnable()
    {
        if (moveTargetGameObject==null)
        {
            moveTargetGameObject = Base.instance.transform.gameObject;
        }
        if (moveTargetGameObject.GetComponent<Base>())
        {
            dir = (transform.position).normalized + Vector3.up * .25f;
            dir += new Vector3(Random.value - .5f, Random.value - .5f, Random.value - .5f) * .65f;
            dir = dir.normalized * 3;
            moveSpeed = 5;
        }

        if (moveTargetGameObject.GetComponent<Thief>())
        {
            dir = -moveTargetGameObject.transform.position.normalized + Vector3.up;
            dir += Random.insideUnitSphere * .5f;
            dir = dir.normalized * 3;
            moveSpeed = 3;
        }

    }
    public void DirToBase()
    {
        DirToBase(5);
    }
    public void DirToBase(float _speedMultiplier)
    {
        dir = -transform.position.normalized;
        dir += Random.onUnitSphere;
        dir = dir.normalized;
        moveSpeed = _speedMultiplier;
    }

    private void FixedUpdate()
    {
        if (moveTargetGameObject==null)
        {
            Destroy(gameObject);
            return;
        }


        if (moveTargetGameObject.GetComponent<Base>())
        {
            dirToBase = (Base.instance.transform.position - transform.position).normalized;
            distanceToBase = Vector3.Distance(transform.position, Base.instance.transform.position);
            if (Vector3.Dot(dir, dirToBase) < 0)
            {
                movingOut = true;
            }
            else
            {
                movingOut = false;
            }

            dir = Vector3.Lerp(dir, dirToBase, .075f);
            transform.position += dir * Time.deltaTime * moveSpeed;
            if (distanceToBase < .25f && !movingOut)
            {
                GetComponent<GameObjectPoolInfo>().RemoveFast();
                AddValue();
            }
        }
        else if (moveTargetGameObject.GetComponent<Thief>())
        {
            Vector3 targetDir =(moveTargetGameObject.transform.position - transform.position).normalized;
            dir = Vector3.Lerp(dir, targetDir, .05f);
            transform.position += dir * Time.deltaTime * moveSpeed;
            float distanceToTarget = Vector3.Distance(transform.position, moveTargetGameObject.transform.position);

            if (distanceToTarget<.25f)
            {
                GetComponent<GameObjectPoolInfo>().RemoveFast();
                moveTargetGameObject.GetComponent<Thief>().StealCoin();
            }

        }
       
    }

    public virtual void AddValue()
    {
        if (moveTargetGameObject.GetComponent<Base>())
        {
            CoinsManager.instance.AddMoney(value);
            value = 1;
            transform.localScale = Vector3.one;
        }
       
    }
}
