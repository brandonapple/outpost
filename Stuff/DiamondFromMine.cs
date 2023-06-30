using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondFromMine : MonoBehaviour
{
    public float value;
    Vector3 dirToBase;
    float distance;
    private void Start()
    {
        switch (value)
        {
            case 1:
                transform.localScale = Vector3.one;
                break;
            case 10:
                transform.localScale = Vector3.one * 3;
                break;
            default:
                transform.localScale = Vector3.one;
                break;
        }
    }
    private void FixedUpdate()
    {
        dirToBase = Base.instance.transform.position - transform.position;
        dirToBase = dirToBase.normalized;
        distance = Vector3.Distance(transform.position, Base.instance.transform.position);


        if (distance >.5f)
        {
            transform.position += dirToBase * Time.deltaTime * 2;
        }
        else if (distance<=.5f)
        {
            Destroy(gameObject);
            //Base.instance.GetDiamondMineral();
            switch (value)
            {
                case 1:
                    Base.instance.GetDiamondMineral(1);
                    break;
                case 10:
                    Base.instance.GetDiamondMineral(10);
                    break;
                default:
                    Base.instance.GetDiamondMineral(1);
                    break;
            }
        }
    }
}
