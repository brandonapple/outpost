using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityRangeIndicator : MonoBehaviour
{
    public float abilityRadius;
    public float radius;
    public GameObject[] rings;

    private void Start()
    {
        transform.localScale = Vector3.one * abilityRadius;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void FixedUpdate()
    {
        foreach (GameObject ring in rings)
        {
            ring.transform.localScale = Vector3.one * (ring.transform.localScale.x + .005f);

            if (ring.transform.localScale.x>.53f)
            {
                ring.transform.localScale = Vector3.one * .05f;
            }

        }
    }
}
