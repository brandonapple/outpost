using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class RelicBootyPanel : MonoBehaviour
{
    public RelicBootyContainer relicBootyContainerPrefab;
    List<RelicBootyContainer> relicBootyContainerList;

    public float relicDistance;

    public Text talkContentText;

    public GameObject talkBoxRoot;
    public GameObject bootyPanel;


    [Space(20)]
    public GameObject adRefreshButton;

    public Button coinsRefreshButton;
    public Text coinsRefreshButtonPriceText;
    public int coinsRefreshTime;
    public int coinsRefreshPrice;


    public bool freeRefresh;
    public int choosenLastTime;

    private void Awake()
    {
        transform.DOScale(1, .25f).SetUpdate(true);
        transform.localPosition = new Vector3(Screen.width/2,Screen.height/2, 0);

        if (Application.platform != RuntimePlatform.Android)
        {
            adRefreshButton.gameObject.SetActive(false);
        }

        choosenLastTime = 1;
        if (TalentManager.instance.doubleSupplyBoxUnlocked)
        {
            choosenLastTime = 2;
        }
    }
    private IEnumerator Start()
    {
        GameSpeedManager.instance.GamePause();

        GetCoinsRefreshPrice();

        talkContentText.text = null;
        bootyPanel.transform.localScale = Vector3.zero;
        relicBootyContainerPrefab.gameObject.SetActive(false);
    
        bootyPanel.transform.DOScale(1, .25f).SetUpdate(true);
        yield return new WaitForSecondsRealtime(.25f);
       // RelicManager.instance.TemporaryAddOneLevel();
        SpawnRelicBootyContainers();
    }

    [ContextMenu("Spawn relic booty")]
    public void SpawnRelicBootyContainers()
    {
        relicBootyContainerPrefab.gameObject.SetActive(true);
        relicBootyContainerList = new List<RelicBootyContainer>();

        RelicDataSO[] relicDataSOs = Resources.LoadAll<RelicDataSO>("RelicSOs");
        int relicChooseCount =(int)DataManager.instance.getCurrentValueByString("baseRelicChooseCount");

        List<RelicDataSO> relicDataSOsList = new List<RelicDataSO>();
        List<RelicDataFake> relicDataFakes = RelicResourcesManager.instance.GetRandomRelicDataFakeResource(relicChooseCount);
      
        foreach (RelicDataFake dataFake in relicDataFakes)
        {
            foreach (RelicDataSO relicDataSO in relicDataSOs)
            {
                if (relicDataSO.relicName ==  dataFake.relicName)
                {
                    relicDataSOsList.Add(relicDataSO);
                    break;
                }
            }
        }

        for (int i = 0; i < relicDataSOsList.Count; i++)
        {
            RelicBootyContainer relicBootyContainer = Instantiate(relicBootyContainerPrefab,transform);
            relicBootyContainerList.Add(relicBootyContainer);
            relicBootyContainer.transform.localPosition = new Vector3((relicChooseCount -1)*-175 +i*350, 0, 0 );

            RelicDataSO relicDataSO;
            relicDataSO = relicDataSOsList[i];
            if (relicDataFakes[i].plus) relicBootyContainer.plus = true;

            relicBootyContainer.GetData(relicDataSO);

            relicBootyContainer.transform.localScale = Vector3.zero;
            relicBootyContainer.transform.DOScale(1, .25f).SetUpdate(true);
           
        }
        relicBootyContainerPrefab.gameObject.SetActive(false);


       

    }

    public void ChooseRelic(RelicBootyContainer relicBootyContainer)
    {
        if (choosenLastTime<=0)
        {
            return;
        }
        choosenLastTime--;
        if (choosenLastTime <= 0)
        {
            GameSpeedManager.instance.GameUnPause();
            transform.DOMoveY(-2000, .5f).SetUpdate(true);
            Destroy(gameObject, .5f);
            MainCanvas.instance.ShowBasePanel();
            RelicManager.instance.AddRelicWaveIndex();
        }



        if (relicBootyContainer != null)
        {
            relicBootyContainer.transform.parent = MainCanvas.instance.gameObject.transform;
            relicBootyContainer.Choosen();
            RelicManager.instance.GetRelic(relicBootyContainer.relicName);
            if (relicBootyContainer.plus)
            {
                RelicManager.instance.GetRelic(relicBootyContainer.relicName);
            }
            RelicResourcesManager.instance.RemoveRelicFakeDataResourceByName(relicBootyContainer.relicName, relicBootyContainer.plus);
        }

        
       

    }
  
    public void ADRefreshButton()
    {
        adRefreshButton.gameObject.SetActive(false);
        RefreshRelics();
        ADCanvas.instance.ShowFullScreenAD();
    }
    public void CoinsRefreshButton()
    {
        if (freeRefresh)
        {
            freeRefresh = false;
            RefreshRelics();
            GetCoinsRefreshPrice();
            return;
        }

        if (CoinsManager.instance.coinCount>coinsRefreshPrice)
        {
            CoinsManager.instance.SpentMoney(coinsRefreshPrice);
            coinsRefreshTime++;
            GetCoinsRefreshPrice();
            RefreshRelics();
        }
        else
        {
            Debug.Log("not enough coins");
        }
    }
    public void FreeRefreshButton()
    {
        RefreshRelics();
    }
    void GetCoinsRefreshPrice()
    {

        coinsRefreshPrice = 5 * Mathf.RoundToInt( Mathf.Pow(2, coinsRefreshTime));// 1 + (coinsRefreshTime +2) * (coinsRefreshTime+2);


        coinsRefreshButtonPriceText.text = coinsRefreshPrice.ToString();
        if (freeRefresh)
        {
            coinsRefreshPrice = 0;
            coinsRefreshButtonPriceText.text = "free";
        }
    }
    void RefreshRelics()
    {
        foreach (RelicBootyContainer container in relicBootyContainerList)
        {
            if (container.gameObject != null)
            {
                Destroy(container.gameObject);
            }
        }
        relicBootyContainerList = new List<RelicBootyContainer>();
        SpawnRelicBootyContainers();


        if (RelicManager.instance.anotherBottleCount>0)
        {
            float f = Random.value;
            if (f<RelicManager.instance.anotherBottleFreeChance)
            {
                freeRefresh = true;
                coinsRefreshButtonPriceText.text = "free";
            }
        }


        if (RelicManager.instance.overTimePayCount>0)
        {
            int value =(int) RelicManager.instance.overTimePayDiamondCount;
            // CoinsManager.instance.AddDiamond(value);
            AdditionalMoneyManager.instance.OverTimePayDropCoins(value);
        }

        GameManager.instance.ValuePlusOne("refreshTime");
    }
    public void CloseButtonClick()
    {
        ChooseRelic(null);
    }
}
