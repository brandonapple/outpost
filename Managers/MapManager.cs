using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public enum MapName {planetA,planetB };
    public static MapName thisMapName;

    public SpriteRenderer desertMap, swampMap;
    public SpriteRenderer mapAMask, mapBMask;

    public Mine diamondMine, goldMine;
   
    public void Start()
    {
        GetMapName();
        LoadMap();
    }

    public void LoadMap()
    {
        switch (DataManager.instance.mapIndex)
        {
            case 0:
                desertMap.gameObject.SetActive(true);
                swampMap.gameObject.SetActive(false);

                mapAMask.gameObject.SetActive(true);
                mapBMask.gameObject.SetActive(false);
                break;
            case 1:
                desertMap.gameObject.SetActive(false);
                swampMap.gameObject.SetActive(true);

                mapAMask.gameObject.SetActive(false);
                mapBMask.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }
   
    public void GetMapName()
    {
        if (DataManager.instance.mapIndex==0)
        {
            thisMapName = MapName.planetA;
        }
        else
        {
            thisMapName = MapName.planetB;
        }
    }
}
