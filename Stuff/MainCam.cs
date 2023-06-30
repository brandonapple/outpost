using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MainCam : MonoBehaviour
{
    public static MainCam instance;
    public enum CamState { Normal,FellowChildShip}
    public CamState thisCamState = CamState.Normal;
    Vector3 offset;
   
    private void Awake()
    {
        instance = this;
        transform.localPosition = new Vector3(0, 40, transform.position.z);
    }
   
    public void ShowBattleField()
    {
        GetComponent<Camera>().DOOrthoSize(plantCamSize , .5f);
        transform.DOMoveX(0, 1);
    }
  
    public void TurnToShowMotherShipSize()
    {
        GetComponent<Camera>().DOOrthoSize(motherShipCamSize, .5f) ;
    }

    float motherShipCamSize
    {
        get
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                return 5.5f * optionManageViewRangeValue;
            }
            else
            {
               return 6.65f * optionManageViewRangeValue;
            }
        }
    }
    float plantCamSize
    {
        get
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                return (GunRangeManager.instance.rangeRadius - .5f) * optionManageViewRangeValue;
            }
            else
            {
                return (GunRangeManager.instance.rangeRadius) * optionManageViewRangeValue;
            }
        }
    }
    public float optionManageViewRangeValue
    {
        get
        {
            if (OptionsManager.instance==null)
            {
                return 1;
            }
            if (OptionsManager.instance.gameObject.activeSelf)
            {
                return OptionsManager.viewRangeValue;
            }
            return 1;
        }
    }
    public void BeginFellowChildShip()
    {
        thisCamState = CamState.FellowChildShip;
        offset = transform.position - ChildShip.instance.transform.position;
        offset = new Vector3(0, 9, offset.z);
       
    }
    public void SkipLandingAnimationEndFellowChildShip()
    {
        thisCamState = CamState.Normal;
    }
    public void TurnToBattleFieldViewFast()
    {
        GetComponent<Camera>().orthographicSize = plantCamSize;
    }

    public void EndFellowChildShipInSpace()
    {
        thisCamState = CamState.Normal;
    }

    public void SkipToPlantPos()
    {
        transform.position = new Vector3(0, 9.5f, -15.4f);
    }
    public void EndFellowShipInPlantAndMoveToTargetPos()
    {
        thisCamState = CamState.Normal;
        transform.DOLocalMoveY(9.5f, 2);
    }


    public void SkipToMotherShipPos()
    {
        transform.position = new Vector3(0, 41.28f, -15.4f);
    }

    public void CamShake()
    {
        if (!OptionsManager.camShakeOn) return;

        transform.DOShakeRotation(.1f, .15f, 40, 15, true);

        StopCoroutine(RotationBackIE());
        StartCoroutine(RotationBackIE());
        IEnumerator RotationBackIE()
        {
            yield return new WaitForSeconds(.1f);
            transform.rotation = Quaternion.Euler(30, 0, 0);
        }
    }


    public void ChangeViewRange()
    {
        if (GameManager.instance.thisGameState == GameManager.GameState.playing)
        {
            ShowBattleField();
        }
        else
        {
            TurnToShowMotherShipSize();
        }
    }
    public void FixedUpdate()
    {
        switch (thisCamState)
        {
            case CamState.Normal:
                break;
            case CamState.FellowChildShip:
                Vector3 targetPos = ChildShip.instance.transform.position + offset;
                transform.position = Vector3.Lerp(transform.position, targetPos, .02f);
                break;
            default:
                break;
        }
    }
}
