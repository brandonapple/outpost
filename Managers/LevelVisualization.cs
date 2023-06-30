using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelVisualization : MonoBehaviour
{
    public int waveCountMax;
    public List<string> waveContents;
    public List<Monster> monsterList;
    [ContextMenu("get waves")]
    public void GetWaveContents()
    {

        foreach (Monster monster in monsterList)
        {
            DestroyImmediate(monster.gameObject);

        }
        monsterList = new List<Monster>();

        waveContents = new List<string>();
        LevelManager.instance = FindObjectOfType<LevelManager>();
        for (int i = 0; i < waveCountMax; i++)
        {
            string _string= LevelManager.instance.GetCurrentLevelContent(i);
            waveContents.Add(_string);
        }


        for (int i = 0; i < waveContents.Count; i++)
        {
            string currentWave = waveContents[i];
            string[] groupDatas = currentWave.Split(char.Parse(","));


            float xPos = 0;
            for (int j = 0; j < groupDatas.Length; j++)
            {
                string [] _strings = groupDatas[j].Split(char.Parse("_"));
                string monsterName = _strings[0];
                string countString = _strings[1];
                int count = int.Parse(countString);
                Debug.Log(monsterName + count);


                for (int k = 0; k < count; k++)
                {
                    Monster monster = Instantiate(Resources.Load<Monster>("Monsters/" + monsterName));
                    monsterList.Add(monster);
                    monster.transform.parent = transform;
                    monster.transform.localPosition = new Vector3(xPos, -i *2);
                    xPos += .3f;
                }
                xPos += .35f;

            }
        }

    }

    [ContextMenu("empty monsters")]
    public void EmptyMonsters()
    {
        foreach (Monster monster in monsterList)
        {
            DestroyImmediate(monster.gameObject);
        }
        monsterList = new List<Monster>();
    }

    //private void OnDrawGizmos()
    //{
    //    for (int i = 0; i < waveContents.Count; i++)
    //    {
    //        Handles.Label(transform.position+new Vector3(-.25f,i*-2,0),i.ToString());
    //    }
    //}
}
