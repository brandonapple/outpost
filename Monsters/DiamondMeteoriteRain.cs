using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondMeteoriteRain : Monster
{
    public DiamondMeteorite.MeteoriteMineralType rainType = DiamondMeteorite.MeteoriteMineralType.Diamond;
    public override void Awake()
    {
        //base.Awake();
    }
    public override void Start()
    {
        avaliable = false;
        //base.Start();
        StartCoroutine(SpawnDiamondMeteorite());
    }
    public  IEnumerator  SpawnDiamondMeteorite()
    {
        TipPanel.instance.ShowTipSingle("一大波流星雨来袭，它们会破坏建筑，但也会带来丰富的矿物");

        yield return new WaitForSeconds(2);
        int meteoriteCount = Random.Range(60, 120);

        float fromPosCenterAngle = Random.value * Mathf.PI * 2;
        Vector3 fromPos = new Vector3(Mathf.Cos(fromPosCenterAngle), 5, Mathf.Sin(fromPosCenterAngle));
        float f = Random.value;
        if (f<.5f)
        {
            rainType = DiamondMeteorite.MeteoriteMineralType.Gold;
        }
      
        for (int i = 0; i < meteoriteCount; i++)
        {
            DiamondMeteorite diamondMeteorite=null;
            switch (rainType)
            {
                case DiamondMeteorite.MeteoriteMineralType.Diamond:
                    diamondMeteorite = Instantiate(Resources.Load<DiamondMeteorite>("Prefab/diamondMeteorite"));
                    break;
                case DiamondMeteorite.MeteoriteMineralType.Gold:
                    diamondMeteorite = Instantiate(Resources.Load<DiamondMeteorite>("Prefab/meteoriteB"));
                    break;
            }

            diamondMeteorite.transform.position = fromPos;
            diamondMeteorite.fallDir = -fromPos.normalized;
            float angle = Random.value * Mathf.PI * 2;
            diamondMeteorite.transform.position += new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) *5 * Random.value;
            float timeInterval = Random.Range(.05f, .15f);
            yield return new WaitForSeconds(timeInterval);
        }
        Destroy(gameObject, 1);
    }

    public override void FixedUpdate()
    {
        //base.FixedUpdate();
    }

}
