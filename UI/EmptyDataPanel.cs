using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyDataPanel : MonoBehaviour
{
    public void EmptyDataButtonClick()
    {
        CoinsManager.instance.ResetButtonClick();
        HistoryManager.instance.ResetData();


       // FindObjectOfType<OptionsManager>().ResetData();
        FindObjectOfType<WeaponDataManager>().ResetData();


        Destroy(gameObject);
        GameManager.instance.ReloadScene();
    }
    public void CancelDataButtonClick()
    {
        Destroy(gameObject);
    }
}
