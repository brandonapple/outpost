using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using DG.Tweening;
public class GameSpeedManager : MonoBehaviour
{
    public static GameSpeedManager instance;
    public Text textMeshPro;

    public GameObject speedChanger;
    public enum GameSpeedState
    {
        Normal,
        Double,
        FourTimes,
        Pause,
    }
    public GameSpeedState thisGameSpeedState = GameSpeedState.Normal;

    
    public Color normalColor, doubleColor, fourTimesColor;
    private void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        UpdateTimeText();
    }

   

    public void UpdateTimeText()
    {
        switch (thisGameSpeedState)
        {
            case GameSpeedState.Normal:
                textMeshPro.text =speedText+ "× 1";
                break;
            case GameSpeedState.Double:
                textMeshPro.text = speedText + "× 2";
                break;
            case GameSpeedState.FourTimes:
                textMeshPro.text = speedText + "× 4";
                break;
            case GameSpeedState.Pause:
                textMeshPro.text = speedText + "× 0";
                break;
            default:
                break;
        }
    }
    public string speedText
    {
        get
        {
          return LanguageManager.GetText("speed");
        }
    }
    public void TimeScaleBackToNormal()
    {
        thisGameSpeedState = GameSpeedState.Normal;
        Time.timeScale = 1;
        UpdateTimeText();
    }


    public void GamePause()
    {
        Time.timeScale = 0;
    }
    public void GameUnPause()
    {
        GetCurrentTimeScale();
    }

    void GetCurrentTimeScale()
    {
        switch (thisGameSpeedState)
        {
            case GameSpeedState.Normal:
                Time.timeScale = 1;
                break;
            case GameSpeedState.Double:
                Time.timeScale = 2;
                break;
            case GameSpeedState.FourTimes:
                Time.timeScale = 4;
                break;
            case GameSpeedState.Pause:
                Time.timeScale = 0;
                break;
            default:
                break;
        }
    }

    

    public void SpeedDownOrUpButton(bool up)
    {
        switch (thisGameSpeedState)
        {
            case GameSpeedState.Pause:
                if (up) thisGameSpeedState = GameSpeedState.Normal;
                break;
            case GameSpeedState.Normal:
                if (up) thisGameSpeedState = GameSpeedState.Double;
                else thisGameSpeedState = GameSpeedState.Pause;
                break;
            case GameSpeedState.Double:
                if (up) thisGameSpeedState = GameSpeedState.FourTimes;
                else thisGameSpeedState = GameSpeedState.Normal;
                break;
            case GameSpeedState.FourTimes:
                if (!up) thisGameSpeedState = GameSpeedState.Double;
                break;
            default:
                break;
        }
        GetCurrentTimeScale();
        UpdateTimeText();

        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.DOShakeRotation(.15f, 60, 60, 60, true).SetUpdate(true);

        StopCoroutine(RotationBackIE());
        StartCoroutine(RotationBackIE());
        IEnumerator RotationBackIE()
        {
            yield return new WaitForSeconds(.15f);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            switch (thisGameSpeedState)
            {
                case GameSpeedState.Normal:
                    thisGameSpeedState = GameSpeedState.Double;
                    break;
                case GameSpeedState.Double:
                    thisGameSpeedState = GameSpeedState.FourTimes;
                    break;
                case GameSpeedState.FourTimes:
                    thisGameSpeedState = GameSpeedState.Normal;
                    break;
                default:
                    break;
            }
            GetCurrentTimeScale();
            UpdateTimeText();
        }
    }

    public void HideSpeedChanger()
    {
        speedChanger.gameObject.SetActive(false);
    }
    public void ShowSpeedChanger()
    {
        speedChanger.gameObject.SetActive(true);
    }
}
