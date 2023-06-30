using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondMeteorite : MonoBehaviour
{
    public float fallSpeed;
    public Vector3 fallDir;
    bool falling;
    public enum MeteoriteMineralType { Diamond,Gold}
    public MeteoriteMineralType thisMeteoriteType = MeteoriteMineralType.Diamond;
    private void Awake()
    {
        falling = true;
        transform.localScale = Vector3.one * Random.Range(.85f, 1.2f);
        fallSpeed *= Random.Range(.85f, 1.2f);
    }
    private void FixedUpdate()
    {
        if (transform.position.y<=0)
        {
            if (falling)
            {
                falling = false;
                Destroy(GetComponentInChildren<TrailRenderer>().gameObject, 1);
                GetComponentInChildren<TrailRenderer>().transform.parent = null;
                Destroy(gameObject);
                
                GameObjectPoolManager.instance.explosionShadowEffectPool.Get(transform.position, 3);
                switch (thisMeteoriteType)
                {
                    case MeteoriteMineralType.Diamond:
                        GameObjectPoolManager.instance.diamondMeteoriteExplosionEffectPool.Get(transform.position, 1);
                        break;
                    case MeteoriteMineralType.Gold:
                        GameObjectPoolManager.instance.goldMeteoriteExplosionEffectPool.Get(transform.position, 1);
                        break;
                    default:
                        break;
                }

                if (Vector3.Distance(transform.position,Base.instance.transform.position)>2f)
                {
                    float f = Random.value;
                    if (f < .05f)
                    {
                        string mineralName = "diamondMeteoriteMineral";
                        switch (thisMeteoriteType)
                        {
                            case MeteoriteMineralType.Diamond:
                                break;
                            case MeteoriteMineralType.Gold:
                                mineralName = "goldMeteoriteMineral";
                                break;
                            default:
                                break;
                        }

                        DiamondMeteoriteMineral diamondMeteoriteMineral =
                            Instantiate(Resources.Load<DiamondMeteoriteMineral>("Prefab/"+mineralName));

                        diamondMeteoriteMineral.transform.position = transform.position;
                    }

                }
                else
                {
                    Base.instance.Hitted(.15f,transform.position);
                }

            }
        }

        if (falling)
        {
            transform.position += fallDir * Time.deltaTime * fallSpeed;
        }

    }
}
