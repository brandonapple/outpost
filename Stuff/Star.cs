using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public float lightStrength;
    float shainingSpeed;
    SpriteRenderer starSpriteRenderer;
    float offset;
    private void Awake()
    {
        shainingSpeed = Random.Range(1f, 2f);
        starSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        lightStrength = Random.Range(0, 1);
        offset = Random.value * Mathf.PI *2;
    }
    private void Update()
    {
        lightStrength = Mathf.Cos(Time.time*shainingSpeed + offset);
        if (lightStrength > .5f) lightStrength = 1;
        starSpriteRenderer.color = new Color(1, 1, 1, lightStrength);

    }
}
