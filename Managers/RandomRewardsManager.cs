using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRewardsManager : MonoBehaviour
{
    public static RandomRewardsManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void DropRewardByValue(float value)
    {
        Vector3 pos = new Vector3(Random.value - .5f, 0, Random.value - .5f);
        pos = pos.normalized * 3;
        DropRewardByValue(value, pos);
    }
    public void DropRewardByValue(float value,Vector3 pos)
    {
        TreasureBox treasureBox = Instantiate(Resources.Load<TreasureBox>("Prefab/treasureBox"));
        treasureBox.treasureValue = value;
        treasureBox.transform.position = pos;
    }
}
