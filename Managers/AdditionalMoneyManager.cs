using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalMoneyManager : MonoBehaviour
{
    public static AdditionalMoneyManager instance;
    private void Awake()
    {
        instance = this;
    }
    public void PiggyBankDropCoins(float _coinsValue)
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(RelicsDisplay.instance.piggyBankIcon.transform.position);

        int temporaryValue =(int)_coinsValue;
        SpawnCoinsOrDiamonds(temporaryValue, pos, "coin");
    }
    public void CompensationDropCoins(float _coinsValue)
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(RelicsDisplay.instance.compensationIcon.transform.position);
        for (int i = 0; i < _coinsValue; i++)
        {
            Coin coin = GameObjectPoolManager.instance.coinPool.Get(pos, 5).GetComponent<Coin>();
            coin.DirToBase();
        }


    }
    public void FundsTransferDropDiamonds(float _coinsValue)
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(RelicsDisplay.instance.fundsTransferIcon.transform.position);
        int value = (int)_coinsValue;

        SpawnCoinsOrDiamonds(value, pos, "diamond");

    }
    public void OverTimePayDropCoins(float _coinsValue)
    {
        if (!RelicsDisplay.instance.overTimePayIcon) return;

        Vector3 pos = Camera.main.ScreenToWorldPoint(RelicsDisplay.instance.overTimePayIcon.transform.position);
        for (int i = 0; i < _coinsValue; i++)
        {
            Coin coin = GameObjectPoolManager.instance.coinPool.Get(pos, 5).GetComponent<Coin>();
            coin.DirToBase();
        }


    }
    public void OldDiamondsDropDiamonds(float _coinsValue)
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(RelicsDisplay.instance.oldDiamondIcon.transform.position);
        for (int i = 0; i < _coinsValue; i++)
        {
            Diamond diamond = GameObjectPoolManager.instance.diamondPool.Get(pos, 5).GetComponent<Diamond>();
            diamond.DirToBase();
        }
    }
    public void TexRefundDropCoins(float _coinsValue)
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(RelicsDisplay.instance.texRefundIcon.transform.position);
       
        for (int i = 0; i < _coinsValue; i++)
        {
            Coin coin = GameObjectPoolManager.instance.coinPool.Get(pos, 5).GetComponent<Coin>();
            coin.DirToBase();
        }
    }


    public void SpawnCoinsOrDiamonds(int count,Vector3 pos,string type)
    {
        if (count>999)
        {
            int countB = count - 999;
            switch (type)
            {
                case "coin":
                    CoinsManager.instance.AddMoney(countB);
                    break;
                case "diamond":
                    CoinsManager.instance.AddMoney(countB);
                    break;
                default:
                    break;
            }

            count = 999;
        }

        int a = count / 100;
        count -= a * 100;

        int b = count / 10;
        count -= b * 1;

        int c = count;

        for (int i = 0; i < a; i++)
        {
            switch (type)
            {
                case "coin":
                    Coin coin = GameObjectPoolManager.instance.coinPool.Get(pos, 5).GetComponent<Coin>();
                    coin.DirToBase();
                    coin.value = 100;
                    coin.transform.localScale = Vector3.one * 3;
                    break;
                case "diamond":
                    Diamond diamond = GameObjectPoolManager.instance.diamondPool.Get(pos, 5).GetComponent<Diamond>();
                    diamond.DirToBase();
                    diamond.value = 100;
                    diamond.transform.localScale = Vector3.one * 3;
                    break;
                default:
                    break;
            }
        }
        for (int i = 0; i < b; i++)
        {
            switch (type)
            {
                case "coin":
                    Coin coin = GameObjectPoolManager.instance.coinPool.Get(pos, 5).GetComponent<Coin>();
                    coin.DirToBase();
                    coin.value = 10;
                    coin.transform.localScale = Vector3.one * 2;
                    break;
                case "diamond":
                    Diamond diamond = GameObjectPoolManager.instance.diamondPool.Get(pos, 5).GetComponent<Diamond>();
                    diamond.DirToBase();
                    diamond.value = 10;
                    diamond.transform.localScale = Vector3.one * 2;
                    break;
                default:
                    break;
            }
        }
        for (int i = 0; i < c; i++)
        {
            switch (type)
            {
                case "coin":
                    Coin coin = GameObjectPoolManager.instance.coinPool.Get(pos, 5).GetComponent<Coin>();
                    coin.DirToBase();
                    coin.value = 1;
                    coin.transform.localScale = Vector3.one * 1;
                    break;
                case "diamond":
                    Diamond diamond = GameObjectPoolManager.instance.diamondPool.Get(pos, 5).GetComponent<Diamond>();
                    diamond.DirToBase();
                    diamond.value = 1;
                    diamond.transform.localScale = Vector3.one * 1;
                    break;
                default:
                    break;
            }
        }
    }
}
