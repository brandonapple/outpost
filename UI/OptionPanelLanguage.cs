using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionPanelLanguage : MonoBehaviour
{
    public Text englishButtonText;
    public Text chineseButtonText;

    private void Awake()
    {
        englishButtonText.color = Color.gray;
        chineseButtonText.color = Color.gray;
    }
    public void LanguageChangeButton(int index)
    {
        FindObjectOfType<OptionsManager>().LanguageButtonClick(index);
        switch (index)
        {
            case 0:
                englishButtonText.color = Color.red;
                chineseButtonText.color = Color.gray;
                break;
            case 1:
                englishButtonText.color = Color.gray;
                chineseButtonText.color = Color.red;
                break;
            default:
                break;
        }

        Destroy(gameObject,1);
    }
}
