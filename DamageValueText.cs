using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class DamageValueText : MonoBehaviour
{
    public TextMeshPro textMeshPro;

    private void Start()
    {
        transform.position += new Vector3(Random.value - .5f, 0, Random.value - .5f)*.35f;
        transform.DOMoveY(.75f, 1f);
    }
    private void OnEnable()
    {
        Start();
    }
    public void SetText(float value)
    {
        value = Mathf.RoundToInt(value);
        textMeshPro = GetComponentInChildren<TextMeshPro>();
        textMeshPro.text = value.ToString();
        textMeshPro.GetComponent<MeshRenderer>().sortingOrder = 99;
    }
}
