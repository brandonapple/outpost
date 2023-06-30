using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class LifeSlider : MonoBehaviour
{
    public SpriteRenderer separaterPrefab;
    public SpriteRenderer greenPart;
    public SpriteRenderer lifeSlow;

    List<GameObject> separaterList;

    private void Awake()
    {
        separaterList = new List<GameObject>();

        for (int i = 0; i < 11; i++)
        {
            SpriteRenderer separater = Instantiate(separaterPrefab);
            separater.transform.parent = separaterPrefab.transform.parent;
            separater.transform.localPosition = Vector3.zero;
            separater.gameObject.SetActive(false);
            separaterList.Add(separater.gameObject);
        }
    }

    [ContextMenu("get separaters")]
    public void GetSepataters(int _lifeMax)
    {
        greenPart.color = Color.green;
        int valuePerGrid = 5;
        if (_lifeMax>=50 && _lifeMax<500)
        {
            valuePerGrid = 50;
            greenPart.color = Color.yellow;
        }
        else if (_lifeMax>=500 && _lifeMax<5000)
        {
            valuePerGrid = 500;
            greenPart.color = Color.red;
        }
        else if (_lifeMax>=5000 && _lifeMax<50000)
        {
            valuePerGrid = 5000;
            greenPart.color = Color.gray;
        }
        else if (_lifeMax>=50000 && _lifeMax<500000)
        {
            valuePerGrid = 50000;
            greenPart.color = Color.blue;
        }
        else if (_lifeMax>=500000)
        {
            valuePerGrid = 500000;
            greenPart.color = Color.black;
        }


        int separaterCount = _lifeMax / valuePerGrid +1;

        if (separaterCount>=separaterList.Count)
        {
            separaterCount = separaterList.Count;
        }


        for (int i = 0; i < separaterCount; i++)
        {
            GameObject separater = separaterList[i];
            separater.gameObject.SetActive(true);
            separater.transform.localPosition = new Vector3(Mathf.Lerp(-.5f,.5f,(float)i *valuePerGrid / _lifeMax), 0, 0);
          
        }
        for (int i = separaterCount; i < 11; i++)
        {
            separaterList[i].gameObject.SetActive(false);
        }

    }

    public void UpdataValue(float _lifePercent)
    {
        float xPos = Mathf.Lerp(-.965f, 0, _lifePercent);
        xPos = Mathf.Clamp(xPos, -.965f, 0);
        greenPart.transform.localPosition = new Vector3(xPos, 0, 0);
        lifeSlow.transform.DOLocalMoveX(greenPart.transform.localPosition.x, 1f);

        if (_lifePercent<=0)
        {
            if (gameObject.activeSelf)
            {
                GetComponent<GameObjectPoolInfo>().RemoveSlow();

            }
        }
    }
   
    public void SetColor(Color _color)
    {
        greenPart.color = _color;
    }
}
