using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGameObjectPool 
{
    protected Queue m_queue;
    protected int m_maxCount;
    protected GameObject m_prefab;
    protected Transform m_trans;
    protected string m_poolName;
    protected const int m_defaultMaxCount = 500;

    public BaseGameObjectPool()
    {
        m_maxCount = m_defaultMaxCount;
        m_queue = new Queue();
    }

    public virtual void Init(string poolName,Transform trans)
    {
        m_poolName = poolName;
        m_trans = trans;
    }

    public GameObject prefab
    {
        set
        {
            m_prefab = value;
        }
    }
    public int maxCount
    {
        set
        {
            m_maxCount = value;
        }
    }
   
    public virtual GameObject Get(Vector3 postion,float lifetime)
    {
        if (lifetime<0)
        {
            return null;
        }
        GameObject returnObj;
        if (m_queue.Count>0)
        {
            returnObj = (GameObject)m_queue.Dequeue();
        }
        else
        {
            returnObj = GameObject.Instantiate(m_prefab) as GameObject;
            returnObj.transform.SetParent(m_trans);
            returnObj.SetActive(false);
        }

        GameObjectPoolInfo info = returnObj.GetComponent<GameObjectPoolInfo>();
        if (info==null)
        {
            info = returnObj.AddComponent<GameObjectPoolInfo>();
        }
        info.pooName = m_poolName;
        if (lifetime>0)
        {
            info.lifetime = lifetime;
        }
        returnObj.transform.position = postion;
        returnObj.SetActive(true);
        return returnObj;

    }

    public virtual void Remove(GameObject obj)
    {
        if (m_queue.Contains(obj))
        {
            return;
        }
        if (m_queue.Count>m_maxCount)
        {
            GameObject.Destroy(obj);
        }
        else
        {
            m_queue.Enqueue(obj);
            obj.SetActive(false);
        }
    }

    public virtual void Destroy()
    {
        m_queue.Clear();
    }
}
