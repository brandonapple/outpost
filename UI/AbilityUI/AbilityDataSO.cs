using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName ="abilityDataSO",menuName ="ScriptableObject/abilityDataSO")]
public class AbilityDataSO : ScriptableObject
{
    public string abilityName;
    public Sprite abilityIcon;
    public float radius;
    public float interval;
   
}
