using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinesManager : MonoBehaviour
{
    public static MinesManager instance;

    public Mine[] mines;
    public MineDiamondAutoSpawner mineDiamondAutoSpawnerPrefab;

    public List<GameObject> spawnerList;
    private void Awake()
    {
        instance = this;
        mines = GetComponentsInChildren<Mine>();
    }
    private void Start()
    {
        GetMines();
    }

    public void GetMines()
    {
        foreach (GameObject spawner in spawnerList)
        {
            if (spawner)
            {
                Destroy(spawner.gameObject);
            }
        }
        spawnerList = new List<GameObject>();


        if (DataManager.instance.miningModeIndex==1)
        {
            foreach (Mine mine in mines)
            {
                mine.gameObject.SetActive(false);
            }
            GetDiamondAutoSpawners();
        }
        else 
        {
            foreach (Mine mine in mines)
            {
                mine.gameObject.SetActive(true);
            }
            mineDiamondAutoSpawnerPrefab.gameObject.SetActive(false);
        }
    }


    void GetDiamondAutoSpawners()
    {

        int spawnerCount = (int)DataManager.instance.getCurrentValueByString("diamondAutoSpawnerSpawnerCount");
        mineDiamondAutoSpawnerPrefab.gameObject.SetActive(true);

        float angle = Mathf.PI * 2 / spawnerCount;
        for (int i = 0; i < spawnerCount; i++)
        {
            Vector3 pos = new Vector3(Mathf.Cos(i * angle), 0, Mathf.Sin(i * angle)) * 2.65f;
            MineDiamondAutoSpawner spawner = Instantiate(mineDiamondAutoSpawnerPrefab);
            spawner.transform.position = pos;
            spawnerList.Add(spawner.gameObject);
        }
        mineDiamondAutoSpawnerPrefab.gameObject.SetActive(false);

    }

    public void DiamondSpawnersSpawnDiamond(int waveIndex)
    {
        foreach (GameObject spawner in spawnerList)
        {
            spawner.GetComponent<MineDiamondAutoSpawner>().SpawnDiamondByWaveIndex(waveIndex);
        }
    }
}
