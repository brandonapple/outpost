using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBodyBloodMark : MonoBehaviour
{
    ParticleSystem mainPar;
    private void Awake()
    {
        mainPar = GetComponentInChildren<ParticleSystem>();
    }
    public void SetColor(Color _color)
    {
        var main = mainPar.main;
        main.startColor = _color;
    }
}
