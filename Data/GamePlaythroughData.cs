using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class GamePlaythroughData 
{
    public int playThroughTime;
    public GamePlaythroughData()
    {
        playThroughTime = GamePlaythroughManager.instance.playThroughTime;
    }

}
