using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class ExplosionShadow : MonoBehaviour
{
    public SpriteRenderer[] shadows;
    public Color outSideShadowColor;
    public Color insideShadowColor;

    private void Start()
    {
        foreach (SpriteRenderer shadow in shadows)
        {
            shadow.DOFade(0, 3);
        }
        
    }
    private void OnEnable()
    {
        shadows[0].color = outSideShadowColor;
        shadows[1].color = insideShadowColor;

        Start();
    }

    public void SetSize(float size)
    {
        transform.localScale = Vector3.one * size;
    }
}
