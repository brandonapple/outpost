using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RelicResourcesManager : MonoBehaviour
{
    public static RelicResourcesManager instance;
    public RelicDataSO[] relicDataSOs;
    public List<RelicDataSO> relicDataSOsList;
    public List<RelicDataFake> relicDataFakeList;

    private void Awake()
    {
        instance = this;
    }
    public void Start()
    {
        CheckAndInitRelicDataSOs();
    }
    public void CheckAndInitRelicDataSOs()
    {
        CheckRelicDataSOs();
        InitRelicDataSOs();
    }
    public void CheckRelicDataSOs()
    {
        relicDataSOs = Resources.LoadAll<RelicDataSO>("RelicSOs");
        foreach (RelicDataSO relicDataSO in relicDataSOs)
        {
            relicDataSO.unlocked = HistoryManager.instance.GetRelicUnlockedStateByString(relicDataSO.name);
        }

    }
    public void InitRelicDataSOs()
    {
        relicDataSOs = Resources.LoadAll<RelicDataSO>("RelicSOs");
        relicDataSOsList = new List<RelicDataSO>();
        foreach (RelicDataSO relicDataSO in relicDataSOs)
        {
            if (relicDataSO.unlocked && relicDataSO.canGotInThisRound)
            {
                relicDataSOsList.Add(relicDataSO);
            }
        }




        relicDataFakeList = new List<RelicDataFake>();
        foreach (RelicDataSO so in relicDataSOsList)
        {
            for (int i = 0; i < 5; i++)
            {
                RelicDataFake relicDataFake = new RelicDataFake();
                relicDataFake.relicName = so.relicName;
                if (i == 4)
                {
                    if (so.canPlus)
                    {
                        relicDataFake.plus = true;
                    }
                }
                relicDataFakeList.Add(relicDataFake);
            }
        }
    }
   
    public void RemoveRelicFakeDataResourceByName(string _relicName,bool _plus)
    {
        RelicDataFake removeRelicDataFake = relicDataFakeList[0];
        foreach (RelicDataFake dataFake in relicDataFakeList)
        {
            if (dataFake.relicName == _relicName && dataFake.plus == _plus)
            {
                removeRelicDataFake = dataFake;
                break;
            }
        }
        relicDataFakeList.Remove(removeRelicDataFake);
    }

    public List<RelicDataFake> GetRandomRelicDataFakeResource(int count)
    {
        if (count>= relicDataSOsList.Count)
        {
            count = relicDataSOsList.Count;
        }


        List<RelicDataFake> list = new List<RelicDataFake>();
        do
        {
            RelicDataFake targetRelicDataFake = relicDataFakeList[Random.Range(0, relicDataFakeList.Count)];
            bool sameRelic = false;
            foreach (RelicDataFake relicDataFake in list)
            {
                if (targetRelicDataFake.relicName == relicDataFake.relicName)
                {
                    sameRelic = true;
                }
            }
            if (!sameRelic)
            {
                list.Add(targetRelicDataFake);
            }
        } while (list.Count<count);


        return list;
    }
}

[System.Serializable]
public struct RelicDataFake
{
    public string relicName;
    public bool plus;

}
