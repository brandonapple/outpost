using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCollectorDevice : MonoBehaviour
{
    public static BodyCollectorDevice instance;
    public bool active;

    public float bodyPieceValueTotal;
    public float bodyPieceValueMax;

   // public GameObject valueRoot;

    public GameObject bodyLiquid;
    private void Awake()
    {
        instance = this;
    }

    public void DeviceSwitchOn()
    {
        active = true;
        bodyPieceValueTotal = 0;
        ShowValueContainer();
    }

    public void DeviceSwitchOff()
    {
        active = false;
        bodyPieceValueTotal = 0;
        ShowValueContainer();
    }


    public void AddBodyPieceValue()
    {
        bodyPieceValueTotal += 1;
        if (bodyPieceValueTotal>bodyPieceValueMax)
        {
            bodyPieceValueTotal = 0;
            Gain();
        }
        ShowValueContainer();
    }

    public void Gain()
    {
        AdditionalMoneyManager.instance.SpawnCoinsOrDiamonds(10,transform.position,"coin");
        AdditionalMoneyManager.instance.SpawnCoinsOrDiamonds(20, transform.position, "diamond");
    }

    public void ShowValueContainer()
    {
        float percent = bodyPieceValueTotal / bodyPieceValueMax;
       // valueRoot.transform.localScale = new Vector3(1, 1*percent, 1);

        Vector3 zeroPos = new Vector3(0, -1.25f, 0);
        Vector3 onePos = new Vector3(0, .8f, 0);
        Vector3 zeroScale = new Vector3(1, 0, 1);
        Vector3 onrScale = new Vector3(1, 1, 1);

        bodyLiquid.transform.localPosition = Vector3.Lerp(zeroPos, onePos, percent);
        bodyLiquid.transform.localScale = Vector3.Lerp(zeroScale, onrScale, percent);

    }

}
