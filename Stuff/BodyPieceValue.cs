using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BodyPieceValue : MonoBehaviour
{
    bool moving;
    private void Awake()
    {
        moving = true;
    }
    void FixedUpdate()
    {
        if (BodyCollectorDevice.instance)
        {
            if (moving)
            {
                transform.position += (BodyCollectorDevice.instance.transform.position + Vector3.up * .25f - transform.position).normalized * Time.deltaTime * 5;

                if (Vector3.Distance(transform.position, BodyCollectorDevice.instance.transform.position + Vector3.up * .25f) < .2f)
                {
                    moving = false;
                    transform.DOScale(0, 1);
                    BodyCollectorDevice.instance.AddBodyPieceValue();
                    Destroy(gameObject, 1);
                }

            }
           
        }
    }
}
