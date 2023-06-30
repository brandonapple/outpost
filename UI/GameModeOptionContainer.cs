using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameModeOptionContainer : MonoBehaviour
{
    public Text unlockedConditionText;
    public Image[] images;
    private void Awake()
    {
        images = GetComponentsInChildren<Image>();
    }
   

    public void LockOption()
    {
        foreach (Image image in images)
        {
            image.color = Color.gray;
        }
        if (unlockedConditionText != null)
        {
            unlockedConditionText.gameObject.SetActive(true);
        }
        GetComponent<Button>().enabled = false;
    }
    public void UnlockOption()
    {
        foreach (Image image in images)
        {
            image.color = Color.white;
        }
        if (unlockedConditionText != null)
        {
           unlockedConditionText.gameObject.SetActive(false);
        }
        GetComponent<Button>().enabled = true;
    }
}
