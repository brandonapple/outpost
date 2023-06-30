using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicsDisplay : MonoBehaviour
{
    public static RelicsDisplay instance;

    public RelicShower piggyBankIcon;
    public RelicShower compensationIcon;
    public RelicShower fundsTransferIcon;
    public RelicShower overTimePayIcon;
    public RelicShower oldDiamondIcon;
    public RelicShower texRefundIcon;
    private void Awake()
    {
        instance = this;
    }

    public void GetIcons()
    {
        List<RelicShower> relicShowerList = RelicManager.instance.relicShowerList;

        foreach (RelicShower relicShower in relicShowerList)
        {
            if (relicShower.relicName == "piggyBank")
            {
                piggyBankIcon = relicShower;
            }
            else if (relicShower.relicName == "compensation")
            {
                compensationIcon = relicShower;
            }
            else if (relicShower.relicName == "fundsTransfer")
            {
                fundsTransferIcon = relicShower;
            }
            else if (relicShower.relicName == "overTimePay")
            {
                overTimePayIcon = relicShower;
            }
            else if (relicShower.relicName == "oldDiamond")
            {
                oldDiamondIcon = relicShower;
            }
            else if (relicShower.relicName == "texRefund")
            {
                texRefundIcon = relicShower;
            }
        }


    }
}
