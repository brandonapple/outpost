using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenEgg : MonoBehaviour
{
    public int roundLast;
    public TextMesh roundLastTextMesh;

    private void Start()
    {
        roundLast = 7;
        UpdateText();
    }
    public void WaveEnd()
    {
        roundLast--;
        if (roundLast<=0)
        {
            Destroy(gameObject);
            RandomRewardsManager.instance.DropRewardByValue(5,transform.position);
        }


        UpdateText();
    }

    void UpdateText()
    {
        roundLastTextMesh.text = roundLast.ToString();
    }
}
