using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class SettleMentPanel : MonoBehaviour
{
    public static SettleMentPanel instance;

    public WaveDataSettleMentContainer waveDataSettleMentContainerPrefab;
    public List<WaveDataSettleMentContainer> containers;

    public Text titleText;
    public bool showing;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        containers = new List<WaveDataSettleMentContainer>();
        Hide();
    }

    public IEnumerator UpdateWaveDataSettleMentContainers()
    {
        yield return new WaitForSeconds(1);

        MainCanvas.instance.HideBasePanel();

        waveDataSettleMentContainerPrefab.gameObject.SetActive(false);
        List<WaveDataSettlement> waveDataSettleMentList = MonsterManager.instance.waveDataSettlementList;


       // titleText.text = "到达第" + waveDataSettleMentList.Count.ToString() + "波 ! ! ";
        titleText.text =LanguageManager.GetText("waveReached") 
            + waveDataSettleMentList.Count.ToString() + LanguageManager.GetText("wave");


        Show();
        yield return new WaitForSeconds(.25f);

        for (int i = 0; i < waveDataSettleMentList.Count -1; i++)
        {
            WaveDataSettleMentContainer waveDataSettleMentContainer = Instantiate(waveDataSettleMentContainerPrefab, waveDataSettleMentContainerPrefab.transform.parent);
            waveDataSettleMentContainer.gameObject.SetActive(true);
            waveDataSettleMentContainer.transform.localPosition += Vector3.down * 90 *i;

            waveDataSettleMentContainer.transform.localScale = Vector3.zero;
            waveDataSettleMentContainer.transform.DOScale(1, .15f);

            containers.Add(waveDataSettleMentContainer);


            waveDataSettleMentContainer.diamondFromMineCount = waveDataSettleMentList[i].diamondFromMine;
            waveDataSettleMentContainer.goldFromMineCount = waveDataSettleMentList[i].goldFromMine;
            waveDataSettleMentContainer.goldFromMonsterCount = waveDataSettleMentList[i].goldFromMonster;
            waveDataSettleMentContainer.waveIndex = i + 1;

            MonsterLevel monsterLevel = waveDataSettleMentList[i].monsterLevel;
            if (monsterLevel != null)
            {
                MonsterGroup[] monsterGroups = monsterLevel.monsterGroups;
                if (monsterGroups != null)
                {
                    foreach (MonsterGroup monsterGroup in monsterGroups)
                    {
                        int count = monsterGroup.monsterCount;
                        Sprite sprite = monsterGroup.monsterPrefab.GetComponentInChildren<SpriteRenderer>().sprite;

                        for (int j = 0; j < count; j++)
                        {
                            waveDataSettleMentContainer.AddMonsterSprite(sprite);
                        }
                    }
                }
            }
           
           
          

            waveDataSettleMentContainer.UpdateTexts();
            waveDataSettleMentContainer.UpdateMonsterSpriters();
            yield return new WaitForSeconds(.25f);

        }
        waveDataSettleMentContainerPrefab.gameObject.SetActive(false);
    }

    public void Hide()
    {
        transform.localScale = Vector3.zero;
        showing = false;
    }
    public void Show()
    {
        transform.DOScale(1, .25f);
        showing = true;
    }

    public void ShowSettleMent()
    {
        StartCoroutine(UpdateWaveDataSettleMentContainers());


    }

    public void CloseButton()
    {
        Hide();
        foreach (WaveDataSettleMentContainer container in containers)
        {
            if (container.gameObject != null)
            {
                Destroy(container.gameObject);
            }
        }
        containers = new List<WaveDataSettleMentContainer>();

        if (GameManager.instance.thisGameState == GameManager.GameState.gameOver)
        {
            GameOverPanel.instance.Show();
        }

        MainCanvas.instance.ShowBasePanel();
    }
}
