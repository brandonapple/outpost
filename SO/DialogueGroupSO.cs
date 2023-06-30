using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "dialogueGroup",menuName ="ScriptableObject/dialogueGroup")]
public class DialogueGroupSO : ScriptableObject
{
    public int appearTime;
    public dialogue[] dialogues;

}
