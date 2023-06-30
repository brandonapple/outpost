using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineDiamondAutoSpawner : MonoBehaviour
{
    public void SpawnDiamondByWaveIndex(int waveIndex)
    {
        int baseDiamondCount =(int)DataManager.instance.getCurrentValueByString("diamondAutoSpawnerWaveDiamondBaseValue");


        int diamondCount = (int)((1 + .2f * waveIndex) * (1 + .2f * waveIndex)) +baseDiamondCount;

        AdditionalMoneyManager.instance.SpawnCoinsOrDiamonds(diamondCount, transform.position + Vector3.up, "diamond");

        //int a = diamondCount / 10;
        //int b = diamondCount - a * 10;
      
        //StartCoroutine(SpawnDianondIE());
        //IEnumerator SpawnDianondIE()
        //{
        //    for (int i = 0; i < a; i++)
        //    {
        //        DiamondFromMine diamondFromMine = Instantiate(Resources.Load<DiamondFromMine>("Prefab/DiamondFromMine"));
        //        diamondFromMine.transform.position = transform.position + Vector3.up;
        //        diamondFromMine.value = 10;
        //        yield return new WaitForSeconds(.1f);
        //    }
        //    for (int i = 0; i < diamondCount; i++)
        //    {
        //        DiamondFromMine diamondFromMine = Instantiate(Resources.Load<DiamondFromMine>("Prefab/DiamondFromMine"));
        //        diamondFromMine.transform.position = transform.position + Vector3.up;
        //        diamondFromMine.value = 1;
        //        yield return new WaitForSeconds(.1f);
        //    }
        //}
    }
}
