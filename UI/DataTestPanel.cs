using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class DataTestPanel : MonoBehaviour
{
    private void Awake()
    {
        transform.localScale = Vector3.zero;
        show = false;
    }
    public InputField inputField;
    bool show;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
           
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (show)
            {
                transform.localScale = Vector3.zero;
                Debug.Log(inputField.text);
                inputField.DeactivateInputField();

                string _string = inputField.text;
                bool onlyDigits = Regex.IsMatch(_string, @"^[0-9]+$");
                if (onlyDigits)
                {
                    int count = int.Parse(_string);
                    CoinsManager.instance.AddTestDiamondPermanent(count);
                    CoinsManager.instance.AddMoney(count);
                }
               
                inputField.text = "";
            }
            else
            {
                transform.localScale = Vector3.one;
                inputField.ActivateInputField();
            }
            show = !show;
        }
    }
}
