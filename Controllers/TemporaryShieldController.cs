using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryShieldController : MonoBehaviour
{
    public static TemporaryShieldController instance;

    public float shieldValue;
    public float shieldValueMax;
    public bool shieldExist;

    public LineRenderer shieldLine;
    private void Awake()
    {
        instance = this;
    }
    
    public void RechargeShield()
    {
        shieldExist = true;
        shieldValue = shieldValueMax;
    }
    public void Hitted(float value)
    {
        shieldValue -= value;
        if (shieldValue<=0)
        {
            shieldValue = 0;
            shieldExist = false;
        }

        RenderLine();
    }


    [ContextMenu("controller on")]

    public void ControllerOn()
    {
        RechargeShield();
        RenderLine();
    }
    public void RenderLine()
    {
        shieldLine.gameObject.SetActive(true);
        shieldLine.positionCount = (int)shieldValue;

        for (int i = 0; i < shieldLine.positionCount; i++)
        {
            float angle = Mathf.PI * 1.5f -Mathf.PI * .33f *.5f;
            angle += i * (Mathf.PI * .11f *4 / 20);
            Vector3 pos = new Vector3(Mathf.Cos(angle),0, Mathf.Sin(angle))*2.25f;
            shieldLine.SetPosition(i, pos);
        }
    }
    public void ControllerOff()
    {
        shieldLine.gameObject.SetActive(false);
        shieldValue = 0;
        shieldExist = false;
        //Debug.Log("off");
    }

    public void WaveClean()
    {
        RechargeShield();
        RenderLine();
    }
}
