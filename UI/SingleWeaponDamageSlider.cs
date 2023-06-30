using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SingleWeaponDamageSlider : MonoBehaviour
{

    public Text damageTotalText;
    public Image damageSliderImage;

    private void Start()
    {
        SetSlider(0);
        SetValueText(0);
    }

    public void SetSlider(float percent)
    {
        percent = Mathf.Clamp01(percent);

        Vector3 centerPosMin = new Vector3(80, 0, 0);
        Vector3 centerPosMax = new Vector3(-20, 0, 0);

        Vector2 sizeMin = new Vector2(0, 20);
        Vector2 sizeMax = new Vector2(200, 20);

        damageSliderImage.transform.localPosition = Vector3.Lerp(centerPosMin, centerPosMax, percent);
        damageSliderImage.GetComponent<RectTransform>().sizeDelta = Vector2.Lerp(sizeMin, sizeMax, percent);
    }

    public void SetValueText(float value)
    {
        damageTotalText.text = ((int)value).ToString();
    }

}
