using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSingle : MonoBehaviour
{
    public enum BuffName
    {
        idle,aiLearning,engineOil,gasoline,goldenBullet,hourglass,loneWolf,partner,rangeAmplifier,showCase
            ,superBattery
    }

    public BuffName thisBuffName = BuffName.idle;
    public TextMesh textMesh;

    public void LoadData()
    {
        switch (thisBuffName)
        {
            case BuffName.idle:
                break;
            case BuffName.aiLearning:
               
                if (GetComponentInParent<BuffVisualization>().GetComponentInParent<GunController>())
                {
                    GetText(RelicManager.instance.gunBulletAddValue);
                }
                else if (GetComponentInParent<BuffVisualization>().GetComponentInParent<SniperController>())
                {
                    GetText(RelicManager.instance.sniperBulletAddValue);
                }
                else if(GetComponentInParent<BuffVisualization>().GetComponentInParent<GatlingController>())
                {
                    GetText(RelicManager.instance.gatlingBulletAddValue);
                }


                break;
            case BuffName.engineOil:
                GunController gunController = GetComponentInParent<GunController>();
                SniperController sniperController = GetComponentInParent<SniperController>();
                GatlingController gatlingController = GetComponentInParent<GatlingController>();
                if (gunController)
                {
                   GetText(gunController.attackSpeed);
                }
                if (sniperController)
                {
                    GetText(sniperController.attackSpeed);
                }
                if (gatlingController)
                {
                    GetText(gatlingController.attackSpeed);
                }

                SetTextColor(Color.yellow);
                break;
            case BuffName.gasoline:
                GetText(RelicManager.instance.gasolineBurnTimeMultiplier,true);
                SetTextColor(Color.yellow);
                break;
            case BuffName.goldenBullet:
                GetText(RelicManager.instance.goldenBulletDamageMultiplier,true);
                break;
            case BuffName.hourglass:
                GetText(RelicManager.instance.hourGlassCDSpeedMultiplier,true);
                SetTextColor(Color.yellow);
                break;

            case BuffName.loneWolf:
                GetText(WeaponsManager.instance.loneWolfWeaponDamageMultiplier,true);
                break;

            case BuffName.partner:
                GetText(WeaponsManager.instance.partnerWeaponDamageMultiplier,true);
                break;

            case BuffName.rangeAmplifier:
                GetText(RelicManager.instance.rangeAmplifierMutiplier,true);
                SetTextColor(Color.blue);
                break;
            case BuffName.showCase:
                GetText(RelicManager.instance.showCaseRelicsCountDamageMultiplier,true);
                break;

            case BuffName.superBattery:
                GetText(RelicManager.instance.electricDamageMultiplier,true);
                break;
            default:
                break;
        }
    }

    void GetText(float value)
    {
        //textMesh.text = value.ToString();
        GetText(value, false);
    }

    void GetText(float value,bool percentMode)
    {
        string _valueString = null;
        if (percentMode)
        {
            _valueString ="×"+ (value * 100).ToString() + "%";
        }
        else
        {
            _valueString ="+"+value.ToString("#0.00");
        }


        textMesh.text = _valueString;

    }
    void SetTextColor(Color _color)
    {
        textMesh.color = _color;
    }
}
