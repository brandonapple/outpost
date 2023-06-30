using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WeaponDamageSettlementManager : MonoBehaviour
{
    public static WeaponDamageSettlementManager instance;

    public bool showing;
    public Image damagePanel;
    public Image hideIcon;

    public float gunDamageTotal;
    public float lightningDamageTotal;
    public float missileDamageTotal;
    public float laserDamageTotal;
    public float teslaTowerDamageTotal;

    public float flameThrowerDamageTotal;
    public float sniperDamageTotal;
    public float biologicalBombDamageTotal;
    public float shurikenBDamageTotal;
    public float magneticFieldDamageTotal;

    public float truckDamageTotal;
    public float freezingAirDamageTotal;
    public float ballLightningDamageTotal;
    public float energyBallDamageTotal;
    public float gatlingDamageTotal;

    public float maxDamageTotalValue;

    [Space(20)]
    public SingleWeaponDamageSlider gunDamageSlider;
    public SingleWeaponDamageSlider lightningDamageSlider;
    public SingleWeaponDamageSlider missileDamageSlider;
    public SingleWeaponDamageSlider laserDamageSlider;
    public SingleWeaponDamageSlider teslaTowerDamageSlider;

    public SingleWeaponDamageSlider flameThrowerDamageSlider;
    public SingleWeaponDamageSlider sniperDamageSlider;
    public SingleWeaponDamageSlider biologicalBombDamageSlider;
    public SingleWeaponDamageSlider shurikenBDamageSlider;
    public SingleWeaponDamageSlider magneticFieldDamageSlider;

    public SingleWeaponDamageSlider truckDamageSlider;
    public SingleWeaponDamageSlider freezingAirDamageSlider;
    public SingleWeaponDamageSlider ballLightningDamageSlider;
    public SingleWeaponDamageSlider energyBallDamageSlider;
    public SingleWeaponDamageSlider gatlingDamageSlider;


    public List<SingleWeaponDamageSlider> allSliders;
    public List<SingleWeaponDamageSlider> activeSliders;
    

    private void Awake()
    {
        instance = this;
        
    }
    private void Start()
    {
        showing = false;
        Hide();
        InitDamageSliders();
    }

    public void InitDamageSliders()
    {
        foreach (SingleWeaponDamageSlider slider in allSliders)
        {
            slider.gameObject.SetActive(false);
        }

        activeSliders = new List<SingleWeaponDamageSlider>();
        CheckSingleDamageSlider("Gun", gunDamageSlider);
        CheckSingleDamageSlider("Lightning", lightningDamageSlider);
        CheckSingleDamageSlider("Missile", missileDamageSlider);
        CheckSingleDamageSlider("Laser", laserDamageSlider);
        CheckSingleDamageSlider("TeslaTower", teslaTowerDamageSlider);
        CheckSingleDamageSlider("FlameThrower", flameThrowerDamageSlider);
        CheckSingleDamageSlider("Sniper", sniperDamageSlider);
        CheckSingleDamageSlider("BiologicalBomb", biologicalBombDamageSlider);
        CheckSingleDamageSlider("ShurikenB", shurikenBDamageSlider);
        CheckSingleDamageSlider("MagneticField", magneticFieldDamageSlider);
        CheckSingleDamageSlider("Truck", truckDamageSlider);
        CheckSingleDamageSlider("FreezingAir", freezingAirDamageSlider);
        CheckSingleDamageSlider("BallLightning", ballLightningDamageSlider);
        CheckSingleDamageSlider("EnergyBall", energyBallDamageSlider);
        CheckSingleDamageSlider("Gatling", gatlingDamageSlider);


        for (int i = 0; i < activeSliders.Count; i++)
        {
            activeSliders[i].transform.localPosition = new Vector3(0,160+ i * -50, 0);
        }

    }
    void CheckSingleDamageSlider(string weaponName,SingleWeaponDamageSlider slider)
    {
        if (WeaponDataManager.instance.GetWeaponOnOffStateByString(weaponName))
        {
            slider.gameObject.SetActive(true);
            activeSliders.Add(slider);
        }
    }

    void UpdateDamageSliders()
    {
        GetMaxDamageValue();
        float maxDamageTotalValueAddTen = maxDamageTotalValue + 10;

        if (gunDamageSlider)
        {
            gunDamageSlider.SetSlider(gunDamageTotal / (maxDamageTotalValue + 10));
            gunDamageSlider.SetValueText(gunDamageTotal);
        }

        if (lightningDamageSlider)
        {
            lightningDamageSlider.SetSlider(lightningDamageTotal / (maxDamageTotalValue + 10));
            lightningDamageSlider.SetValueText(lightningDamageTotal);
        }


        if (missileDamageSlider)
        {
            missileDamageSlider.SetSlider(missileDamageTotal / (maxDamageTotalValue + 10));
            missileDamageSlider.SetValueText(missileDamageTotal);
        }

        if (laserDamageSlider)
        {
            laserDamageSlider.SetSlider(laserDamageTotal / (maxDamageTotalValue + 10));
            laserDamageSlider.SetValueText(laserDamageTotal);
        }

        if (teslaTowerDamageSlider)
        {
            teslaTowerDamageSlider.SetSlider(teslaTowerDamageTotal / (maxDamageTotalValue + 10));
            teslaTowerDamageSlider.SetValueText(teslaTowerDamageTotal);
        }

        if (flameThrowerDamageSlider)
        {
            flameThrowerDamageSlider.SetSlider(flameThrowerDamageTotal / (maxDamageTotalValue + 10));
            flameThrowerDamageSlider.SetValueText(flameThrowerDamageTotal);
        }

        if (sniperDamageSlider)
        {
            sniperDamageSlider.SetSlider(sniperDamageTotal / (maxDamageTotalValue + 10));
            sniperDamageSlider.SetValueText(sniperDamageTotal);
        }

        if (biologicalBombDamageSlider)
        {
            biologicalBombDamageSlider.SetSlider(biologicalBombDamageTotal / (maxDamageTotalValue + 10));
            biologicalBombDamageSlider.SetValueText(biologicalBombDamageTotal);
        }

        if (shurikenBDamageSlider)
        {
            shurikenBDamageSlider.SetSlider(shurikenBDamageTotal / (maxDamageTotalValue + 10));
            shurikenBDamageSlider.SetValueText(shurikenBDamageTotal);
        }

        if (magneticFieldDamageSlider)
        {
            magneticFieldDamageSlider.SetSlider(magneticFieldDamageTotal / (maxDamageTotalValue + 10));
            magneticFieldDamageSlider.SetValueText(magneticFieldDamageTotal);
        }



        if (truckDamageSlider)
        {
            truckDamageSlider.SetSlider(truckDamageTotal / maxDamageTotalValueAddTen);
            truckDamageSlider.SetValueText(truckDamageTotal);
        }
        if (freezingAirDamageSlider)
        {
            freezingAirDamageSlider.SetSlider(freezingAirDamageTotal / maxDamageTotalValueAddTen);
            freezingAirDamageSlider.SetValueText(freezingAirDamageTotal);
        }
        if (ballLightningDamageSlider)
        {
            ballLightningDamageSlider.SetSlider(ballLightningDamageTotal / maxDamageTotalValueAddTen);
            ballLightningDamageSlider.SetValueText(ballLightningDamageTotal);
        }
        if (energyBallDamageSlider)
        {
            energyBallDamageSlider.SetSlider(energyBallDamageTotal / maxDamageTotalValueAddTen);
            energyBallDamageSlider.SetValueText(energyBallDamageTotal);
        }
        if (gatlingDamageSlider)
        {
            gatlingDamageSlider.SetSlider(gatlingDamageTotal / maxDamageTotalValueAddTen);
            gatlingDamageSlider.SetValueText(gatlingDamageTotal);
        }

    }
    float frameIndex;
    public void ResetDamageMaxValue()
    {
        maxDamageTotalValue = 1;


        gunDamageTotal =0;
        lightningDamageTotal = 0;
        missileDamageTotal = 0;
        laserDamageTotal = 0;
        teslaTowerDamageTotal = 0;

        flameThrowerDamageTotal = 0;
        sniperDamageTotal = 0;
        biologicalBombDamageTotal = 0;
        shurikenBDamageTotal = 0;
        magneticFieldDamageTotal = 0;

        truckDamageTotal = 0;
        freezingAirDamageTotal = 0;
        ballLightningDamageTotal = 0;
        energyBallDamageTotal = 0;
        gatlingDamageTotal = 0;
    }
    private void Update()
    {
        frameIndex++;
        if (frameIndex>20)
        {
            frameIndex = 0;
            UpdateDamageSliders();
        }
    }

    public void ShowOrHideButtonClick()
    {
        showing = !showing;
        if (showing)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    void Show()
    {
        damagePanel.gameObject.SetActive(true);
        hideIcon.gameObject.SetActive(false);
        InitDamageSliders();
    }
    void Hide()
    {
        damagePanel.gameObject.SetActive(false);
        hideIcon.gameObject.SetActive(true);
    }


    public void GunDealDamage(float value)
    {
        gunDamageTotal += value;
    }
    public void LightningDealDamage(float value)
    {
        lightningDamageTotal += value;
    }
    public void MissileDealDamage(float value)
    {
        missileDamageTotal += value;
    }
    public void LaserDealDamage(float value)
    {
        laserDamageTotal += value;
    }
    public void TeslaTowerDealDamage(float value)
    {
        teslaTowerDamageTotal += value;
    }

    public void FlameThrowerDealDamage(float value)
    {
        flameThrowerDamageTotal += value;
    }
    public void SniperDealDamage(float value)
    {
        sniperDamageTotal += value;
    }
    public void BiologicalBombDealDamage(float value)
    {
        biologicalBombDamageTotal += value;
    }
    public void ShurikenBDealDamage(float value)
    {
        shurikenBDamageTotal += value;
    }
    public void MagneticFieldDealDamage(float value)
    {
        magneticFieldDamageTotal += value;
    }

    public void TruckDealDamage(float value)
    {
        truckDamageTotal += value;
    }
    public void FreezingAirDealDamage(float value)
    {
        freezingAirDamageTotal += value;
    }

    public void BallLightningDealDamage(float value)
    {
        ballLightningDamageTotal += value;
    }
    public void EnergyBallDealDamage(float value)
    {
        energyBallDamageTotal += value;
    }
    public void GatlingDealDamage(float value)
    {
        gatlingDamageTotal += value;
    }

    void GetMaxDamageValue()
    {
        if (gunDamageTotal > maxDamageTotalValue) maxDamageTotalValue = gunDamageTotal;
        if (lightningDamageTotal > maxDamageTotalValue) maxDamageTotalValue = lightningDamageTotal;
        if (missileDamageTotal > maxDamageTotalValue) maxDamageTotalValue = missileDamageTotal;
        if (laserDamageTotal > maxDamageTotalValue) maxDamageTotalValue = laserDamageTotal;
        if (teslaTowerDamageTotal > maxDamageTotalValue) maxDamageTotalValue = teslaTowerDamageTotal;

        if (flameThrowerDamageTotal > maxDamageTotalValue) maxDamageTotalValue = flameThrowerDamageTotal;
        if (sniperDamageTotal > maxDamageTotalValue) maxDamageTotalValue = sniperDamageTotal;
        if (biologicalBombDamageTotal > maxDamageTotalValue) maxDamageTotalValue = biologicalBombDamageTotal;
        if (shurikenBDamageTotal > maxDamageTotalValue) maxDamageTotalValue = shurikenBDamageTotal;
        if (magneticFieldDamageTotal > maxDamageTotalValue) maxDamageTotalValue = magneticFieldDamageTotal;

        if (truckDamageTotal > maxDamageTotalValue) maxDamageTotalValue = truckDamageTotal;
        if (freezingAirDamageTotal > maxDamageTotalValue) maxDamageTotalValue = freezingAirDamageTotal;
        if (ballLightningDamageTotal > maxDamageTotalValue) maxDamageTotalValue = ballLightningDamageTotal;
        if (energyBallDamageTotal > maxDamageTotalValue) maxDamageTotalValue = energyBallDamageTotal;
        if (gatlingDamageTotal > maxDamageTotalValue) maxDamageTotalValue = gatlingDamageTotal;





    }

    
}
