using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityContainer : MonoBehaviour,IPointerDownHandler
{
    public string abilityName;
    public Sprite abilityIcon;
    public float abilityRadius;

    public float timer;
    public float interval;
    public Image CDMask;



    public Image abilityIconImage;


    private void Awake()
    {
        timer = interval;
        SetCDMask();
    }
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (timer < interval) return;
        AbilityManager.instance.ChooseAbility(this);
    }


    public void AbilityInvoke()
    {
        timer = 0;
        SetCDMask();
    }
    private void Update()
    {
        if (timer<interval)
        {
            timer += Time.deltaTime;
            SetCDMask();
        }
    }
    void SetCDMask()
    {
        float percent =1- timer/interval;

        Vector2 localSize = new Vector2(150, Mathf.Lerp(0, 150,percent));
        Vector3 localPos = new Vector3(0, Mathf.Lerp(-75, 0, percent));

        CDMask.GetComponent<RectTransform>().sizeDelta = localSize;
        CDMask.transform.localPosition = localPos;

    }

    public void ShowData()
    {
        abilityIconImage.sprite = abilityIcon;
    }
}
