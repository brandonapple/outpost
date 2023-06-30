using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeThreeEye : Slime
{
    public override void FixeMoveDir()
    {
        Vector3 dirA = transform.position.normalized;
        Vector3 normalDir = Vector3.Cross(dirA, Vector3.up);
        Vector3 targetDir = (-dirA + normalDir).normalized;
        moveDir = Vector3.Lerp(moveDir, targetDir, .02f);
    }

}
