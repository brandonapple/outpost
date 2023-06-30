using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCDIcon : MonoBehaviour
{
    public LineRenderer lineRenderer;

    public int pointCount;
    public float cdPercent;
    public float radiusMultiplier =1;
    [ContextMenu("set cd circle")]
    public void SetCDCircle(float _cdPercent)
    {
        pointCount =(int)(_cdPercent * 30);
        if (pointCount<=0)
        {
            pointCount = 0;
        }
        lineRenderer.positionCount = pointCount;

        for (int i = 0; i < pointCount; i++)
        {
            float f = (float)i;
            float radius = .15f;
            lineRenderer.SetPosition(i, new Vector3( Mathf.Cos(Mathf.PI * 2 * f / 29 - Mathf.PI*.5f),0, 
                Mathf.Sin(Mathf.PI * 2 * f / 29 - Mathf.PI * .5f)) * radius * radiusMultiplier);
        }
    }
    public void SetRadiusMultiplier(float value)
    {
        radiusMultiplier = value;
    }
}
