using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WaveDataSettleMentContainer : MonoBehaviour
{
    public List<Sprite> monsterSpriteList;
    public int diamondFromMineCount;
    public int goldFromMineCount;
    public int goldFromMonsterCount;
    public int waveIndex;

    public Image monsterIconPrefab;
    public Text diamondFromMineCountText;
    public Text goldFromMindCountText;
    public Text goldFromMonsterCountText;
    public Text waveIndexText;
    private void Start()
    {
        monsterSpriteList = new List<Sprite>();
    }
    public void AddMonsterSprite(Sprite sprite)
    {
        monsterSpriteList.Add(sprite);
    }

    public void UpdateMonsterSpriters()
    {
        monsterIconPrefab.gameObject.SetActive(true);
        for (int i = 0; i < monsterSpriteList.Count; i++)
        {
            Image monsterIcon = Instantiate(monsterIconPrefab, monsterIconPrefab.transform.parent);

           // monsterIcon.transform.localPosition = Vector3.zero;
            monsterIcon.sprite = monsterSpriteList[i];
            monsterIcon.transform.localPosition += Vector3.right * 20f * i;
        }
        monsterIconPrefab.gameObject.SetActive(false);
    }

    public void UpdateTexts()
    {
        diamondFromMineCountText.text = diamondFromMineCount.ToString();
        goldFromMindCountText.text = goldFromMineCount.ToString();
        goldFromMonsterCountText.text = goldFromMonsterCount.ToString();
        waveIndexText.text = waveIndex.ToString();
    }

}
