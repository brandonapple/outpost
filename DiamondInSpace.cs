using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondInSpace : MonoBehaviour
{
    public GameObject motherShipLibrary;
    Vector3 dir;
    private void Start()
    {
        dir = new Vector3((Random.value - .5f)*1f, -1, 0);
        dir = dir.normalized;
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0,360));
    }

    private void FixedUpdate()
    {
        dir = Vector3.Lerp(dir, (motherShipLibrary.transform.position - transform.position).normalized, .01f);

        transform.position += dir * Time.deltaTime * 4;
        if (Vector3.Distance(motherShipLibrary.transform.position,transform.position)<.3f)
        {
            Destroy(gameObject);
        }
    }
}
