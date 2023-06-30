using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class WeaponDamageSlider : MonoBehaviour
{
    //public Image spriteRenderer;
    public Image damageSliderImage;
    public float damageTotal;
    public Text damageTotalText;

    public static WeaponDamageSlider gunWeaponDamageSlider;
    public static WeaponDamageSlider missileWeaponDamageSlider;
    public static WeaponDamageSlider laserWeaponDamageSlider;
    public static WeaponDamageSlider lightningWeaponDamageSlider;
    public static WeaponDamageSlider teslaTowerWeaponDamageSlider;

    public enum WeaponDamageSliderType
    {
        gun, missile, laser, lightning, teslaTower
    }
    public WeaponDamageSliderType thisWeaponDamageSliderType = WeaponDamageSliderType.gun;

    private void Awake()
    {
        damageSliderImage = GetComponentsInChildren<Image>()[1];
        damageTotalText = GetComponentInChildren<Text>();
    }
    private void Start()
    {
        switch (thisWeaponDamageSliderType)
        {
            case WeaponDamageSliderType.gun:
                gunWeaponDamageSlider = this;
                break;
            case WeaponDamageSliderType.missile:
                missileWeaponDamageSlider = this;
                break;
            case WeaponDamageSliderType.laser:
                laserWeaponDamageSlider = this;
                break;
            case WeaponDamageSliderType.lightning:
                lightningWeaponDamageSlider = this;
                break;
            case WeaponDamageSliderType.teslaTower:
                teslaTowerWeaponDamageSlider = this;
                break;
            default:
                break;
        }
        UpdateDamageText();
        CheckShowOrNot();
    }
  
   
    public void UpdateDamagePercent(float _percent)
    {
        Vector3 localPosA = new Vector3(0, 0, 0);
        Vector3 localPosB = new Vector3(100, 0, 0);

        Vector2 sizeA = new Vector2(120, 20);
        Vector2 sizeB = new Vector2(0, 20);

        damageSliderImage.transform.localPosition = Vector3.Lerp(localPosB, localPosA, _percent);
        damageSliderImage.GetComponent<RectTransform>().sizeDelta = Vector2.Lerp(sizeB, sizeA, _percent);
        Vector3 textPosRight = new Vector3(60, 0, 0);
        Vector3 textPosLeft = new Vector3(-107, 0, 0);

        GetComponentInChildren<Text>().transform.localPosition = Vector3.Lerp(textPosRight, textPosLeft, _percent);
    }

    public void DealDamage(float damage)
    {
        damageTotal += damage;
        UpdateDamageText();
    }
    void UpdateDamageText()
    {
        damageTotalText.text = damageTotal.ToString("#0.0");
    }
    public void ResetDamageValue()
    {
        damageTotal = 0;
        UpdateDamageText();
    }


    public void Show()
    {
       // transform.localScale = Vector3.one * .63f;
    }
    public void Hide()
    {
        transform.localScale = Vector3.zero;
    }
    public void CheckShowOrNot()
    {
        switch (thisWeaponDamageSliderType)
        {
            case WeaponDamageSliderType.gun:
                if (WeaponDataManager.instance.weaponGunUnlocked) Show();
                else Hide();
                break;
            case WeaponDamageSliderType.missile:
                if (WeaponDataManager.instance.weaponMissileUnlocked) Show();
                else Hide();
                break;
            case WeaponDamageSliderType.laser:
                if (WeaponDataManager.instance.weaponLaserUnlocked) Show();
                else Hide();
                break;
            case WeaponDamageSliderType.lightning:
                if (WeaponDataManager.instance.weaponLightningUnlocked) Show();
                else Hide();
                break;
            case WeaponDamageSliderType.teslaTower:
                if (WeaponDataManager.instance.weaponTeslaTowerUnlocked) Show();
                else Hide();
                break;
            default:
                break;
        }
    }
}
