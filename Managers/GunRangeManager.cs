using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GunRangeManager : MonoBehaviour
{
    public static GunRangeManager instance;
    public LineRenderer rangeLineRenderer;

    public GameObject viewRange;
    public float rangeRadius;

    Color2 color2transparent = new Color2(new Color(1, 1, 1, 0), new Color(1, 1, 1, 0));
    Color2 color2white = new Color2(new Color(1, 1, 1, 1), new Color(1, 1, 1, 1));

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        UpdateRange();
    }

    [ContextMenu("Update Range")]
    public void UpdateRange()
    {
        rangeLineRenderer.positionCount = 40;
        rangeRadius = WeaponDataManager.instance.gunRangeValueCurrent;

        if (RelicManager.instance.radarCount>0)
        {
            rangeRadius += RelicManager.instance.radarViewRadiusValue;
        }


        for (int i = 0; i < 40; i++)
        {
            float angle = (float)i / 39 * Mathf.PI * 2;
            rangeLineRenderer.SetPosition(i, new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) *rangeRadius);
        }
        ShowRange();
        viewRange.transform.DOScale(Vector3.one * rangeRadius / 4, .5f);

        if (GameManager.instance.thisGameState == GameManager.GameState.playing)
        {
          MainCam.instance.ShowBattleField();
        }
    }
   
    public void ShowRange()
    {
        rangeLineRenderer.DOColor(color2transparent, color2white, 1f);
    }
    public void HideRange()
    {
        rangeLineRenderer.DOColor(color2white, color2transparent, 1f);
    }

}
