using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    public Image sliderValue;

    public Text waveIndexTextMeshPro;

    public Image[] sliderValues;
    private void Awake()
    {
        instance = this;
    }

    public void UpdateSlider(float _percent)
    {
        sliderValue.transform.localPosition = new Vector3(Mathf.Lerp(-475,0,_percent), 0, 0);
        if (_percent<1)
        {
            SliderRolling();
        }
    }
    public void UpdateWaveIndex(int _waveIndex)
    {
        waveIndexTextMeshPro.text = (_waveIndex +1).ToString();
        
    }
  
    public void SliderRolling()
    {
        sliderValues[0].transform.localPosition += Vector3.left  * 1.5f;
        sliderValues[1].transform.localPosition += Vector3.left * 1.5f;

        if (sliderValues[0].transform.localPosition.x<=-475)
        {
            sliderValues[0].transform.localPosition = new Vector3(475, 0, 0);
        }
        if (sliderValues[1].transform.localPosition.x <= -475)
        {
            sliderValues[1].transform.localPosition = new Vector3(475, 0, 0);
        }
    }
}
