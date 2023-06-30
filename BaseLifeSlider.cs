using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BaseLifeSlider : MonoBehaviour
{
    public LineRenderer lifeBaseLine;
    public LineRenderer lifeCircleValueLine;
    public GameObject lifeSeparatorPrefab;
    public List<GameObject> separatorsList;

    public float radius;

    private void Awake()
    {
        separatorsList = new List<GameObject>();
    }
    private void Start()
    {
      //  StartCoroutine(Hide());
    }
    public void UpdateSlider(float _percent)
    {
       // transform.DOScale(1, .1f);
       // StopAllCoroutines();
       // if (gameObject.activeSelf)
       // {
       ////   StartCoroutine(Hide());
       // }
        SetLifeCircleLine(_percent);
        
    }
    public void SetSeparators(float lifeMax)
    {
        foreach (GameObject separator in separatorsList)
        {
            if (separator)
            {
                Destroy(separator);
            }
        }
        separatorsList = new List<GameObject>();


        foreach (GameObject separator in separatorsList)
        {
            Destroy(separator.gameObject);
        }
        lifeSeparatorPrefab.gameObject.SetActive(true);

        int separatorCount = Mathf.FloorToInt( lifeMax / 5);
        for (int i = 0; i < separatorCount; i++)
        {
            float posPercent = ((float)i + 1) * 5 / lifeMax;
            GameObject separator = Instantiate(lifeSeparatorPrefab);
            separator.transform.parent = transform;
          //  float f = Mathf.PI * (1 + posPercent);


            float angleA = (1.5f - .33f * .5f) * Mathf.PI;
            float angleB = (1.5f + .33f * .5f) * Mathf.PI;
            float f = Mathf.Lerp(angleA,angleB,(float) i / separatorCount);

            Vector3 pos = new Vector3(Mathf.Cos(f), 0, Mathf.Sin(f));
            separator.transform.localPosition = pos * radius;
            separatorsList.Add(separator);
            separator.transform.rotation = Quaternion.LookRotation(pos, Vector3.up);
        }
        separatorsList[0].gameObject.SetActive(false);
        lifeSeparatorPrefab.gameObject.SetActive(false);
    }

    [ContextMenu("set circle pos")]
    public void SetLifeCircleLine(float lifePercent)
    {
        int pointCount = Mathf.FloorToInt(30 * lifePercent);

        if (pointCount < 0) pointCount = 0;
        lifeCircleValueLine.positionCount = pointCount;
        for (int i = 0; i < pointCount; i++)
        {
            float f = (float)(i-15);
            f = (f / 99) * Mathf.PI ;
            f += Mathf.PI * 1.5f;

            Vector3 pos = new Vector3(Mathf.Cos(f), 0, Mathf.Sin(f)) * radius;
            lifeCircleValueLine.SetPosition(i, pos);
        }
    }

    [ContextMenu("set circle full ")]
    public void SetLifeCircleLineFull()
    {
        SetLifeCircleLine(1);
    }
    [ContextMenu("set life base line")]
    public void SetLifeBaseLine()
    {
        for (int i = 0; i < 30; i++)
        {
            float f = (float)(i-15);
            f = (f / 99) * Mathf.PI;
            f += Mathf.PI * 1.5f;

            Vector3 pos = new Vector3(Mathf.Cos(f), 0, Mathf.Sin(f)) * radius;
            lifeBaseLine.SetPosition(i, pos);
        }
    }
    
}
