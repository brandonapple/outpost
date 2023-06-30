using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathCalculate : MonoBehaviour
{
    public static int Accumlate(int a )
    {
        int result = 1;
        for (int i = 1; i <= a; i++)
        {
            result += i;
        }

        
        return result;
    }
    
}
