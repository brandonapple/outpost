using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPoolInfo : MonoBehaviour
{
    public float lifetime = 0;
    public string pooName;
    WaitForSeconds m_waitTime;
    private void Awake()
    {
        if (lifetime > 0)
        {
            m_waitTime = new WaitForSeconds(lifetime);
        }
    }

    private void OnEnable()
    {
        if (lifetime > 0)
        {
            StartCoroutine(CountDown(lifetime));
        }
    }
    IEnumerator CountDown(float lifetime)
    {
        yield return m_waitTime;
        GameObjectPoolManager.instance.RemoveGameObject(pooName, gameObject);
    }

    public void RemoveFast()
    {
        GameObjectPoolManager.instance.RemoveGameObject(pooName, gameObject);
    }
    public void RemoveSlow()
    {
        StartCoroutine(RemoveSlowIE());
        IEnumerator RemoveSlowIE()
        {
            yield return new WaitForSeconds(.3f);
            GameObjectPoolManager.instance.RemoveGameObject(pooName, gameObject);
        }
    }

}
