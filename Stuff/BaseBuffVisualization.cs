using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBuffVisualization : MonoBehaviour
{
    public static BaseBuffVisualization instance;
    public List<BaseBuffSingle> baseBuffSingleList;

    public BaseBuffSingle baseBuffSinglePrefab;


    public Color damageColor;
    public Color speedColor;
    public Color critColor;
    public Color rangeColor;

    private void Awake()
    {
        instance = this;
        baseBuffSingleList = new List<BaseBuffSingle>();
        baseBuffSinglePrefab.gameObject.SetActive(false);
    }

    public void GetBuff(string _buffName)
    {
        bool existBuff = false;
        foreach (BaseBuffSingle baseBuffSingle in baseBuffSingleList)
        {
            if (baseBuffSingle.thisBaseBuffType.ToString() ==_buffName)
            {
                existBuff = true;
                break;
            }
        }
        if (!existBuff)
        {
            BaseBuffSingle baseBuffSingle = Instantiate(baseBuffSinglePrefab);
            baseBuffSingle.gameObject.SetActive(true);
            baseBuffSingle.transform.parent = baseBuffSinglePrefab.transform.parent;
            baseBuffSingleList.Add(baseBuffSingle);

            switch (_buffName)
            {
                case "riskForDamage":
                    baseBuffSingle.thisBaseBuffType = BaseBuffSingle.BaseBuffType.riskForDamage;
                    baseBuffSingle.SetColor(damageColor);
                    baseBuffSingle.SetText("+20%");
                    break;
                case "moneyForDamage":
                    baseBuffSingle.thisBaseBuffType = BaseBuffSingle.BaseBuffType.moneyDorDamage;
                    baseBuffSingle.SetColor(damageColor);
                    baseBuffSingle.SetText("+20%");
                    break;
                case "critChance":
                    baseBuffSingle.thisBaseBuffType = BaseBuffSingle.BaseBuffType.critChance;
                    baseBuffSingle.SetColor(critColor);
                    baseBuffSingle.SetText("+20%");
                    break;
                default:
                    break;
            }

            for (int i = 0; i < baseBuffSingleList.Count; i++)
            {
                baseBuffSingleList[i].transform.localPosition = new Vector3(0, i * .3f, 0);
            }

        }


    }

    public void EmptyBuffs()
    {
        foreach (BaseBuffSingle baseBuffSingle in baseBuffSingleList)
        {
            if (baseBuffSingle)
            {
                if (baseBuffSingle.gameObject)
                {
                    Destroy(baseBuffSingle.gameObject);
                }
            }
        }
        baseBuffSingleList = new List<BaseBuffSingle>();
    }
}
