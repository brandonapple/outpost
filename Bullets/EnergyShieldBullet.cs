using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyShieldBullet : MonoBehaviour
{
    public void Start()
    {
        Monster[] monsters = FindObjectsOfType<Monster>();
        foreach (Monster monster in monsters)
        {
            monster.Stuned();
        }
        Destroy(gameObject, 2);
    }
}
