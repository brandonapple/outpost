using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager instance;
    public bool steamAchievementActive;
    private void Awake()
    {
        instance = this;
        if (Application.platform == RuntimePlatform.Android)
        {
            steamAchievementActive = false;
        }
    }


    public void ReachAchievement(SteamManager.AchievementType _type)
    {
        if (steamAchievementActive)
        {
            //SteamManager.Instance.ReachAchievement(_type);
            FindObjectOfType<SteamManager>().ReachAchievement(_type);
        }

    }
}
