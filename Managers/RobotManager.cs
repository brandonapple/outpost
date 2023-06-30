using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotManager : MonoBehaviour
{
    public static RobotManager instance;
    public List<Robot> goldMiningRobotList;
    public List<Robot> diamondMiningRobotList;

    private void Awake()
    {
        instance = this;
        goldMiningRobotList = new List<Robot>();
        diamondMiningRobotList = new List<Robot>();
    }
    public void Start()
    {
        GetRobots();
        SetRobotsReadyTime();
    }
    public void GetRobots()
    {
        if (DataManager.instance.miningModeIndex==1)
        {
            foreach (Robot robot in goldMiningRobotList)
            {
                if (robot) Destroy(robot.gameObject);
            }
            foreach (Robot robot in diamondMiningRobotList)
            {
                if (robot) Destroy(robot.gameObject);
            }
            return;
        }


        int goldMiningRobotCount =(int)DataManager.instance.getCurrentValueByString("miningGoldRobot");

        if (goldMiningRobotList.Count < goldMiningRobotCount)
        {
            int additionalCount = goldMiningRobotCount - goldMiningRobotList.Count;
            for (int i = 0; i < additionalCount; i++)
            {
                AddGoldMiningRobot();
            }

            void AddGoldMiningRobot()
            {
                Robot robot = Instantiate(Resources.Load<Robot>("Robots/RobotGold"), transform);
                robot.transform.localPosition = Vector3.zero;
                goldMiningRobotList.Add(robot);

            }
        }
       

        int diamondRobotCount =(int)DataManager.instance.getCurrentValueByString("miningDiamondRobot");
        if (diamondMiningRobotList.Count< diamondRobotCount)
        {
            int additionalCount = diamondRobotCount - diamondMiningRobotList.Count;
            for (int i = 0; i < additionalCount; i++)
            {
                AddDiamondRobot();   
            }
            void AddDiamondRobot()
            {
                Robot robot = Instantiate(Resources.Load<Robot>("Robots/RobotDiamond"), transform);
                robot.transform.localPosition = Vector3.zero;
                diamondMiningRobotList.Add(robot);
            }
        }

        

    }
    public void UpdateRobotData()
    {
        foreach (Robot robot in goldMiningRobotList)
        {
            robot.GetData();
        }
        foreach (Robot robot in diamondMiningRobotList)
        {
            robot.GetData();
        }
    }
    public void SetRobotsReadyTime()
    {
        for (int i = 0; i < goldMiningRobotList.Count; i++)
        {
            float timeInverval = 2f / (goldMiningRobotList.Count+1);
            goldMiningRobotList[i].SetReadyTime(i * timeInverval);
        }
        for (int i = 0; i < diamondMiningRobotList.Count; i++)
        {
            float timeInterval = 2f / (diamondMiningRobotList.Count + 1);
            diamondMiningRobotList[i].SetReadyTime(i * timeInterval);
        }
    }

    public void EmptyAllRobots()
    {
        foreach (Robot robot in goldMiningRobotList)
        {
            Destroy(robot.gameObject);
        }
        foreach (Robot robot in diamondMiningRobotList)
        {
            Destroy(robot.gameObject);
        }
        goldMiningRobotList = new List<Robot>();
        diamondMiningRobotList = new List<Robot>();
    }

    public void CallBackRobots()
    {
        if (DataManager.miningIgnoreMonsterUnlocked) return;
        if (TalentManager.instance.miningIgnoreMonstersUnlocked) return;


        Robot[] robots = FindObjectsOfType<Robot>();
        foreach (Robot robot in robots)
        {
            robot.BackToBaseFast();
        }
    }
}
