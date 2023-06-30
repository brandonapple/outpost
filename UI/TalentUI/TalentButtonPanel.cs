using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class TalentButtonPanel : MonoBehaviour
{
    public static TalentButtonPanel instance;

    public Image pointCountTextContainer;
    public Text pointCountText;
    bool haveToUseTalentPoint;
    float shakeTimer;

    private void Awake()
    {
        instance = this;
        
    }
    private void Start()
    {
        CheckIfToUseTalentPoints();
    }

    public void CheckIfToUseTalentPoints()
    {
        int toUseTalentPoints = TalentPanel.instance.pointsToUseCount;
        if (toUseTalentPoints>0)
        {
            pointCountTextContainer.gameObject.SetActive(true);
            pointCountText.text = toUseTalentPoints.ToString();
            haveToUseTalentPoint = true;
        }
        else
        {
            pointCountTextContainer.gameObject.SetActive(false);
            haveToUseTalentPoint = false;
        }
    }
    private void FixedUpdate()
    {
        if (haveToUseTalentPoint)
        {
            shakeTimer += Time.deltaTime;
            if (shakeTimer>1)
            {
                shakeTimer = 0;
                Shake();
            }
        }
    }

    void Shake()
    {
        transform.localScale = Vector3.one;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.DOShakeScale(.5f, .25f, 50, 10, true);
        transform.DOShakeRotation(.5f, .25f, 50, 10, true);
    }
}
