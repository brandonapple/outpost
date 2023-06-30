using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TalentPanel : MonoBehaviour
{
    public static TalentPanel instance;

    public List<TalentContainer> talentContainers;
    public List<TalentContainer> rootTalentContainerList;

    public float containerHorizontalDistance;
    public float containerVerticalDistance;

    public List<Image> lines;
    public Image linePrefab;

    public int unlockPointCount;
   // public int pointsToUseCount;
    public int pointCountTotal;
    public int pointAdditionalCount;

    public Text talentPointCountText;

    public Color gotColor;
    public Color notGotUnlockedColor;
    public Color notGotLockedColor;

    private void Awake()
    {
        instance = this;
        talentContainers = new List<TalentContainer>(GetComponentsInChildren<TalentContainer>());
    }
    public void Start()
    {
        CheckAllContainersState();
        ShowAllContainersState();
        ShowTalentPointCountText();
    }

    [ContextMenu("check containers pos")]
    public void CheckContainersPos()
    {
        foreach (Image line in lines)
        {
            if (line)
            {
                DestroyImmediate(line.gameObject);
            }
        }
        lines = new List<Image>();
        linePrefab.gameObject.SetActive(true);

        for (int i = 0; i < rootTalentContainerList.Count; i++)
        {
            List<TalentContainer> nextTalentContainers = new List<TalentContainer>();
            nextTalentContainers.Add(rootTalentContainerList[i]);
            do
            {
                List<TalentContainer> newNextTalentContainers = new List<TalentContainer>();

                foreach (TalentContainer container in nextTalentContainers)
                {
                    for (int j = 0; j < container.nextTalentContainers.Length; j++)
                    {
                        TalentContainer talentContainerB = container.nextTalentContainers[j];
                        if (!newNextTalentContainers.Contains(talentContainerB))
                        {
                            newNextTalentContainers.Add(talentContainerB);
                            talentContainerB.transform.position = container.transform.position + new Vector3(j*containerHorizontalDistance, containerVerticalDistance, 0);

                            Image line = Instantiate(linePrefab, linePrefab.transform.parent);
                            line.transform.position = (container.transform.position + talentContainerB.transform.position)/2;
                            float distance = Vector3.Distance(container.transform.position, talentContainerB.transform.position);
                            line.GetComponent<RectTransform>().sizeDelta = new Vector2(distance, 15);
                            Vector3 dirUp = Vector3.Cross(Vector3.forward, talentContainerB.transform.position - container.transform.position);
                            line.transform.rotation = Quaternion.LookRotation(Vector3.forward, dirUp);
                            lines.Add(line);

                        }
                    }
                }
                nextTalentContainers = newNextTalentContainers;

            } while (nextTalentContainers.Count>0);

        }
        linePrefab.gameObject.SetActive(false);
     

    }

    [ContextMenu("show all containers state")]
    public void ShowAllContainersState()
    {
        foreach (TalentContainer container in talentContainers)
        {
            container.ShowState();
        }
    }

    [ContextMenu("check all containers state")]
    public void CheckAllContainersState()
    {
        foreach (TalentContainer container in rootTalentContainerList)
        {
            container.unlocked = true;
        }

        foreach (TalentContainer container in rootTalentContainerList)
        {
            List<TalentContainer> nextContainers =new List<TalentContainer>();
            nextContainers.Add(container);

            do
            {
                List<TalentContainer> newNextContainers = new List<TalentContainer>();
                foreach (TalentContainer containerB in nextContainers)
                {
                    foreach (TalentContainer containerC in containerB.nextTalentContainers)
                    {
                        if (!newNextContainers.Contains(containerC))
                        {
                            newNextContainers.Add(containerC);
                            containerC.unlocked = containerB.got;

                        }
                    }
                }
                nextContainers = newNextContainers;

            } while (nextContainers.Count>0);
        }

    }

    public void ChooseTalentContainer(TalentContainer talentContainer)
    {
        if (unlockPointCount < pointCountTotal )
        {
            unlockPointCount++;
            talentContainer.UnlockedContainer();

            TalentManager.instance.GotTalent(talentContainer.talentName);

            Start();
            ShowTalentPointCountText();


            TalentButtonPanel.instance.CheckIfToUseTalentPoints();
            AudioManager.PlayClip("talentGot");
        }
    }

    public void ShowTalentPointCountText()
    {
        talentPointCountText.text ="可用天赋点数："+pointsToUseCount.ToString();
    }

    public void CloseButtonClick()
    {
        transform.DOMoveY(2000, .25f).SetUpdate(true);
        MainCanvas.instance.ShowBasePanel();
        AbilityManager.instance.ShowPanel();
        GameSpeedManager.instance.GameUnPause();
        AudioManager.PlayClip("talentPanelHide");
    }
    public void ShowPanel()
    {
        transform.DOMoveY(540, .25f).SetUpdate(true);
    }

    public void HidePanelFast()
    {
        transform.localPosition = new Vector3(0, 2000, 0);
    }

    public void ResetAllTalentContainers()
    {
        foreach (TalentContainer container in talentContainers)
        {
            container.got = false;
            container.unlocked = false;
        }

        CheckAllContainersState();
        ShowAllContainersState();
    }

    public void ResetTalentPointsButton()
    {
        unlockPointCount = 0;
        ShowTalentPointCountText();

        TalentManager.instance.ResetTalents();
        ResetAllTalentContainers();
        AbilityManager.instance.EmptyAllAbilities();
        DevicesManager.instance.SwitchOffAllDevices();
    }

    public void WaveCleanCheckTalentPoints()
    {
        int waveIndex = MonsterManager.instance.waveIndexReally;
        pointCountTotal = waveIndex / 10 + pointAdditionalCount;

        TalentButtonPanel.instance.CheckIfToUseTalentPoints();
       // Debug.Log("wave clean check");
        ShowTalentPointCountText();
    }
    public int pointsToUseCount
    {
        get
        {
            return pointCountTotal - unlockPointCount;
        }
    }
}
