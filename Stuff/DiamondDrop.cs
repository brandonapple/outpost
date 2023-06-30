using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondDrop : MonoBehaviour
{
    public Vector3 speed;
    public float value;


    private void Awake()
    {
        speed = new Vector3(Random.value - .5f, Random.value +1, Random.value - .5f).normalized;
        speed *= Random.Range(5,12);
    }

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
            case 100:
                transform.localScale = Vector3.one * 6;
                break;
            default:
                transform.localScale = Vector3.one;
                break;
        }
    }
    private void FixedUpdate()
    {
        if (transform.position.y < 0)
        {
            return;
        }
        speed += Vector3.down * Time.deltaTime * 10;
        transform.position += speed * Time.deltaTime;
    }
}
